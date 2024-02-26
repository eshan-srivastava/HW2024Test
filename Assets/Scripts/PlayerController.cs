using UnityEngine;
public class PlayerController
{
    private float _playerSpeed = 3f;
    public float jumpMultiplier = 1.8f;
    public float movementMultiplier = 2.5f;
    private Rigidbody _rb;
    
    public PlayerController(Rigidbody rb, float playerSpeed){
        _rb = rb;
        _playerSpeed = playerSpeed;
    }
    public void Movement(Vector3 inputMovement){
        float movementFactor = movementMultiplier * _playerSpeed;
        // rb.velocity += inputMovement * movementFactor;
        _rb.velocity = movementFactor * inputMovement;
    }
    public void Jump(){
        if (IsGrounded()){
           return;
        }
        float jumpfactor = jumpMultiplier * _playerSpeed;
        // _rb.velocity = new Vector3(_rb.velocity.x, jumpfactor, _rb.velocity.z);
        _rb.velocity += new Vector3(0, jumpfactor, 0);
    }
    bool IsGrounded()
    {
        return _rb.velocity.y == 0;
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