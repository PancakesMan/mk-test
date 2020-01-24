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
    public CoinSpawnInfo[] CoinSpawns;  // Available places to spawn it
    //public float CoinSpawnChance = 0.5f;  // Chance of it spawning at each point

    public void SpawnNewCoins()
    {
        DespawnExistingCoins();

        // For each spawn point available
        foreach (CoinSpawnInfo info in CoinSpawns)
        {
            // Determine if we will spawn a coin here
            if (Random.Range(0.0f, 1.0f) < info.CoinSpawnChance)
            {
                // Instantiate the coin object
                info.CoinInstance = Instantiate(CoinPrefab);

                // Ensure it spawns in the correct position
                info.CoinInstance.transform.SetParent(info.CoinSpawnPoint.transform, false);
            }
        }
    }

    public void DespawnExistingCoins()
    {
        // For each spawn point available
        foreach (CoinSpawnInfo info in CoinSpawns)
        {
            // Delete coins if they haven't been collected
            if (info.CoinInstance)
            {
                Destroy(info.CoinInstance);
            }
        }
    }
}
