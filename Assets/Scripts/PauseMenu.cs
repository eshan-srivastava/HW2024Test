using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;

    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] private SceneFader sceneFader;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        PauseMenuUI.SetActive(!PauseMenuUI.activeSelf);

        if (PauseMenuUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        Debug.Log("Load home hit from PauseMenu");
        TogglePause();
        Time.timeScale = 1f;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        TogglePause();
        Time.timeScale = 1f;
        // SceneManager.LoadScene("Menu");
        sceneFader.FadeTo("Menu");
    }
    public void Quit()
    {
        Debug.Log("Quit game hit from PauseMenu");
        Application.Quit();
    }
}
