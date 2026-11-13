using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackScript : MonoBehaviour
{
    // Fields

    // Input
    [Header("Input actions")]
    [SerializeField] private InputActionReference attack;

    // Attack stats
    [Header("Attack stats")]
    [SerializeField] private Collider hitbox;
    [SerializeField] private float attackDuration;

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

        // attack
        Debug.Log("I'm attacking rn");

        // wait for some time
        yield return new WaitForSeconds(attackDuration);

        // disable the hitbox collider again
        hitbox.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"I collided with {collision.gameObject.name}");

        // if a game object with the tag "Enemy" collides with this one...
        if (collision.gameObject.tag == "Enemy")
        {
            // ...get damaged
            throw new NotImplementedException("Player should be damaged/killed by an enemy now.");
        }
    }
}
