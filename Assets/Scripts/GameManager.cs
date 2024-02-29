using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject winLevelScreen;
    public float gameOverThreshold = -10f;
    
    // private bool _dataLoaded = false;
    public static bool gameHasEnded;
    
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
        gameHasEnded = false;
        _apiData = jsonLoader.GetLoadedData();
        StartCoroutine(FetchAndLoadData());
        
    }

    void Update()
    {
        if (gameHasEnded)
        {
            return;
        }
        if (playerInputController.transform.position.y < gameOverThreshold || Input.GetKeyDown(KeyCode.L))
        {
            EndLevel();
        }

        if (PlayerStats.score == 50 || Input.GetKeyDown(KeyCode.J))
        {
            WinLevel();
        }
    }
    private IEnumerator FetchAndLoadData()
    {
        // if (apiData != null){
        //     Debug.Log("not null api data");
            
        // }
        // yield return _apiData;
        yield return new WaitWhile(() => _apiData == null);
        
        platformGenerator.SetPulpitData(_apiData.pulpit_data);
        //playerInputController.PlayerSpeed = _apiData.player_data.speed;
        PlayerInputController.PlayerSpeed = _apiData.player_data.speed;
        player.SetActive(true);
    }
    private void EndLevel()
    {
        gameHasEnded = true;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
        playerInputController.enabled = false;
        // Debug.Log("Game Ended");
    }
    
    private void WinLevel()
    {
        Time.timeScale = 0f;
        gameHasEnded = true;
        winLevelScreen.SetActive(true);
    }
    
}