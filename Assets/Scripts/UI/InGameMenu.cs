using Assets.Scripts.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.UI
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] fishCount;
        public static bool GameIsPaused;

        public GameObject PauseMenu;

        public GameObject GameOverScreen;

        public GameObject WinScreen;

        private void Start()
        {
            fishCount = GameObject.FindGameObjectsWithTag("Fish");
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

            if (!Player.IsBreathing || Player.IsDead)
            {
                ShowGameOverScreen();
            }

            fishCount = GameObject.FindGameObjectsWithTag("Fish");
            if (fishCount.Length == 0)
            {
                ShowWinScreen();
            }
        }

        public void OnDestroy()
        {
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        public void Resume()
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Pause()
        {
            PauseMenu.SetActive(true);
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
            PauseMenu.SetActive(false);
            GameOverScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            GameIsPaused = true;
        }

        public void RestartGame()
        {
            GameOverScreen.SetActive(false);
            GameIsPaused = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ShowWinScreen()
        {
            PauseMenu.SetActive(false);
            WinScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            GameIsPaused = true;
        }
    }
}