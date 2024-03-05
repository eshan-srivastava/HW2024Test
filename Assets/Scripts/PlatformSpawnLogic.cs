using System;
using UnityEngine;
using Util;

public class PlatformSpawnLogic
{
    private const float Delta = 0.04f;
    private const float Tolerance = 1f;
    
    public Vector3 NextRandomSpawnPoint(GameObject platform, Vector3 lastEdgeDir){
        if (platform == null)
        {
            // If there is no previous platform, return the current position of the generator
            Debug.Log("no platform, returning origin");
            return Vector3.zero;
        }
        Vector3 spawnEdge = Vector3.zero;
        Vector3 edgeDir = Vector3.zero;

        float halfWidth = 4.5f;
        
        bool foundSpawnPoint = false;
        Vector3[] shuffledEdges = GetShuffledEdges(platform);

        // Randomly choose one of the four edges
        foreach (Vector3 edgeDirection in shuffledEdges)
        {
            // Calculate a potential spawn position along the edge
            
            spawnEdge = platform.transform.position + (edgeDirection * halfWidth);
            edgeDir = edgeDirection * 2 * halfWidth;
            
            // Check for collisions using Physics.CheckSphere, also platform position cannot be same as previous to previous platform
            
            if (!Physics.CheckSphere(position:2*spawnEdge, radius:halfWidth - Delta, layerMask: 0) && edgeDir + lastEdgeDir != Vector3.zero)
            {
                Debug.Log($"Trying curr position : {spawnEdge} with curr edge as {edgeDir} and last edge dir as {lastEdgeDir}");
                foundSpawnPoint = true;
                break; // Exit the loop as soon as a suitable edge is found
            }
        }

        if (foundSpawnPoint)
        {
            Vector3 nextSpawnPoint = NextPosition(spawnEdge, platform.transform.position);
            
            //check for when the nextSpawnPoint has both x and z different from previous
            if (Math.Abs(nextSpawnPoint.x - platform.transform.position.x) > Tolerance &&
                Math.Abs(nextSpawnPoint.z - platform.transform.position.z) > Tolerance)
            {
                Console.WriteLine("Diagonal block generated");
            }
            
            return nextSpawnPoint;
        }
        // Default: return origin, this is triggered when a spawn point is not found
        Debug.Log("Default case of platform spawn triggered");
        return Vector3.zero;
    }
    
    Vector3 NextPosition(Vector3 spawnPosition, Vector3 platformPosition)
    {
        const float platformHalfWidth = 9f * 0.5f;
        //check which component of Vector3 has changed
        //if new x is above -> add 4.5 if new x is negative -> - 4.5
        spawnPosition = spawnPosition - platformPosition;
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
        
        // Debug.Log("Platform generated at displacement: " + spawnPosition);
        
        //adding platform disp again to spawn it relative to current platform
        // spawnPosition += platformPosition; moved to main script as highlighted there
        
        return spawnPosition;
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
        Shuffle(ref shuffledEdges);
        return shuffledEdges;
    }
    private static void Shuffle(ref Vector3[] array)
    {
        Shuffle<Vector3>.ShuffleArray(ref array);
    }
}

/*
// Get the collider of the previous platform
        Collider collider = platform.GetComponent<Collider>();
        var halfWidth = collider.bounds.extents.x;
        //BoxCollider collider.size = new Vector3(9, 0.1f, 9);
        if (collider == null)
        {
            // If there is no collider, return the current position of the generator
            Debug.Log("no collider, returning origin");
            return Vector3.zero;
        }
*/