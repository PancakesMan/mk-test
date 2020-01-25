using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [System.Serializable]
    public class CoinSpawnInfo
    {
        public GameObject CoinSpawnPoint;
        public GameObject CoinInstance;
        public float CoinSpawnChance;
    }

    public GameObject CoinPrefab;         // Prefab to spawn
    public CoinSpawnInfo[] CoinSpawns;    // Available places to spawn it

    public void SpawnNewCoins()
    {
        DespawnExistingCoins();

        // For each spawn point available
        foreach (CoinSpawnInfo info in CoinSpawns)
        {
            // Determine if we will spawn a coin here
            if (Random.Range(0.0f, 1.0f) < info.CoinSpawnChance)
            {
                // If one hasn't been created yet
                if (!info.CoinInstance)
                {
                    // Create a new coin
                    info.CoinInstance = Instantiate(CoinPrefab);
                    // Ensure it spawns in the correct position
                    info.CoinInstance.transform.SetParent(info.CoinSpawnPoint.transform, false);
                }
                else
                {
                    // Enable the coin instance that has already been created
                    info.CoinInstance.SetActive(true);
                }
            }
        }
    }

    public void DespawnExistingCoins()
    {
        // For each spawn point available
        foreach (CoinSpawnInfo info in CoinSpawns)
        {
            // If a coin has already been created
            if (info.CoinInstance)
            {
                // Disable it
                info.CoinInstance.SetActive(false);
            }
        }
    }
}
