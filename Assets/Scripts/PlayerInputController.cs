using UnityEngine;

// [RequireComponent(typeof(PlayerMovement))]
//more aptly, PlayerMovement class
public class PlayerInputController : MonoBehaviour
{  
    public float PlayerSpeed{get;set;}
    private PlayerController _playerController;
    private Rigidbody _rb;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerController = new PlayerController(_rb, PlayerSpeed);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
           _playerController.Jump();
        }
        Vector3 inputMovement = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _playerController.Movement(inputMovement);
    }
}
