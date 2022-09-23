using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnakeGameScripts.Generic
{
    public class LevelLoader : MonoBehaviour
    {
        
        public void RestartCurrentLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
