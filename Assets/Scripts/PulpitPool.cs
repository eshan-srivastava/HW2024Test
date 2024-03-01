using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PulpitPool : IInitializable, IDisposable
{
    private GameObject _pulpitPrefab;
    private Pulpit.Factory _pulpitFactory;

    // private IObjectPool<GameObject> _pulpitPool;
    private Queue<GameObject> _pulpitPool;
    private int _amountToPool;
    
    [Inject]
    public PulpitPool(GameObject pulpitPrefab, Pulpit.Factory pulpitFactory)
    {
        this._pulpitPrefab = pulpitPrefab;
        this._pulpitFactory = pulpitFactory;
    }
    public void Initialize()
    {
        _pulpitPool = new Queue<GameObject>();
        _amountToPool = 2;
        //prewarm the pool
        AddPulpits(_amountToPool);
    }
    public void Dispose()
    {
        _pulpitPool.Clear();
    }
    private void AddPulpits(int count)
    {
        for (var i = 0; i < count; i++)
        {
            // GameObject obj = Instantiate(pulpitPrefab);
            // obj.GetComponent<Pulpit>().pulpitPool = this;
            // GameObject obj = Pulpit.Factory.Create();
            Pulpit obj = _pulpitFactory.Create();
            
            obj.gameObject.SetActive(false);
            _pulpitPool.Enqueue(obj.gameObject);
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
