using Assets.Scripts.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.UI
{
    public class InGameMenu : MonoBehaviour
    {
        public GameObject[] fishCount;
        public static bool GameIsPaused;

        public GameObject pauseMenu;

        public GameObject gameOverScreen;

        public GameObject winScreen;

        private void Start()
        {
                fishCount = GameObject.FindGameObjectsWithTag("Fish");
                Debug.Log(fishCount);
        }

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

            if (!Player.isBreathing || Player.isDead)
            {
                ShowGameOverScreen();
            }

            if (fishCount.Length == Weapon.fishKilled)
            {
                WinScreen();
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

        public void WinScreen()
        {
            pauseMenu.SetActive(false);
            winScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}