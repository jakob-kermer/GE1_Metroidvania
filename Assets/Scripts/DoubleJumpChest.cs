using UnityEngine;

public class DoubleJumpChest : MonoBehaviour
{
    [SerializeField] private PlayerMovement player; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);

        player.DoubleJumpUnlocked = true;
    }
}
