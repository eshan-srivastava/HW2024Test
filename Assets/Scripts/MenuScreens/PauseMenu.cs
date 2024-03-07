using UnityEngine;
using UnityEngine.SceneManagement;

namespace MenuScreens
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool gamePaused = false;

        [SerializeField] GameObject PauseMenuUI;
        [SerializeField] private SceneFader sceneFader;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause(PauseMenuUI);
            }
        }

        public void TogglePause(GameObject menuUI)
        {
            menuUI.SetActive(!menuUI.activeSelf);

            Time.timeScale = menuUI.activeSelf ? 0f : 1f;
        }

        public void Retry(GameObject menuUI)
        {
            Debug.Log($"Load home hit from {menuUI.name}");
            TogglePause(menuUI);
            Time.timeScale = 1f;
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        }
        public void Menu(GameObject menuUI)
        {
            TogglePause(menuUI);
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
}
