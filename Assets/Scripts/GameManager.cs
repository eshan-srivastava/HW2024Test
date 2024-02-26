using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public float gameOverThreshold = -10f;
    // public GameObject player;

    [SerializeField]
    private PlayerInputController playerInputController;
    private MyDataClass apiData;
    [SerializeField]
    private JsonLoader jsonLoader;
    [SerializeField]
    private PlatformGenerator platformGenerator;
    void Start()
    {
        apiData = jsonLoader.GetLoadedData();
        platformGenerator.SetPulpitData(apiData.pulpit_data);
        playerInputController.PlayerSpeed = apiData.player_data.speed;
    }

    void Update()
    {
        if (playerInputController.transform.position.y < gameOverThreshold)
        {
            Debug.Log("Game over");
            StopGame();
            ShowGameOverScreen();
        }
    }

    void StopGame()
    {
        Time.timeScale = 0f;
        playerInputController.enabled = false;
        // Stop other game elements (audio, etc.)
    }
    
    void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        // ... (other actions for displaying game over)
    }
}