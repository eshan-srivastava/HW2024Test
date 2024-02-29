using System;
using UnityEngine;

public class PlatformSpawnLogic
{
    private const float Delta = 0.04f;
    private const float Tolerance = 1f;

    
    public Vector3 NextRandomSpawnPoint(GameObject platform, Transform generator){
        if (platform == null)
        {
            // If there is no previous platform, return the current position of the generator
            Debug.Log("no platform, returning origin");
            return generator.position;
        }
        Vector3 spawnEdge = Vector3.zero;
        // Get the collider of the previous platform
        Collider collider = platform.GetComponent<Collider>();
        var halfWidth = collider.bounds.extents.x;
        //BoxCollider collider.size = new Vector3(9, 0.1f, 9);
        if (collider == null)
        {
            // If there is no collider, return the current position of the generator
            Debug.Log("no collider, returning origin");
            return generator.position;
        }
        
        bool foundSpawnPoint = false;
        Vector3[] shuffledEdges = GetShuffledEdges(platform);

        // Randomly choose one of the four edges
        foreach (Vector3 edgeDirection in shuffledEdges)
        {
            // Calculate a potential spawn position along the edge
            
            // spawnPosition = lastPlatform.transform.position + (edgeDirection * collider.bounds.extents.x);
            // spawnPosition = platform.transform.position + (edgeDirection * collider.bounds.extents.x);
            spawnEdge = platform.transform.position + (edgeDirection * halfWidth);

            //Debug.Log("Trying position : " + spawnPosition);
            
            // Check for collisions using Physics.CheckSphere, also platform position cannot be same as previous to previous platform
            //TODO: TO MAKE NEW PLATFORM NOT HAVE POSITION SAME AS LAST TO LAST PLATFORM, NEED TO MAKE OBJECT POOL A DE-QUE
            if (!Physics.CheckSphere(position:2*spawnEdge, radius:halfWidth - Delta, layerMask: 0))
            {
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
        // Default: return the current position of the generator
        // this is triggered when a spawn point is not found
        Debug.Log("Default case of platform spawn triggered");
        // Debug.Log();
        return generator.position;
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
    Vector3 NextPosition(Vector3 spawnPosition, Vector3 platformPosition)
    {
        const float platformHalfWidth = 9f * 0.5f;
        //change logic, check which component of Vector3 has changed
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
        
        Debug.Log("Platform generated at: " + spawnPosition);
        //adding platform position again to spawn it relative to current platform
        spawnPosition += platformPosition;
        
        return spawnPosition;
    }
    private static void Shuffle(ref Vector3[] array)
    {
        var p = array.Length;
        for (var n = p - 1; n >= 0; n--)
        {
            var r = UnityEngine.Random.Range(0, n);
            (array[r], array[n]) = (array[n], array[r]);
        }
    }
}
