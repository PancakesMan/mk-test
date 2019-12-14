using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] Prefabs; // List of platforms that can be spawned

    [Header("Spawn Settings")]
    public float VerticalRandomFactorMax; // Max position platform is randomly placed relative to the previous
    public float VerticalRandomFactorMin; // Min position platform is randomly placed relative to the previous
    public float SpawnVerticalMax; // Max vertical position of the new platform
    public float SpawnVerticalMin; // Min vertical position of the new platform
    public float SpawnDelayMax; // Max delay before next platform is spawned
    public float SpawnDelayMin; // Min delay before next platform is spawned

    private float SpawnDelay;    // Stores the current delay until next platform spawns
    private float timer = 0.0f;  // Internal timer used to spawn platforms
    private Vector2 SpawnOrigin; // Origin position platforms are spawned relative to

    void Awake()
    {
        // If there are no prefabs to spawn
        if (Prefabs.Length == 0)
        {
            // Disable the script
            enabled = false;
        }

        // Default SpawnOrigin to the position of the current object
        SpawnOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // If we have passed the time delay to spawn a new platform
        if (timer > SpawnDelay)
        {
            // Pick a random platform
            GameObject go = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)]);

            // Give it a random position
            go.transform.position = new Vector2(SpawnOrigin.x, SpawnOrigin.y + Random.Range(VerticalRandomFactorMin, VerticalRandomFactorMax));

            // Ensure platform falls between Min and Max vertical position
            if (go.transform.position.y < SpawnVerticalMin)
            {
                go.transform.position = new Vector2(go.transform.position.x, SpawnVerticalMin);
            }
            if (go.transform.position.y > SpawnVerticalMax)
            {
                go.transform.position = new Vector2(go.transform.position.x, SpawnVerticalMax);
            }

            // Update the SpawnOrigin for the next platform
            SpawnOrigin = go.GetComponent<PlatformController>().EndPosition.position;

            // Reset the timer
            timer = 0.0f;

            // Pick a new delay time
            SpawnDelay = Random.Range(SpawnDelayMin, SpawnDelayMax);
        }
    }
}
