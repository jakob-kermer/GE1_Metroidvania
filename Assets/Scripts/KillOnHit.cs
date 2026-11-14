using UnityEngine;

public class KillOnHit : MonoBehaviour
{
    // Fields

    // GameManager
    [SerializeField] private GameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"I collided with {collision.gameObject.name}");

        // if the Player collides with this game object...
        if (collision.gameObject.tag == "Player")
        {
            // ...trigger a game over
            gameManager.GameOver();
        }
    }
}
