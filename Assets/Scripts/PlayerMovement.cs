using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

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
        Debug.Log("jump pressed");
        Jump();
    }

    private void Jump()
    {
        // add upwards force to jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
