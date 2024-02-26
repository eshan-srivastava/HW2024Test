using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject pulpitPrefab;
    private float generationInterval = 2.2f;
    private float platformDurationMin = 4.1f;
    private float platformDurationMax = 5.1f;
    public float platformDuration;

    [SerializeField] Vector3 lastGeneratedPosition;
    public GameObject lastPlatform;
    // public Vector3[] potentialEdgeDirections;
    // Start is called before the first frame update    
    public void SetPulpitData(PulpitData pulpitData){
        generationInterval = pulpitData.pulpit_spawn_time;
        platformDurationMin = pulpitData.min_pulpit_destroy_time;
        platformDurationMax = pulpitData.max_pulpit_destroy_time;
    }
    void Start()
    {      
        lastGeneratedPosition = lastPlatform.transform.position;   
        InvokeRepeating(nameof(SpawnPulpit), generationInterval, generationInterval);
        //StartCoroutine(GeneratePlatform());
    }
    
    public void SpawnPulpit()
    {
        //Debug.Log("SpawnPulpit called");
        platformDuration = UnityEngine.Random.Range(platformDurationMin, platformDurationMax);
        Vector3 nextSpawnPoint = NextRandomSpawnPoint(lastPlatform);
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
    Vector3 NextRandomSpawnPoint(GameObject platform){
        if (platform == null)
        {
            // If there is no previous platform, return the current position of the generator
            Debug.Log("no platform");
            return transform.position;
        }
        Vector3 spawnPosition = Vector3.zero;
        // Get the collider of the previous platform
        Collider collider = platform.GetComponent<Collider>();
        //BoxCollider collider.size = new Vector3(9, 0.1f, 9);
        if (collider == null)
        {
            // If there is no collider, return the current position of the generator
            Debug.Log("no collider");
            return transform.position;
        }
        
        bool foundSpawnPoint = false;
        Vector3[] shuffledEdges = GetShuffledEdges(platform);


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
    Vector3[] GetShuffledEdges(GameObject platform)
    {
        Vector3[] potentialEdgeDirections = new Vector3[] {
                new (1, 0, 0),  // Right edge
                new (-1, 0, 0), // Left edge
                new (0, 0, 1),  // Forward edge
                new (0, 0, -1)  // Backward edge
            };
        Vector3[] shuffledEdges = potentialEdgeDirections;
        Shuffle(shuffledEdges);
        return shuffledEdges;
    }
    Vector3 NextPosition(Vector3 spawnPosition)
    {
        float platformHalfWidth = 9f * 0.5f;
        if (spawnPosition.x > 0)
        {
            spawnPosition.x += platformHalfWidth;
        }
        else if (spawnPosition.x < 0)
        {
            spawnPosition.x -= platformHalfWidth;
        }
        else if (spawnPosition.z > 0)
        {
            spawnPosition.z += platformHalfWidth;
        }
        else if (spawnPosition.z < 0)
        {
            spawnPosition.z -= platformHalfWidth;
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
}
