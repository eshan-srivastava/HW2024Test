using UnityEngine;

namespace Interfaces
{
    public interface IPlayerMovementController
    {
        public void Jump();
        public void Movement(Vector3 inputMovement);
    }
}
