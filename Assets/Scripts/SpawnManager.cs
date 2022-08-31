using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemy References")]
    [SerializeField] private GameObject enemyPrefab;    
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private float spawningWaitTime=5f;

    [Header("Power up References")]
    [SerializeField] private List<GameObject> powerUps;
    [SerializeField] private float powerUpSpawnWaitTime = 5f;

    private bool stopSpawning;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine(spawningWaitTime));
        StartCoroutine(SpawnPowerUpRoutine());
    }
    #region Coroutines
    private IEnumerator SpawnEnemyRoutine(float waitTime)
    {
        while (!stopSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9, 9), 4, 0);
            Instantiate(enemyPrefab, posToSpawn, Quaternion.identity, enemyContainer);

            yield return new WaitForSeconds(waitTime);
        }
    }
    private IEnumerator SpawnPowerUpRoutine()
    {        
        while (!stopSpawning)
        {
            yield return new WaitForSeconds(Random.Range(3,8));
            Vector3 posToSpawn = new Vector3(Random.Range(-9, 9), 4, 0);
            int randomPowerUp = Random.Range(0, powerUps.Count);
            Instantiate(powerUps[randomPowerUp], posToSpawn, Quaternion.identity);
        }
    }
    #endregion

    #region Public Methods
    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }
    #endregion
}
