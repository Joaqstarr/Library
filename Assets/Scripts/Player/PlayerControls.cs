using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerControls : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }

        public delegate void InputDelegate();

        public InputDelegate OnInteractPressed;
        public void OnMove(InputValue value)
        {
            MoveInput = value.Get<Vector2>();
        }

        public void OnLook(InputValue value)
        {
            LookInput = value.Get<Vector2>();
        }


        public void OnInteract(InputValue value)
        {
            if (value.isPressed)
            {
                OnInteractPressed?.Invoke();
            }
        }
    }
}