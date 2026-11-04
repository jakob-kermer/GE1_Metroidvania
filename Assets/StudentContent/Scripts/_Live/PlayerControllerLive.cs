using UnityEngine;
using UnityEngine.InputSystem;

namespace _Live
{
    public class PlayerControllerLive : MonoBehaviour
    {
        public InputActionReference jump;
        public InputActionReference move;
        
        public Rigidbody rb;
        public float speed;
        public float jumpForce;
        
        private void OnEnable()
        {
            move.action.Enable();
            jump.action.Enable();
            
            jump.action.performed += OnJump;
        }


        private void OnDisable()
        {
            jump.action.performed -= OnJump;
            
            move.action.Disable();
            jump.action.Disable();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            var input = move.action.ReadValue<Vector2>().normalized;

            rb.linearVelocity = new Vector3(
                input.x * speed,
                rb.linearVelocity.y,
                input.y * speed
            );
        }
        
        private void OnJump(InputAction.CallbackContext obj)
        {
            Jump();
        }
        
        private void Jump()
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}