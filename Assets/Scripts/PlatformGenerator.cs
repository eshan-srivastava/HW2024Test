using UnityEngine;
// using UnityEngine.Pool;
using System.Collections.Generic;

public class PlatformGenerator : MonoBehaviour
{
    //Platform generator should only be concerned with generating a platform at a position
    //The task of finding the position for next platform can be delegated to another class
    
    //public GameObject pulpitPrefab;
    // [SerializeField] private Pulpit pulpitPrefab;
    
    private float _generationInterval = 2.2f;
    private float _platformDurationMin = 4.1f;
    private float _platformDurationMax = 5.1f;
    public float platformDuration;

    [SerializeField] private Vector3 lastGeneratedPosition;
    public GameObject lastPlatform;

    private PlatformSpawnLogic _platformSpawnLogic;

    // public Vector3[] potentialEdgeDirections;
    [SerializeField] private PulpitPool pulpitPool;
    public void SetPulpitData(PulpitData pulpitData){
        _generationInterval = pulpitData.pulpit_spawn_time;
        _platformDurationMin = pulpitData.min_pulpit_destroy_time;
        _platformDurationMax = pulpitData.max_pulpit_destroy_time;
    }
    
    private void Start()
    {      
        _platformSpawnLogic = new PlatformSpawnLogic();
        
        lastGeneratedPosition = lastPlatform.transform.position;   
        InvokeRepeating(nameof(SpawnPulpit), _generationInterval, _generationInterval);
        //StartCoroutine(GeneratePlatform());
    }
    
    public void SpawnPulpit()
    {
        //Debug.Log("SpawnPulpit called");
        platformDuration = UnityEngine.Random.Range(_platformDurationMin, _platformDurationMax);

        // Vector3 nextSpawnPoint = NextRandomSpawnPoint(lastPlatform);
        Vector3 nextSpawnPoint = _platformSpawnLogic.NextRandomSpawnPoint(lastPlatform, transform);

        // GameObject newPlatform = Instantiate(pulpitPrefab, nextSpawnPoint, Quaternion.identity);
        GameObject newPlatform = pulpitPool.GetPooledObject();
        newPlatform.transform.position = nextSpawnPoint;
        newPlatform.transform.rotation = Quaternion.identity;
        newPlatform.SetActive(true);

        //assign pulpit scripts its components
        //newPlatform.AddComponent<Pulpit>();
        newPlatform.GetComponent<Pulpit>().pulpitPool = pulpitPool;
        newPlatform.GetComponent<Pulpit>().startingNumber = platformDuration;
        newPlatform.layer = 3;
        
        lastPlatform = newPlatform;
        lastGeneratedPosition = nextSpawnPoint;
        
        // Debug.Log("Platform generated at: " + lastGeneratedPosition);
        //yield return new WaitForSeconds(generationInterval);
    }
}
