using UnityEngine;

// [RequireComponent(typeof(PlayerMovement))]
//more aptly, PlayerMovement class
public class PlayerInputController : MonoBehaviour
{  
    public float PlayerSpeed{get;set;}
    private PlayerMovementController _playerMovementController;
    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerMovementController = new PlayerMovementController(_rb, PlayerSpeed);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
           Debug.Log("received space");
           _playerMovementController.Jump();
        }
        Vector3 inputMovement = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        // if (Input.GetKeyDown(KeyCode.W)){
        //     Debug.Log("w pressed");
        // }
        // Debug.Log(inputMovement.x + " " + inputMovement.z);
        
        _playerMovementController.Movement(inputMovement);
    }
}
