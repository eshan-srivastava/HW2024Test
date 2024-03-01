using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;
    
    public void Retry()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        sceneFader.FadeTo("MainMenu");
    }
}
