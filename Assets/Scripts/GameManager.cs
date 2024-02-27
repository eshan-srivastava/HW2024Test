using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public float gameOverThreshold = -10f;
    // public GameObject player;

    private bool _dataLoaded = false;
    private MyDataClass _apiData;

    [SerializeField] private GameObject player;
    [SerializeField]
    private PlayerInputController playerInputController;
    [SerializeField]
    private JsonLoader jsonLoader;
    [SerializeField]
    private PlatformGenerator platformGenerator;
    void Start()
    {
        _apiData = jsonLoader.GetLoadedData();
        // if (apiData != null){
        //     Debug.Log("not null api data");
            
        // }
        
        platformGenerator.SetPulpitData(_apiData.pulpit_data);
    }

    void Update()
    {
        if (_dataLoaded)
        {
            return;
        }
        else
        {
            if (_apiData != null && player.activeInHierarchy == false)
            {
                _dataLoaded = true;
                playerInputController.PlayerSpeed = _apiData.player_data.speed;
                player.SetActive(true);
            }
        }
        
        if (playerInputController.transform.position.y < gameOverThreshold)
        {
            // Debug.Log("Game over");
            // StopGame();
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