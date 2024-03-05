using Interfaces;
using UnityEngine;
using Zenject;

public class PlayerMovementController : IPlayerMovementController
{
    public static float playerSpeed;
    readonly Rigidbody _rb;
    
    private const float JumpMultiplier = 3.8f;
    private const float MovementMultiplier = 2.5f;
    
    [Inject]
    public PlayerMovementController(Rigidbody rb){
        _rb = rb;
    }
    public void Movement(Vector3 inputMovement){
        float movementFactor = MovementMultiplier * playerSpeed;

        // Debug.Log("movement factor is " + movementFactor);
        // rb.velocity += inputMovement * movementFactor;
        
        _rb.velocity = movementFactor * inputMovement;

    }
    public void Jump(){
        if (!IsGrounded()){
           return;
        }
        float jumpFactor = JumpMultiplier * playerSpeed;
        // _rb.velocity = new Vector3(_rb.velocity.x, jumpFactor, _rb.velocity.z);
        // _rb.velocity += new Vector3(0, jumpFactor, 0);
        _rb.AddForce(Vector3.up * jumpFactor, ForceMode.VelocityChange);
    }
    private bool IsGrounded()
    {
        // return _rb.velocity.y == 0; // ideally, working with position check for now, realistic y limit should be 0.7-1
        return _rb.position.y < 2;
    }
}

/*
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
*/