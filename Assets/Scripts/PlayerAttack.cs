using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    // Fields

    // Input
    [Header("Input actions")]
    [SerializeField] private InputActionReference attack;

    // Attack stats
    [Header("Attack stats")]
    [SerializeField] private Collider hitbox;
    [SerializeField] private float attackDuration;

    // Animator
    [Header("Animation stuff")]
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        // enable input actions
        attack.action.Enable();

        // listen to attack action
        attack.action.performed += OnAttack;
    }

    private void OnDisable()
    {
        // stop listening to attack action
        attack.action.performed -= OnAttack;

        // disable input actions
        attack.action.Disable();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        // if the "attack" input action is performed...
        if (context.performed)
        {
            // ...attack
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        // enable the hitbox collider
        hitbox.enabled = true;

        // write attack message on the console
        Debug.Log("I'm attacking rn");

        // set animation parameter
        animator.SetTrigger("HitIt");

        // wait for some time
        yield return new WaitForSeconds(attackDuration);

        // disable the hitbox collider again
        hitbox.enabled = false;
    }
}
