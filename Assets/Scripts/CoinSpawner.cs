using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject CoinPrefab;         // Prefab to spawn
    public GameObject[] CoinSpawnPoints;  // Available places to spawn it
    public float CoinSpawnChance = 0.5f;  // Chance of it spawning at each point

    // Start is called before the first frame update
    void Start()
    {
        // For each spawn point available
        foreach (GameObject go in CoinSpawnPoints)
        {
            // Determine if we will spawn a coin here
            if (Random.Range(0.0f, 1.0f) < CoinSpawnChance)
            {
                // Instantiate the coin object
                GameObject coin = Instantiate(CoinPrefab);

                // Enure it spawns in the correct position
                coin.transform.SetParent(go.transform, false);
            }
        }
    }
}
