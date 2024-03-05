using UnityEngine;
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

    private Vector3[] _positions;

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
        _positions = new Vector3[2];
        for (int i=0; i<2; i++)
        {
            _positions[i] = Vector3.zero;
        }
        lastGeneratedPosition = lastPlatform.transform.position;   
        InvokeRepeating(nameof(SpawnPulpit), _generationInterval, _generationInterval);
        //StartCoroutine(GeneratePlatform());
    }
    
    public void SpawnPulpit()
    {
        platformDuration = UnityEngine.Random.Range(_platformDurationMin, _platformDurationMax);

        // Vector3 nextSpawnPoint = _platformSpawnLogic.NextRandomSpawnPoint(lastPlatform, transform, _pulpitPool.PeekBack().transform.position);

        //updated this function to return next spawn point relative to current platform, helps in 
        Vector3 nextSpawnPoint = _platformSpawnLogic.NextRandomSpawnPoint(lastPlatform, _positions[1]);

        AddPosition(nextSpawnPoint);

        nextSpawnPoint += lastPlatform.transform.position;
        // GameObject newPlatform = Instantiate(pulpitPrefab, nextSpawnPoint, Quaternion.identity);
        GameObject newPlatform = _pulpitPool.GetPooledObject();
        newPlatform.transform.position = nextSpawnPoint;
        newPlatform.transform.rotation = Quaternion.identity;
        newPlatform.SetActive(true);

        newPlatform.GetComponent<Pulpit>().startingNumber = platformDuration;
        //these are handled by default for any new pulpit that spawns
        // newPlatform.GetComponent<Pulpit>().pulpitPool = pulpitPool;
        // newPlatform.layer = 3;

        lastPlatform = newPlatform;

        //yield return new WaitForSeconds(generationInterval);
    }

    private void AddPosition(Vector3 newestPosition)
    {
        _positions[0] = _positions[1];
        _positions[1] = newestPosition;
    }
}
