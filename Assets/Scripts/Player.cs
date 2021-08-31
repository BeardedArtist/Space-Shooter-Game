using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _SpeedMultiplier = 2.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.3f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;


    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
    private bool _isRightEngineActive = false;
    private bool _isLeftEngineActive = false;
    
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _rightEngineDamageVisualizer, _leftEngineDamageVisualizer;
    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;
    private SpawnManager _spawnManager;

    //Audio clip variables
    [SerializeField]
    private AudioClip _laserAudioClip;
    private AudioSource _audioSource;
    private AudioSource _explosionAudio;

    

    // Start is called before the first frame update
    void Start()
    {
        // Take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>(); //find object. get component.
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>(); //find object. get component.
        _audioSource = GetComponent<AudioSource>();
        _explosionAudio = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL.");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL");
        }

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on the player is NULL!");
        }
        else
        {
            _audioSource.clip = _laserAudioClip;
        }

        if (_explosionAudio == null)
        {
            Debug.LogError("Explosion AudioSource on player is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

        

    }


    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);


        // If statements controlling player boundry Y
        if (transform.position.y >= 3.5)
        {
            transform.position = new Vector3(transform.position.x, 3.5f, 0);
        }
        else if (transform.position.y <= -3.9)
        {
            transform.position = new Vector3(transform.position.x, -3.9f, 0);
        }

        // If statements controlling player boundry X
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

    }

    void FireLaser()
    {
        // Function for fire button and cooldown
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(1.400157f, 1.56f, -9.449708f), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        // Play laser audio clip
        _audioSource.Play();

    }

    public void Damage()
    {

        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        else if (_isShieldActive == false)
        {
            _lives--;

            if (_lives == 2)
            {
                RightEngineDamage();
            }
            else if (_lives == 1)
            {
                LeftEngineDamage();
            }
            

            _uiManager.UpdateLives(_lives);

            if (_lives < 1)
            {
                _spawnManager.onPlayerDeath();
                Destroy(this.gameObject);
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _explosionAudio.Play();
            }
        }


    }


    public void TripleShotActive()
    {
        //tripleshotActive becomes true
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    //IEnumerator TripleShotPowerDownRoutine
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _SpeedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    //IEnumerator SpeedBoostPowerDownRoutine
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _SpeedMultiplier;
    }


    public void ShieldsActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void RightEngineDamage()
    {
        _isRightEngineActive = true;
        _rightEngineDamageVisualizer.SetActive(true);
    }

    public void LeftEngineDamage()
    {
        _isLeftEngineActive = true;
        _leftEngineDamageVisualizer.SetActive(true);
    }


    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

}
