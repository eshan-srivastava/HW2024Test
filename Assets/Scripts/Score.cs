using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    
    void Update()
    {
        scoreText.text = "Score: " + PlayerStats.score;
    }
}
