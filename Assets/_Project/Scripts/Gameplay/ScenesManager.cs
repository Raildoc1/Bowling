using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bowling.Gameplay
{
    public class ScenesManager : MonoBehaviour
    {
        public void NextLevel()
        {
            Debug.Log("NextLevel()");
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCount)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        public void RestartLevel()
        {
            Debug.Log("RestartLevel()");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}