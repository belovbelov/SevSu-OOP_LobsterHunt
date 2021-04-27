using Assets.Scripts.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] fishCount;
        public static bool GameIsPaused;
        public Player Player;
        public GameObject PauseMenu;
        public GameObject GameOverScreen;
        public GameObject NextLevelScreen;
        [SerializeField] private readonly int initialScore = Score.Instance.Amount;

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


            if (SceneManager.GetActiveScene().name == "LastLevel" && fishCount.Length == 0)
            {
                ShowWinScreen();
            }
            else
            {
                if (fishCount.Length == 0)
                {
                    ShowNextLevelScreen();
                }

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

        public void LoadNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
            Score.Instance.Amount = initialScore;
            Score.Instance.Deaths += 1;
            GameOverScreen.SetActive(false);
            GameIsPaused = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ShowNextLevelScreen()
        {
            if (Time.timeScale != 0)
            {
                Score.Instance.TimeSpent += Time.timeSinceLevelLoad;
            }
            PauseMenu.SetActive(false);
            NextLevelScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            GameIsPaused = true;
        }

        public void ShowWinScreen()
        {
            if (Time.timeScale != 0)
            {
                Score.Instance.TimeSpent += Time.timeSinceLevelLoad;
            }
            PauseMenu.SetActive(false);
            NextLevelScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            GameIsPaused = true;
            ShowStats();
        }

        private void ShowStats()
        {
            var deaths = GameObject.Find("Deaths");
            deaths.GetComponent<Text>().text = "Deaths: " + Score.Instance.Deaths;
            var timeSpent = GameObject.Find("TimeSpent");
            timeSpent.GetComponent<Text>().text = "Time spent: " + Score.Instance.TimeSpent;
            var fishKilled = GameObject.Find("FishKilled");
            fishKilled.GetComponent<Text>().text = "Fish killed: " + Score.Instance.Fishkilled;
        }
    }
}