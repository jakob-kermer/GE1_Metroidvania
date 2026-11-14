using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Fields
    [Header("Gizmo distances")]
    [SerializeField] private float chaseDistance;
    [SerializeField] private float attackDistance;

    private void OnTriggerEnter(Collider other)
    {
        // if the enemy collides with the hitbox trigger from Player...
        if (other.gameObject.tag == "Player")
        {
            // ...disable this game object
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        // draw chase distance radius
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
        // draw attack distance radius
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
