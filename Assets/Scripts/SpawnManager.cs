using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject _powerupContainer;

    private bool _stopSpawning = false;

    public void StartSpawning()
    {
        //Start Coroutines here
        //Will start spawning coroutines in the 'asteroid' class
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }


    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f); //waits 3 seconds to spawn enemy after astroid is destoryed
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.0f, 9.0f), 8, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3.0f);
        }
    }


    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f); //waits 3 seconds to spawn powerup after astroid is destoryed
        while (_stopSpawning == false)
        {
            float wait_time = Random.Range(3.0f, 8.0f);

            Vector3 posToSpawn = new Vector3(Random.Range(-9.0f, 9.0f), 8, 0);
            int randomPowerUp = Random.Range(0, 3);
            GameObject PowerUp = Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            PowerUp.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(wait_time);
        }
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }


}
