using UnityEngine;
// using UnityEngine.Pool;
using System.Collections.Generic;
using Zenject;

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

    [Inject]
    private PlatformSpawnLogic _platformSpawnLogic;

    // public Vector3[] potentialEdgeDirections;
    [Inject]
    private PulpitPool _pulpitPool;
    public void SetPulpitData(PulpitData pulpitData){
        _generationInterval = pulpitData.pulpit_spawn_time;
        _platformDurationMin = pulpitData.min_pulpit_destroy_time;
        _platformDurationMax = pulpitData.max_pulpit_destroy_time;
    }
    
    private void Start()
    {      
        // DI'd
        // _platformSpawnLogic = new PlatformSpawnLogic();
        
        lastGeneratedPosition = lastPlatform.transform.position;   
        InvokeRepeating(nameof(SpawnPulpit), _generationInterval, _generationInterval);
        //StartCoroutine(GeneratePlatform());
    }
    
    public void SpawnPulpit()
    {
        //Debug.Log("SpawnPulpit called");
        platformDuration = UnityEngine.Random.Range(_platformDurationMin, _platformDurationMax);

        Vector3 nextSpawnPoint = _platformSpawnLogic.NextRandomSpawnPoint(lastPlatform, transform);

        // GameObject newPlatform = Instantiate(pulpitPrefab, nextSpawnPoint, Quaternion.identity);
        GameObject newPlatform = _pulpitPool.GetPooledObject();
        newPlatform.transform.position = nextSpawnPoint;
        newPlatform.transform.rotation = Quaternion.identity;
        newPlatform.SetActive(true);

        //assign pulpit scripts its components
        // newPlatform.GetComponent<Pulpit>().pulpitPool = pulpitPool;
        newPlatform.GetComponent<Pulpit>().startingNumber = platformDuration;
        // newPlatform.layer = 3;
        
        lastPlatform = newPlatform;
        lastGeneratedPosition = nextSpawnPoint;
        
        // Debug.Log("Platform generated at: " + lastGeneratedPosition);
        //yield return new WaitForSeconds(generationInterval);
    }
}
