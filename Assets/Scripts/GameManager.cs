using System.Collections;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject winLevelScreen;
    public float gameOverThreshold = -10f;
    
    // private bool _dataLoaded = false;
    private static bool _gameHasEnded;
    
    private MyDataClass _apiData;

    [SerializeField] private GameObject player;
    // [SerializeField] | infer from player
    private PlayerInputController _playerInputController;
    // [SerializeField] | DI'd
    [Inject]
    private JsonLoader _jsonLoader;
    [SerializeField]
    private PlatformGenerator platformGenerator;
    void Start()
    {
        //for zenject, make loading a signal and make it a subject that the GM observes to set data values
        _gameHasEnded = false;
        _playerInputController = player.GetComponent<PlayerInputController>();
        // _apiData = jsonLoader.GetLoadedData();
        _apiData = _jsonLoader.GetLoadedDataSync();
        SetDataFromApi();
        // StartCoroutine(FetchAndLoadData());
    }

    void Update()
    {
        if (_gameHasEnded)
        {
            return;
        }
        if (_playerInputController.transform.position.y < gameOverThreshold || Input.GetKeyDown(KeyCode.L))
        {
            EndLevel();
        }

        if (PlayerStats.score == 50 || Input.GetKeyDown(KeyCode.J))
        {
            WinLevel();
        }
    }

    public void SetDataFromApi()
    {
        platformGenerator.SetPulpitData(_apiData.pulpit_data);
        //PlayerInputController.PlayerSpeed = _apiData.player_data.speed;
        PlayerMovementController.playerSpeed = _apiData.player_data.speed;
        player.SetActive(true);
    }
    private IEnumerator FetchAndLoadData()
    {
        yield return new WaitWhile(() => _apiData.player_data == null);
    }
    private void EndLevel()
    {
        _gameHasEnded = true;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
        _playerInputController.enabled = false;
        // Debug.Log("Game Ended");
    }
    
    private void WinLevel()
    {
        Time.timeScale = 0f;
        _gameHasEnded = true;
        winLevelScreen.SetActive(true);
    }
    
}