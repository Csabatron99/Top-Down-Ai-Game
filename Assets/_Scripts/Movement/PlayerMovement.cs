using UnityEngine;

using TopDown.Movement;
using UnityEngine.InputSystem;

namespace TopDown.Movement
{
    [RequireComponent(typeof(PlayerInput))]
    public class PLayerMovement : Mover
    {
        //Get input from somwhere else.
        private void OnMove(InputValue value)
        {
            Vector3 playerInput = new Vector3(value.Get<Vector2>().x, value.Get<Vector2>().y, 0);
            currentInput = playerInput;
        }
    }

}

