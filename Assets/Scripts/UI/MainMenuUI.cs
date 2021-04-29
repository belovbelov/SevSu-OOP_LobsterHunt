using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lobster.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}