using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public string playerTag = "Player";
    public float distance = 5f; // Distance from the sphere
    public float height = 2f;   // Height above the sphere
    public float damping = 5f;
    private Transform player;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
    }
}
