using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    // Jump logic
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckDistance = 1.1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool canDoubleJump;

    void OnEnable()
    {
        // enable input actions
        move.action.Enable();
        jump.action.Enable();

        // listen to jump action
        jump.action.performed += OnJump;
    }

    void OnDisable()
    {
        // stop listening to jump action
        jump.action.performed -= OnJump;

        // disable input actions
        move.action.Disable();
        jump.action.Disable();
    }

    void FixedUpdate()
    {
        // write speed on the console
        // Debug.Log($"{speed}... I am {speed}");

        // movement with move action
        var input = move.action.ReadValue<Vector2>().normalized;

        rb.linearVelocity = new Vector3(
            input.x * speed,
            rb.linearVelocity.y,
            rb.linearVelocity.z
        );
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            Jump();
        }
        else if (context.performed && canDoubleJump)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
}
