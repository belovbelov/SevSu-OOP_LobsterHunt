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
        public Player Player;
        public GameObject PauseMenu;

        public GameObject GameOverScreen;

        public GameObject NextLevelScreen;
        [SerializeField] private readonly int initialScore = Score.Instance.Amount;
        public void Update()
        {
            //переместить код в функцию
            if (!GameIsPaused)
            {
                Score.Instance.TimeSpent = Time.timeSinceLevelLoad;
            }
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
                ShowNextLevelScreen();
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
            PauseMenu.SetActive(false);
            NextLevelScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            GameIsPaused = true;
            Debug.Log(Score.Instance.Fishkilled.ToString());
            Debug.Log(Score.Instance.TimeSpent.ToString("0"));
            Debug.Log(Score.Instance.Deaths.ToString());
        }

        public void ShowWinScreen()
        {

        }

    }
}