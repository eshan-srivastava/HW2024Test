using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneFader sceneFader;
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //in build settings, the game scene is next after menu
        sceneFader.FadeTo("MainLevel");
    }
    public void QuitGame()
    {
        Debug.Log("Game Quit pressed");
        Application.Quit();
    }
}
