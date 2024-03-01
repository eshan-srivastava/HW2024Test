using System.Collections;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Networking;

public class JsonLoader
{
    // private static bool hasLoaded = false;
    private readonly string _jsonUrl = "https://s3.ap-south-1.amazonaws.com/superstars.assetbundles.testbuild/doofus_game/doofus_diary.json";
    private MyDataClass _loadedData;

    // public void OnEnable(){
    //     StartCoroutine(LoadJsonCoroutine());
    // }
    public MyDataClass GetLoadedData()
    {
        Debug.Log("GetLoadedData method called.");
        return _loadedData;
    }

    public MyDataClass GetLoadedDataSync()
    {
        Debug.Log("GetLoadedDataSync method called.");
        FetchDataSync();
        return _loadedData;
    }
    private void FetchDataSync()
    {
        using HttpClient client = new HttpClient();
        HttpResponseMessage res = client.GetAsync(_jsonUrl).Result;

        var data = res.Content.ReadAsStringAsync();

        ParseJson(jsonText:data.Result.ToString());
        // var webRequest = new HttpRequestMessage(HttpMethod.Get, jsonUrl)
        // {
        //     Content = new 
        // }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator LoadJsonCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(_jsonUrl);
        Debug.Log("connecting to : " + _jsonUrl);
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
            _loadedData = data;
            Debug.Log("Assigned data to loadedData");
        }
        else
        {
            Debug.LogError("Failed to parse JSON.");
        }
    }
}
