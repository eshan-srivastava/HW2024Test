using System;
using TMPro;
using UnityEngine;

public class Pulpit : MonoBehaviour
{
    public float startingNumber = 5;
    public PulpitPool pulpitPool;
    //[SerializeField] float decreaseRate = 0.003f;

    private float _currentNumber;
    private TextMeshPro _tileTime;
    public Vector3[] spawnPoints;
    //public Score scoreInstance;
    private PulpitPool _pulpitPool;
// if singletons are allowed then pulpit pool should be singleton so that it can be accessed from multiple files
    
    void Start()
    {
    }

    private void OnEnable()
    {
        _tileTime = GetComponentInChildren<TextMeshPro>();
        _currentNumber = startingNumber;
        _pulpitPool = pulpitPool;
        UpdateText();
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
    void UpdateText()
    {
        // Update the text component to display the current number, F2 format for 2 decimal places
        _tileTime.text = _currentNumber.ToString("F2");
    }
    // Update is called once per frame
    void Update()
    {
        if (_currentNumber > 0f)
        { 
            _currentNumber -= Time.deltaTime;
            // Update the text component to display the current number
            UpdateText();
        }
        else
        {
            //Destroy(gameObject);
            pulpitPool.ReturnToPool(gameObject);
            return;
        }
    }
}

// void InitializeSpawnPoints()
// {
//     spawnPoints = new Vector3[4];
//
//     for (int i = 0; i < 4; i++)
//     {
//         // Store the position of each child in the spawnPoints array
//         spawnPoints[i] = transform.GetChild(i).position;
//     }
//     //Debug.Log(spawnPoints[0]);
// }