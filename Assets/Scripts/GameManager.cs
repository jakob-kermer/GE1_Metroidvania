using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Fields

    // Input
    [Header("Input Action References")]
    [SerializeField] private InputActionReference pause;
    [SerializeField] private InputActionReference restart;

    private void OnEnable()
    {
        // enable input actions
        pause.action.Enable();
        restart.action.Enable();

        // listen to input actions
        pause.action.performed += OnPause;
        restart.action.performed += OnRestart;
    }

    private void OnDisable()
    {
        // stop listening to input actions
        pause.action.performed -= OnPause;
        restart.action.performed -= OnRestart;
        
        // disable input actions
        pause.action.Disable();
        restart.action.Disable();
    }

    private void OnRestart(InputAction.CallbackContext context)
    {
        RestartGame();
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0f)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
