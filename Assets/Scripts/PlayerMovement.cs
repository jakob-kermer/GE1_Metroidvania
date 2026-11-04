using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Fields
    [SerializeField] private Rigidbody rb;

    // Input
    [Header("Input actions")]
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;

    // Movement
    [Header("Movement speed")]
    [SerializeField] private float speed;
    
    // Jump logic
    [Header("Jump stats")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    [SerializeField] private bool doubleJumpUnlocked;
    [SerializeField] private bool canDoubleJump;

    // Raycast
    [Header("Ground checking raycast")]
    [SerializeField] private float groundCheckDistance = 1.1f;
    [SerializeField] private LayerMask groundLayer;

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
        // check every frame if the character is grounded
        if (IsGrounded())
        {
            // if yes, enable double jumping again
            canDoubleJump = true;
        }

        // movement
        Move();
    }

    // Movement
    private void Move()
    {
        // movement with "move" input action
        // create 2D input vector depending on input
        var input = move.action.ReadValue<Vector2>().normalized;

        // set character velocity depending on input, but only left and right (x-axis), y and z
        rb.linearVelocity = new Vector3(
            input.x * speed,
            rb.linearVelocity.y,
            rb.linearVelocity.z
        );
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        // if the "jump" input action is performed and the character is grounded...
        if (context.performed && IsGrounded())
        {
            // ...jump
            Jump();
        }
        // if the character is not grounded but has the double jump unlocked...
        else if (doubleJumpUnlocked && context.performed && canDoubleJump)
        {
            // ...jump anyway (in the air) and then disable double jumping 
            DoubleJump();
            canDoubleJump = false;
        }
    }

    private void Jump()
    {
        // add upwards force to jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void DoubleJump()
    {
        // set upwards velocity to 0 to make the second jump uniform and independent from previous velocity
        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x,
            0,
            rb.linearVelocity.z
        );
        // add upwards force to jump
        rb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
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
