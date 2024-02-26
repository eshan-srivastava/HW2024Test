using TMPro;
using UnityEngine;

public class Pulpit : MonoBehaviour
{
    public float startingNumber = 5;
    [SerializeField] float decreaseRate = 0.003f;

    private float currentNumber;
    private TextMeshPro TileTime;
    public Vector3[] spawnPoints;
    Score scoreInstance;
    // Start is called before the first frame update
    void Start()
    {
        InitializeSpawnPoints();
        TileTime = GetComponentInChildren<TextMeshPro>();
        currentNumber = startingNumber;
        scoreInstance = GameObject.FindObjectOfType<Score>();
        UpdateText();

    }
    void InitializeSpawnPoints()
    {
        spawnPoints = new Vector3[4];

        for (int i = 0; i < 4; i++)
        {
            // Store the position of each child in the spawnPoints array
            spawnPoints[i] = transform.GetChild(i).position;
        }
        //Debug.Log(spawnPoints[0]);
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    void UpdateText()
    {
        // Update the text component to display the current number, F2 format for 2 decimal places
        TileTime.text = currentNumber.ToString("F2");
    }
    // Update is called once per frame
    void Update()
    {
        currentNumber -= decreaseRate;

        // Update the text component to display the current number
        UpdateText();

        if (currentNumber <= 0f)
        {
            //platformGenerator.SpawnPulpit(gameObject, spawnPoints);
            Destroy(gameObject);
        }
    }
}
