using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int score;
    public int startScore = 0;
    
    void Start()
    {
        score = startScore;
    }
}
