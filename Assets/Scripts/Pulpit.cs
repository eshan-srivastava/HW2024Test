using System;
using TMPro;
using UnityEngine;
using Zenject;

public class Pulpit : MonoBehaviour
{
    public float startingNumber = 5;
    // public PulpitPool pulpitPool;
    private PulpitPool _pulpitPool;

    private bool _hasIncreasedScoreOnce;
    //[SerializeField] float decreaseRate = 0.003f;

    private float _currentNumber;
    private TextMeshPro _tileTime;
    public Vector3[] spawnPoints;
    // if singletons are allowed then pulpit pool should be singleton so that it can be accessed from multiple files

    private void OnEnable()
    {
        _hasIncreasedScoreOnce = false;
        _tileTime = GetComponentInChildren<TextMeshPro>();
        _currentNumber = startingNumber;
        // _pulpitPool = pulpitPool;
        UpdateText();
    }

    [Inject]
    public void Construct(PulpitPool pulpitPool)
    {
        _pulpitPool = pulpitPool;
        this.gameObject.layer = 3;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasIncreasedScoreOnce)
        {
            return;
        }
        //inc score by 1
        PlayerStats.score++;
        _hasIncreasedScoreOnce = true;
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
            // pulpitPool.ReturnToPool(gameObject);
            _pulpitPool.ReturnToPool(gameObject);
            return;
        }
    }
    
    public class Factory : PlaceholderFactory<Pulpit> { }
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