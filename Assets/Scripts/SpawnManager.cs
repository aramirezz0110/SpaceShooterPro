using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private float spawningWaitTime=5f;

    private bool stopSpawning;
    private Vector3 posToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine(spawningWaitTime));
    }
    #region Coroutines
    private IEnumerator SpawnRoutine(float waitTime)
    {
        while (!stopSpawning)
        {
            posToSpawn = new Vector3(Random.Range(-9, 9), 4, 0);
            Instantiate(enemyPrefab, posToSpawn, Quaternion.identity, enemyContainer);

            yield return new WaitForSeconds(waitTime);
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
