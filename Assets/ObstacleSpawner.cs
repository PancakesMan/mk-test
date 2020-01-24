using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject[] SpawnPoints;

    //public float SpawnChance;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate the enemy
        GameObject enemy = Instantiate(EnemyPrefab);

        // Ensure it spawns in the correct position
        enemy.transform.SetParent(SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
