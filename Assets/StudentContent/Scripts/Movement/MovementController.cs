using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private InputActionReference jump;
        [SerializeField] private InputActionReference move;
        
        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float groundCheckDistance = 1.1f;
        [SerializeField] private LayerMask groundLayer;
        
        private Rigidbody m_Rb;
        private bool m_CanMove = true;
    
        private void Awake()
        {
            m_Rb = GetComponent<Rigidbody>();
        }

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
            if (!m_CanMove) return;
        
            Move();
        }

        private void Move()
        {
            // Read input value
            var input = move.action.ReadValue<Vector2>();

            // Move
            m_Rb.linearVelocity = new Vector3(
                input.x * speed,
                m_Rb.linearVelocity.y,
                input.y * speed
            );
        }
    
        private void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed && IsGrounded())
                Jump();
        }
    
        private void Jump()
        {
            m_Rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(
                transform.position,
                Vector3.down,
                groundCheckDistance,
                groundLayer
            );
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = IsGrounded() ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
        }
    }
}