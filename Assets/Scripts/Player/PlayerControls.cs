using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerControls : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        
        public bool AimPressed { get; private set; }
        public bool BlowPressed { get; private set; }
        public delegate void InputDelegate();

        public InputDelegate OnInteractPressed;
        public InputDelegate OnJumpPressed;

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
        
        public void OnJump(InputValue value)
        {
            if (value.isPressed)
            {
                OnJumpPressed?.Invoke();
            }
        }
        
        public void OnAim(InputValue value)
        {
            AimPressed = value.isPressed;
        }
        
        public void OnBlow(InputValue value)
        {
            BlowPressed = value.isPressed;
        }
    }
}