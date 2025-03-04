using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
            }

            else
            {
                PauseGame(true);
            }
        }
    }

    #region Game Over

    public void Gameover()
    {
        gameOverScreen.SetActive(true);
        Sound_Manager.instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
     #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
     #endif

    }

    #endregion

    #region Pause

    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);

        if (status)
        {
            Time.timeScale = 0;
        }

        else
        { 
            Time.timeScale = 1;
        }
    }

    public void SoundVolume()
    {
        Sound_Manager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        Sound_Manager.instance.ChangeMusicVolume(0.2f);
    }

    #endregion
}
