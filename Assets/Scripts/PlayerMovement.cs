using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        // Access the JsonLoader instance
        JsonLoader jsonLoader = JsonLoader.Instance;

        // Ensure JsonLoader is not null before proceeding
        if (jsonLoader != null)
        {
            // Start a coroutine to wait for data to be loaded
            StartCoroutine(WaitForDataLoaded(jsonLoader));
        }
        else
        {
            Debug.LogError("JsonLoader instance is null.");
        }

        rb = GetComponent<Rigidbody>();
    }
    private IEnumerator WaitForDataLoaded(JsonLoader jsonLoader)
    {
        // Trigger JSON loading
        yield return StartCoroutine(jsonLoader.LoadJsonCoroutine());

        // Access the loaded data
        MyDataClass loadedData = jsonLoader.GetLoadedData();

        // Ensure loaded data is not null before using it
        if (loadedData != null)
        {
            // Access and use the data as needed
            Debug.Log("PlayerSpeed: " + loadedData.player_data.speed);
            Debug.Log("Min Pulpit Destroy: " + loadedData.pulpit_data.min_pulpit_destroy_time);
            playerSpeed = loadedData.player_data.speed;

            Update();
        }
        else
        {
            Debug.LogError("Loaded data is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float jumpfactor = (float)1.8 * playerSpeed;
        float movementFactor = (float)2.5 * playerSpeed;
        if(Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpfactor, rb.velocity.z);
        }
        if(Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, movementFactor);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -movementFactor);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(movementFactor, rb.velocity.y, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-movementFactor, rb.velocity.y, rb.velocity.z);
        }
    }
    bool IsGrounded()
    {
        //Physics.CheckSphere(groundCheck.position, 0.1f, );
        return rb.velocity.y == 0;
    }
}

/*
 * MyDataManager dataManager = MyDataManager.Instance;
        if(dataManager != null)
        {
            MyDataClass data = dataManager.GetData();
            if (data != null)
            {
                Debug.Log(data.player_data.speed);
                playerSpeed = data.player_data.speed;
            }
            else
            {
                Debug.Log("Data.GET is null, retrying");
                JsonLoader.LoadJsonAndPrintDebug();
            }
        }
        else
        {
            Debug.Log("DataManager is null");
        }
*/