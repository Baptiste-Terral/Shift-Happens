using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public static bool isMusicPlaying = true;
    
    public GameObject pauseMenuUi;
    public GameObject backgroundImage;

    private void Start()
    {
        pauseMenuUi.SetActive(false);
        backgroundImage.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }    
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMusic();
        }
    }
    
    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        backgroundImage.SetActive(false);
        Time.timeScale = 1f; 
        isGamePaused = false;
    }
    
    public void Pause()
    {
        pauseMenuUi.SetActive(true);
        backgroundImage.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToggleMusic()
    {
        isMusicPlaying = !isMusicPlaying;
    }
}
