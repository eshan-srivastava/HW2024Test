using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    private TextMeshPro score;
    public int currscore = 0;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + currscore;
    }
}
