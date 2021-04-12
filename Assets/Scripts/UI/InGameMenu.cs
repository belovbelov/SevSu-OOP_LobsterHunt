using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Assets.Scripts.Entities;


namespace Assets.Scripts.UI
{
    public class InGameMenu : MonoBehaviour
    {
        public static bool GameIsPaused;


        public GameObject pauseMenu;

        public GameObject gameOverScreen;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
            if (!Player.isBreathing)
            {
                ShowGameOverScreen();
            }
        }

        public void OnDestroy()
        {
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        public void Resume()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Pause()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void LoadMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ShowGameOverScreen()
        {
            pauseMenu.SetActive(false);
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }

        public void RestartGame()
        {
            gameOverScreen.SetActive(false);
            GameIsPaused = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}