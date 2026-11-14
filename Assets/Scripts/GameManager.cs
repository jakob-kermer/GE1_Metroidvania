using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Fields

    // Input
    [Header("Input Action References")]
    [SerializeField] private InputActionReference pause;
    // [SerializeField] private InputActionReference restart;

    // UI
    [Header("UI")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject gameOverScreen;

    private void OnEnable()
    {
        // enable input actions
        pause.action.Enable();
        // restart.action.Enable();

        // listen to input actions
        pause.action.performed += OnPause;
        // restart.action.performed += OnRestart;
    }

    private void OnDisable()
    {
        // stop listening to input actions
        pause.action.performed -= OnPause;
        // restart.action.performed -= OnRestart;

        // disable input actions
        pause.action.Disable();
        // restart.action.Disable();
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

    /* private void OnRestart(InputAction.CallbackContext context)
    {
        RestartGame();
    } */

    private void PauseGame()
    {
        Debug.Log("Game paused");
        
        // stop time
        Time.timeScale = 0f;

        // show pause screen
        pauseScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        Debug.Log("Game resumed");
        
        // stop showing pause screen
        pauseScreen.SetActive(false);

        // reset time
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        Debug.Log("Game restarted");
        
        // restart current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // reset time
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        Debug.Log("Game over");
        
        // stop taking in pause input action
        pause.action.performed -= OnPause;
        pause.action.Disable();

        // stop time
        Time.timeScale = 0f;

        // show game over screen
        gameOverScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Game quit");
        
        // quit game
        Application.Quit();
    }
}
