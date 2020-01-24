using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPool
    {
        public GameObject Prefab;
        public int PoolSize;

        private Queue<GameObject> Pool;

        public void Setup()
        {
            Pool = new Queue<GameObject>(PoolSize);
            for (int i = 0; i < PoolSize; i++)
            {
                // Instatiate new platform
                GameObject obj = Instantiate(Prefab);

                // Move the newly instantiated object off-screen
                // Cheaper than calling ResetPlatform immediately upon creation
                obj.transform.position = new Vector3(-100f, -100f);

                // Get reference to PlatformController script
                PlatformController controller = obj.GetComponent<PlatformController>();

                // When object is disabled, return it to the queue automatically
                controller.OnDisabled.AddListener(Return);

                // Add the spawned platform to the queue
                Pool.Enqueue(obj);
            }
        }

        public GameObject Get()
        {
            // Ensure there are are unused objects in the pool
            if (Pool.Count > 0)
                return Pool.Dequeue();
            else
                return null;
        }

        public void Return(GameObject obj)
        {
            Pool.Enqueue(obj);
        }
    }

    public ObjectPool[] ObjectPools; // List of platforms that can be spawned

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
        if (ObjectPools.Length == 0)
        {
            // Disable the script
            enabled = false;
            return;
        }

        // Initialize the Object Pools
        for (int i = 0; i < ObjectPools.Length; i++)
        {
            ObjectPools[i].Setup();
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
            // Spawn a platform from the pool
            if (TrySpawnPlatform())
            {
                // Reset the timer
                timer = 0.0f;

                // Pick a new delay time
                SpawnDelay = Random.Range(SpawnDelayMin, SpawnDelayMax);
            }
        }
    }

    public bool TrySpawnPlatform()
    {
        // Attempt to get a random platform
        GameObject go = ObjectPools[Random.Range(0, ObjectPools.Length)].Get();

        // If there are no objects left in the pool, return false
        if (go == null)
        {
            return false;
        }

        PlatformController controller = go.GetComponent<PlatformController>();

        // Reset the platform
        controller.ResetPlatform();

        // Give it a random position
        controller.transform.position = new Vector2(SpawnOrigin.x, SpawnOrigin.y + Random.Range(VerticalRandomFactorMin, VerticalRandomFactorMax));

        // Ensure platform falls between Min and Max vertical position
        if (controller.transform.position.y < SpawnVerticalMin)
        {
            controller.transform.position = new Vector2(controller.transform.position.x, SpawnVerticalMin);
        }
        if (controller.transform.position.y > SpawnVerticalMax)
        {
            controller.transform.position = new Vector2(controller.transform.position.x, SpawnVerticalMax);
        }

        // Update the SpawnOrigin for the next platform
        SpawnOrigin = controller.EndPosition.position;

        // Return success
        return true;
    }
}
