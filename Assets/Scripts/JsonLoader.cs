using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JsonLoader : MonoBehaviour
{
    // private static bool hasLoaded = false;
    public string jsonUrl = "https://s3.ap-south-1.amazonaws.com/superstars.assetbundles.testbuild/doofus_game/doofus_diary.json";
    private MyDataClass _loadedData;

    public void OnEnable(){
        StartCoroutine(LoadJsonCoroutine());
    }
    public MyDataClass GetLoadedData()
    {
        Debug.Log("GetLoadedData method called.");
        return _loadedData;
    }

    // LoadJsonAndPrintDebug method to debug log and load json data
    // public static void LoadJsonAndPrintDebug()
    // {
    //     // Trigger the loading of JSON data
    //     JsonLoader.Instance.StartCoroutine(JsonLoader.Instance.LoadJsonCoroutine());
    // }

    // public void LoadJson()
    // ReSharper disable Unity.PerformanceAnalysis
    public IEnumerator LoadJsonCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(jsonUrl);
        Debug.Log("connecting to : " + jsonUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to download JSON: " + request.error);
        }
        else
        {
            string jsonText = request.downloadHandler.text;
            ParseJson(jsonText);
        }
    }

    private void ParseJson(string jsonText)
    {
        MyDataClass data = JsonUtility.FromJson<MyDataClass>(jsonText);

        if (data != null)
        {
            // Access parsed data
            //float playerSpeed = data.player_data.speed;
            //float minPulpitDestroyTime = data.pulpit_data.min_pulpit_destroy_time;
            //float maxPulpitDestroyTime = data.pulpit_data.max_pulpit_destroy_time;
            //float pulpitSpawnTime = data.pulpit_data.pulpit_spawn_time;

            // Print the parsed data for debugging -> WORKING RIGHT NOW
            //Debug.Log("Player Speed: " + playerSpeed);
            //Debug.Log("Min Pulpit Destroy Time: " + minPulpitDestroyTime);
            //Debug.Log("Max Pulpit Destroy Time: " + maxPulpitDestroyTime);
            //Debug.Log("Pulpit Spawn Time: " + pulpitSpawnTime);
            _loadedData = data;
            Debug.Log("Assigned data to loadedData");
        }
        else
        {
            Debug.LogError("Failed to parse JSON.");
        }
    }
}

// void Start()
    // {
    //     // Ensure that the script is only executed once
    //     if (!hasLoaded)
    //     {
    //         StartCoroutine(LoadJsonCoroutine());
    //         hasLoaded = true;
    //     }
    // }

// coroutine for loading JSON
    // public IEnumerator LoadJsonCoroutine()
    // {
    //     UnityWebRequest request = UnityWebRequest.Get(jsonUrl);
    //     Debug.Log("connecting to : " + jsonUrl);
    //     yield return request.SendWebRequest();

    //     if (request.result != UnityWebRequest.Result.Success)
    //     {
    //         Debug.LogError("Failed to download JSON: " + request.error);
    //     }
    //     else
    //     {
    //         string jsonText = request.downloadHandler.text;
    //         ParseJson(jsonText);
    //     }
    // }

    // // Singleton because attached to one object
    // private static JsonLoader _instance;
    // public static JsonLoader Instance
    // {
    //     get
    //     {
    //         if (_instance == null)
    //         {
    //             _instance = FindObjectOfType<JsonLoader>();
    //             if (_instance == null)
    //             {
    //                 GameObject obj = new GameObject("JsonLoader");
    //                 _instance = obj.AddComponent<JsonLoader>();
    //             }
    //         }
    //         return _instance;
    //     }
    // }

