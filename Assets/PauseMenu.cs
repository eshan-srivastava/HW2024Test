using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    [SerializeField] GameObject PauseMenuUI;
    //public static bool PlayerPaused = false;
    public void Resume()
    {
        //Public to attach it to resume button
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }
    public void LoadHome()
    {
        Debug.Log("Load home hit from PauseMenu");
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
        GamePaused = false;
    }
    public void QuitGame()
    {
        Debug.Log("Quit game hit from PauseMenu");
        Application.Quit();
    }
    private void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume(); //if game was already paused, resume it
            }
            else
            {
                Pause();
            }
        }
    }
}
