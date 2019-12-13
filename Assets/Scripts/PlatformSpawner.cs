using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] Prefabs;

    [Header("Spawn Settings")]
    public int SpawnVerticalMax;
    public int SpawnVerticalMin;
    public int HorizontalMax;
    public int VerticalMax;
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
        SpawnDelay = Random.Range(SpawnDelayMin, SpawnDelayMax);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > SpawnDelay)
        {
            GameObject go = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)]);
            go.transform.position = new Vector2(transform.position.x, Random.Range(SpawnVerticalMin, SpawnVerticalMax));
            //SpawnOrigin = go.transform.position;

            timer = 0.0f;
            //SpawnDelay = go.GetComponent<PlatformController>().FallTimer * 0.5f;
            SpawnDelay = Random.Range(SpawnDelayMin, SpawnDelayMax);
        }
    }
}
