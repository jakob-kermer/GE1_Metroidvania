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

    // Animator 
    [Header("Animation stuff")]
    [SerializeField] private Animator animator;

    // Player Model
    [SerializeField] private GameObject playerModel;

    private void OnEnable()
    {
        // enable input actions
        move.action.Enable();
        jump.action.Enable();

        // listen to jump action
        jump.action.performed += OnJump;
    }

    private void OnDisable()
    {
        // stop listening to jump action
        jump.action.performed -= OnJump;

        // disable input actions
        move.action.Disable();
        jump.action.Disable();
    }

    private void FixedUpdate()
    {
        // check every frame if the character is grounded
        if (IsGrounded())
        {
            // if yes, enable double jumping again
            canDoubleJump = true;
            animator.SetBool("IsFalling", false);
            animator.SetBool("Touching Ground", true);
        }

        // if the player is moving downwards...
        if (rb.linearVelocity.y < 0)
        {
            // ...the player is falling and not touching the ground
            animator.SetBool("IsFalling", true);
            animator.SetBool("Touching Ground", false);
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

        // if the player is moving in any direction...
        if (input.x != 0)
        {
            // ...the player is running (duh)...
            animator.SetBool("IsRunning", true);
            // ...and rotate the player in movement direction
            playerModel.transform.rotation = Quaternion.LookRotation(input);
        }
        // otherwise...
        else
        {
            // ...the player is running (duh)
            animator.SetBool("IsRunning", false);
        }
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

        // set animation parameters
        animator.SetBool("Touching Ground", false);
        animator.SetTrigger("JumpTrigger");
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

        // set animation parameters
        animator.SetBool("Touching Ground", false);
        animator.SetTrigger("JumpTrigger");
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
        // draw IsGrounded raycast gizmo
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
