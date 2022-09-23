using SnakeGameScripts.SnakeScripts;
using UnityEngine;

namespace SnakeGameScripts.UI
{
    public class GameOverUIHandler : MonoBehaviour
    {

        [SerializeField]
        private GameObject gameOverUI;
        private SnakeController _snakeController;
    
        void Start()
        {
            FindSnakeController();
            SubscribeSnakeControllerGameOverAction();
        }

        private void FindSnakeController() => _snakeController = FindObjectOfType<SnakeController>();
        private void SubscribeSnakeControllerGameOverAction() => _snakeController.GameOverAction += OnGameOver;
        private void UnSubscribeSnakeControllerGameOverAction() => _snakeController.GameOverAction -= OnGameOver;

        private void OnGameOver() => gameOverUI.SetActive(true);

        private void OnDestroy()
        {
            UnSubscribeSnakeControllerGameOverAction();
        }
    }
}
