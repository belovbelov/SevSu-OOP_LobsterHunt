using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lobster.UI
{
    public class MainMenuUI : MonoBehaviour, ILoadable
    {
        [SerializeField] private Animator animator;
        public float transitionTime = 2f;


        public void PlayGame()
        {
            Score.Instance.Zeros();
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public IEnumerator LoadLevel(int levelIndex)
        {
            animator.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(levelIndex);
        }
    }
}