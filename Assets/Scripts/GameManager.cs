using System;
using System.Collections;
using Lobster.Entities;
using Lobster.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Lobster
{
    public class GameManager : MonoBehaviour, ILoadable
    {
        private GameManager() {}
        [SerializeField] private static GameManager instance;
        [SerializeField] public static GameManager Instance
        {
            get { return instance;}
        }
        
        [SerializeField]
        private GameObject[] fishCount;
// костыль(то что паблик, решиит валера. Сделать ShopAlert синглтон?) -> 25 строчка в weapon 
        [SerializeField] public ShopAlert shop;
        private int initialScore;
        public bool GameIsPaused;
        public Player Player;
        public GameObject PauseMenu;
        public GameObject GameOverScreen;
        [FormerlySerializedAs("NextLevelScreen")] public GameObject EndLevelScreen;

        public Animator animator;
        float transitionTime = 2f;
        private static readonly int Start1 = Animator.StringToHash("Start");

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                instance.initialScore = Score.Instance.Amount;
            }
        }

        private void Start()
        {
            UpdateScore();
            shop = GameObject.Find("ShopManager").GetComponent<ShopAlert>();
            Player = GameObject.Find("Player").GetComponent<Player>();
        }

        public void Update()
        {
            if (!shop.ShopIsOpen)
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
            }

            if (shop.alertShop)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!shop.ShopIsOpen)
                    {
                        shop.OpenShop();
                    }
                    else
                    {
                        shop.CloseShop();
                    }
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
                return;
            }

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

        public void Pause()
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void LoadMenu()
        {
            StartCoroutine(LoadLevel(0));
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void LoadNextLevel()
        {
            Time.timeScale = 1f;
            StopAllCoroutines();
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
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
            StopAllCoroutines();
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        }

        public void ShowNextLevelScreen()
        {
            if (Time.timeScale != 0)
            {
                Score.Instance.TimeSpent += Time.timeSinceLevelLoad;
            }
            PauseMenu.SetActive(false);
            EndLevelScreen.SetActive(true);
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
            EndLevelScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            GameIsPaused = true;
            ShowStats();
        }

        public IEnumerator LoadLevel(int levelIndex)
        {
            animator.SetTrigger("Start");
            yield return new WaitForSecondsRealtime(transitionTime);
            SceneManager.LoadScene(levelIndex);
        }
        // ReSharper disable Unity.PerformanceAnalysis
        private void ShowStats()
        {
            var deaths = GameObject.Find("Deaths");
            deaths.GetComponent<Text>().text = "Deaths: " + Score.Instance.Deaths;
            var timeSpent = GameObject.Find("TimeSpent");
            timeSpent.GetComponent<Text>().text = "Time spent: " + Score.Instance.TimeSpent;
            var fishKilled = GameObject.Find("FishKilled");
            fishKilled.GetComponent<Text>().text = "Fish killed: " + Score.Instance.Fishkilled;
        }
        public static void UpdateScore()
        {
            var txt = GameObject.Find("ScoreText");
            txt.GetComponent<TextMeshProUGUI>().text = "Score: " + Score.Instance.Amount;
        }
    }
}