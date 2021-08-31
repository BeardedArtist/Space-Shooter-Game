using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField]
    private float _spinSpeed = 19.0f;

    private Player _player;
    private SpawnManager _spawnManager;

    [SerializeField]
    private GameObject _explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.Log("player is null");
        }

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // rotate object on the z axis.
        transform.Rotate(Vector3.forward * _spinSpeed * Time.deltaTime);

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

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _spawnManager.StartSpawning(); // This is starting our enemy spawning from SpawnManager
            Destroy(this.gameObject, 0.25f);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _spawnManager.StartSpawning(); // This is starting our enemy spawning from SpawnManager
            Destroy(this.gameObject, 0.25f);
            

        }

    }

}
