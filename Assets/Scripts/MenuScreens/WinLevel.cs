using UnityEngine;

namespace MenuScreens
{
    public class WinLevel : MonoBehaviour
    {
        [SerializeField] private SceneFader sceneFader;
        [SerializeField] private string menuSceneName = "MainMenu";
    
        public void Menu()
        {
            PlayerPrefs.SetInt("LevelReached", 2);
            sceneFader.FadeTo(menuSceneName);
        }
        //ideally i should make a base class of pause functions have the menus inherit but I guess for only two
    
    }
}
