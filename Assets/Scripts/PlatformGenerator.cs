using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{

    public GameObject pulpitPrefab;
    public float generationInterval;
    public float platformDurationMin;
    public float platformDurationMax;
    public float platformDuration;

    [SerializeField] Vector3 lastGeneratedPosition;
    public GameObject lastPlatform;
    public Vector3[] potentialEdgeDirections;
    bool dataLoaded = false;
    // Start is called before the first frame update
    void Start()
    {
        JsonLoader jsonLoader = JsonLoader.Instance;
        if (!lastPlatform) lastPlatform = GameObject.Find("starterpulpit");
        // Ensure JsonLoader is not null before proceeding
        if (!dataLoaded && jsonLoader != null)
        {
            // Start a coroutine to wait for data to be loaded
            StartCoroutine(WaitForDataLoaded(jsonLoader));
            dataLoaded = true;
        }
        else
        {
            Debug.LogError("JsonLoader instance is null.");
        }
        
        lastGeneratedPosition = lastPlatform.transform.position;
        
        InvokeRepeating(nameof(SpawnPulpit), generationInterval, generationInterval);
        //StartCoroutine(GeneratePlatform());
    }
    private IEnumerator WaitForDataLoaded(JsonLoader jsonLoader)
    {
        // Trigger JSON loading
        yield return StartCoroutine(jsonLoader.LoadJsonCoroutine());

        // Access the loaded data
        MyDataClass loadedData = jsonLoader.GetLoadedData();

        // Ensure loaded data is not null before using it
        if (loadedData != null)
        {
            // Access and use the data as needed
            Debug.Log("Min Pulpit Destroy: " + loadedData.pulpit_data.min_pulpit_destroy_time);
            generationInterval = loadedData.pulpit_data.pulpit_spawn_time;
            platformDurationMin = loadedData.pulpit_data.min_pulpit_destroy_time;
            platformDurationMax = loadedData.pulpit_data.max_pulpit_destroy_time;

            Start();
        }
        else
        {
            Debug.LogError("Loaded data is null.");
        }
    }
    
    public void SpawnPulpit()
    {
        //Debug.Log("SpawnPulpit called");
        platformDuration = UnityEngine.Random.Range(platformDurationMin, platformDurationMax);
        Vector3 nextSpawnPoint = GetRandomEdge(lastPlatform);
        GameObject newPlatform = Instantiate(pulpitPrefab, nextSpawnPoint, Quaternion.identity);
        //newPlatform.AddComponent<Pulpit>();
        newPlatform.GetComponent<Pulpit>().startingNumber = platformDuration;
        //Destroy(newPlatform, platformDuration);
        lastPlatform = newPlatform;
        lastGeneratedPosition = nextSpawnPoint;
        //Destroy(newPlatform, platformDuration);
        Debug.Log("Platform generated at: " + lastGeneratedPosition);
        //yield return new WaitForSeconds(generationInterval);

    }
    Vector3 GetRandomEdge(GameObject platform)
    {
        if (platform == null)
        {
            // If there is no previous platform, return the current position of the generator
            Debug.Log("no platform");
            return transform.position;
        }
        
        Vector3 spawnPosition = Vector3.zero;
        bool foundSpawnPoint = false;
        // Get the collider of the previous platform
        Collider collider = platform.GetComponent<Collider>();
        //BoxCollider collider.size = new Vector3(9, 0.1f, 9);
        if (collider == null)
        {
            // If there is no collider, return the current position of the generator
            Debug.Log("no collider");
            return transform.position;
        }
        Vector3[] potentialEdgeDirections = new Vector3[] {
                new (1, 0, 0),  // Right edge
                new (-1, 0, 0), // Left edge
                new (0, 0, 1),  // Forward edge
                new (0, 0, -1)  // Backward edge
            };
        Vector3[] shuffledEdges = potentialEdgeDirections;
        Shuffle(shuffledEdges);
        
        // Randomly choose one of the four edges
        foreach (Vector3 edgeDirection in shuffledEdges)
        {
            // Calculate a potential spawn position along the edge
            spawnPosition = lastPlatform.transform.position + (edgeDirection * collider.bounds.extents.x);
            //Debug.Log("Trying position : " + spawnPosition);

            // Check for collisions using Physics.CheckSphere:
            if (!Physics.CheckSphere(spawnPosition, collider.bounds.extents.x))
            {
                foundSpawnPoint = true;
                break; // Exit the loop as soon as a suitable edge is found
            }
        }

        if (!foundSpawnPoint)
        {
            //return RandomPositionWithinBounds(spawnPosition);
            //return spawnPosition;
            return NextPosition(spawnPosition);
        }
        // Default: return the current position of the generator
        return transform.position;
    }
    Vector3 NextPosition(Vector3 spawnPosition)
    {
        float platformHalfWidth = 9f * 0.5f;
        if (spawnPosition.x > 0)
        {
            spawnPosition.x = spawnPosition.x + platformHalfWidth;
        }
        else if (spawnPosition.x < 0)
        {
            spawnPosition.x = spawnPosition.x - platformHalfWidth;
        }
        else if (spawnPosition.z > 0)
        {
            spawnPosition.z = spawnPosition.z + platformHalfWidth;
        }
        else if (spawnPosition.z < 0)
        {
            spawnPosition.z = spawnPosition.z - platformHalfWidth;
        }
        return spawnPosition;
    }
    void Shuffle(Vector3[] array)
    {
        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = UnityEngine.Random.Range(1, n);
            Vector3 t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
