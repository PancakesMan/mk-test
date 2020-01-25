using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject[] SpawnPoints;

    private GameObject EnemyInstance;

    // Awake is called before OnEnable
    void Awake()
    {
        // Instantiate the enemy object
        EnemyInstance = Instantiate(EnemyPrefab);

        // Spawn it in one of the random spawn points
        EnemyInstance.transform.SetParent(SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform, false);
    }

    // When pooled platform is reused
    private void OnEnable()
    {
        // Move the saw instance to one of the spawn points randomly
        EnemyInstance.transform.SetParent(SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform, false);
    }
}
