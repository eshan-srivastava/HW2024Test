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
    public float gameOverThreshold = -3f;
    
    [Inject]
    private readonly SignalBus _signalBus;

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
        if (transform.position.y < -10f || Input.GetKeyDown(KeyCode.L))
        {
            _signalBus.Fire<PlayerDiedSignal>();
        }
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
           _playerMovementController.Jump();
        }
        Vector3 inputMovement = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        _playerMovementController.Movement(inputMovement);
    }
}
