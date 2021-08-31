using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;

    private Player _player;
    private Animator _anim;

    [SerializeField]
    private AudioClip _explosionAudioClip;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //Caching the player object to easily call functions from player
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("Player is Null");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("anim is NULL");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on Enemy is NULL");
        }
        else
        {
            _audioSource.clip = _explosionAudioClip;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Move down at 4 meters per second
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        //If bottom of screen respawn at top

        if (transform.position.y <= -10)
        {
            float randomX = Random.Range(-9.0f, 9.0f);
            transform.position = new Vector3(randomX, 8, 0);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            EnemyDeathAnimation(); //Play animation and then delete object after (2.8) seconds (LINE 60,61)
            _enemySpeed = 1.0f;
            _audioSource.Play();
            Destroy(this.gameObject, 2.9f);
            Destroy(this.GetComponent<BoxCollider2D>());
        }


        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(Random.Range(5,20));
            }

            EnemyDeathAnimation(); //Play animation and then delete object after (2.8) seconds (LINE 73,74)
            _enemySpeed = 1.0f;
            _audioSource.Play();
            Destroy(this.gameObject, 2.9f);
            Destroy(this.GetComponent<BoxCollider2D>());
        }

    }

    public void EnemyDeathAnimation()
    {
        _anim.SetTrigger("OnEnemyDeath");
    }
}
