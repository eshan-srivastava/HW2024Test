using Interfaces;
using UnityEngine;
using Zenject;

// [RequireComponent(typeof(PlayerMovement))]
//more aptly, PlayerMovement class
public class PlayerInputController : MonoBehaviour
{
    // both interface usage and direct class type is working
    // private PlayerMovementController _playerMovementController;
    private IPlayerMovementController _playerMovementController;
    
    private Rigidbody _rb;

    [Inject]
    public void Construct(IPlayerMovementController playerMovementController)
    {
        _playerMovementController = playerMovementController;
    }
    private void OnEnable()
    {
        //moved to dependency
        // _rb = GetComponent<Rigidbody>();
        // _playerMovementController = new PlayerMovementController(_rb);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
           // Debug.Log("received space");
           _playerMovementController.Jump();
        }
        Vector3 inputMovement = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        _playerMovementController.Movement(inputMovement);
    }
}
