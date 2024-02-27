using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulpitPool : MonoBehaviour
{
    public GameObject pulpitPrefab;

    // private IObjectPool<GameObject> _pulpitPool;
    private Queue<GameObject> _pulpitPool;
    private int _amountToPool;

    private void Awake()
    {
        _pulpitPool = new Queue<GameObject>();
        _amountToPool = 3;
    }
    
    void Start()
    {
        //prewarm the pool
        AddPulpits(_amountToPool);
    }
    private void AddPulpits(int count)
    {
        for (var i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(pulpitPrefab);
            obj.SetActive(false);
            _pulpitPool.Enqueue(obj);
        }
    }
    public GameObject GetPooledObject()
    {
        if (_pulpitPool.Count == 0)
        {
            AddPulpits(1);
        }
        return _pulpitPool.Dequeue();
    }
    public void ReturnToPool(GameObject pulpitToReturn)
    {
        pulpitToReturn.SetActive(false);
        _pulpitPool.Enqueue(pulpitToReturn);
    }
}

/*
     * can make this a singleton because:
     * 1. Need to call instantiate method so needs to be MonoBehavior
     * 2. MonoBehavior cross communication needs singleton
     */
    
//public static PulpitPool Instance { get; private set; }
