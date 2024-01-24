using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public float gameOverThreshold;

    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerController.transform.position.y < -3)
        {
            StopGame();
            ShowGameOverScreen();
        }
    }

    void StopGame()
    {
        Time.timeScale = 0f;
        playerController.enabled = false;
        // Stop other game elements (audio, etc.)
    }

    void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        // ... (other actions for displaying game over)
    }
}