using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDataManager : MonoBehaviour
{
    private static MyDataManager _instance;
    public static MyDataManager Instance
    {
        //singleton design pattern
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MyDataManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("MyDataManager");
                    _instance = obj.AddComponent<MyDataManager>();
                }
            }
            return _instance;
        }
    }
    // Start is called before the first frame update -> removed
    private MyDataClass myData;
    
    public void SetData(MyDataClass data)
    {
        myData = data;
    }
    public MyDataClass GetData()
    {
        return myData;
    }
}
