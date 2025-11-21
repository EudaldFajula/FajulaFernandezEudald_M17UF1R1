using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool gamePaused = false;
    public static event Action<bool> PausePlayer = delegate { };
    public void Pause()
    {
        PausePlayer.Invoke(true);
        gamePaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
    public void Resume()
    {
        PausePlayer.Invoke(false);
        gamePaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void OnUsePauseMenu()
    {
        
        if (gamePaused)
        {
            Debug.Log("Resume juego");
            Resume();
        }
        else {
            Debug.Log("Pause juego");
            Pause(); }
    }
    private void OnEnable()
    {
        Player.UsePauseMenu += OnUsePauseMenu;
    }
    private void OnDisable()
    {
        Player.UsePauseMenu -= OnUsePauseMenu;
    }
    private void OnDestroy()
    {
        Player.UsePauseMenu -= OnUsePauseMenu;
    }
}