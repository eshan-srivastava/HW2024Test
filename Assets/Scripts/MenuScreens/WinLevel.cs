using UnityEngine;

public class WinLevel : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private string menuSceneName = "MainMenu";
    
    public void Menu()
    {
        PlayerPrefs.SetInt("LevelReached", 2);
        sceneFader.FadeTo(menuSceneName);
    }
}
