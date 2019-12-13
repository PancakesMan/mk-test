using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] Prefabs;

    [Header("Spawn Settings")]
    public float VerticalRandomFactorMax;
    public float VerticalRandomFactorMin;
    public float SpawnVerticalMax;
    public float SpawnVerticalMin;
    public float SpawnDelayMax;
    public float SpawnDelayMin;

    private float SpawnDelay;
    private float timer = 0.0f;
    private Vector2 SpawnOrigin;

    void Awake()
    {
        if (Prefabs.Length == 0)
        {
            enabled = false;
        }

        SpawnOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > SpawnDelay)
        {
            GameObject go = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)]);
            go.transform.position = new Vector2(SpawnOrigin.x, SpawnOrigin.y + Random.Range(VerticalRandomFactorMin, VerticalRandomFactorMax));
            if (go.transform.position.y < SpawnVerticalMin)
            {
                go.transform.position = new Vector2(go.transform.position.x, SpawnVerticalMin);
            }
            if (go.transform.position.y > SpawnVerticalMax)
            {
                go.transform.position = new Vector2(go.transform.position.x, SpawnVerticalMax);
            }
            SpawnOrigin = go.GetComponent<PlatformController>().EndPosition.position;

            timer = 0.0f;
            SpawnDelay = Random.Range(SpawnDelayMin, SpawnDelayMax);
        }
    }
}
