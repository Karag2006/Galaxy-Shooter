using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private float _spawnHeight = 9f;

    [SerializeField]
    private float _spawnTime = 5f;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private GameObject _enemyContainer;


    private bool _stopSpawning = false;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemyRoutine()
    {
        while(_stopSpawning == false)
        {
            float randomXPosition = Random.Range(-8f, 8f);
            Vector3 SpawnPosition = new Vector3(randomXPosition, _spawnHeight, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, SpawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnTime);
        }
    }
    IEnumerator SpawnPowerupRoutine()
    {
        while(_stopSpawning == false)
        {
            float spawnDelay = Random.Range(5f, 10f);
            yield return new WaitForSeconds(spawnDelay);

            float rXPosition = Random.Range(-8f, 8f);
            Vector3 SpawnPosition = new Vector3(rXPosition, _spawnHeight, 0);
            int randomPowerup = Random.Range(0, 2);
            Instantiate(_powerups[randomPowerup], SpawnPosition, Quaternion.identity);

        }
    }



    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
