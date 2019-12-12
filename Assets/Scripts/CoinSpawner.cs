using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject CoinPrefab;
    public GameObject[] CoinSpawnPoints;
    public float CoinSpawnChance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in CoinSpawnPoints)
        {
            if (Random.Range(0.0f, 1.0f) < CoinSpawnChance)
            {
                GameObject coin = Instantiate(CoinPrefab);
                coin.transform.SetParent(go.transform, false);
            }
        }
    }
}
