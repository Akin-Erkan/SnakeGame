using SnakeGameScripts.EdibleElements;
using SnakeGameScripts.SnakeScripts;
using TMPro;
using UnityEngine;

namespace SnakeGameScripts.UI
{
    public class UIEdibleItemCounter : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI edibleScoreCountTMP;
        [SerializeField]
        private TextMeshProUGUI lastEatenEdibleTMP;
        
        private SnakeHeadPartHandler _snakeHeadPartHandler;
        private int _atenEdibleCounter = 0;
        
        void Start()
        {
            FindSnakeHead();
            SubscribeToHeadPartActions();
        }

        private void FindSnakeHead() => _snakeHeadPartHandler = FindObjectOfType<SnakeHeadPartHandler>();

        private void SubscribeToHeadPartActions()
        {
            if (_snakeHeadPartHandler)
            {
                _snakeHeadPartHandler.OnIncreaseSnakeLenghtEdible += OnIncreaseSnakeLenghtEdible;
                _snakeHeadPartHandler.OnDecreaseSnakeLenghtEdible += OnDecreaseSnakeLenghtEdible;
                _snakeHeadPartHandler.OnReverseSneakEdible += OnReverseSneakEdible;
                _snakeHeadPartHandler.OnSlowDownSnakeEdible += OnSlowDownSnakeEdible;
                _snakeHeadPartHandler.OnSpeedUpSnakeEdible += OnSpeedUpSnakeEdible;
            }
        }
        
        private void UnSubscribeToHeadPartActions()
        {
            if (_snakeHeadPartHandler)
            {
                _snakeHeadPartHandler.OnIncreaseSnakeLenghtEdible -= OnIncreaseSnakeLenghtEdible;
                _snakeHeadPartHandler.OnDecreaseSnakeLenghtEdible -= OnDecreaseSnakeLenghtEdible;
                _snakeHeadPartHandler.OnReverseSneakEdible -= OnReverseSneakEdible;
                _snakeHeadPartHandler.OnSlowDownSnakeEdible -= OnSlowDownSnakeEdible;
                _snakeHeadPartHandler.OnSpeedUpSnakeEdible -= OnSpeedUpSnakeEdible;
            }
        }

        private void OnSpeedUpSnakeEdible(SpeedUpSnakeEdible obj)
        {
            lastEatenEdibleTMP.text = "Last Eaten: Speed Up";
            UpdateEdibleCount();
        }

        private void OnSlowDownSnakeEdible(SlowDownSnakeEdible obj)
        {
            lastEatenEdibleTMP.text = "Last Eaten: Slow Down";
            UpdateEdibleCount();

        }

        private void OnReverseSneakEdible(ReverseSneakEdible obj)
        {
            lastEatenEdibleTMP.text = "Last Eaten: Reverse Sneak";
            UpdateEdibleCount();

        }

        private void OnDecreaseSnakeLenghtEdible(DecreaseSnakeLenghtEdible obj)
        {
            lastEatenEdibleTMP.text = "Last Eaten: Decrease Snake";
            UpdateEdibleCount();

        }

        private void OnIncreaseSnakeLenghtEdible(IncreaseSnakeLenghtEdible obj)
        {
            lastEatenEdibleTMP.text = "Last Eaten: Increase Lenght";
            UpdateEdibleCount();

        }

        private void UpdateEdibleCount()
        {
            _atenEdibleCounter++;
            edibleScoreCountTMP.text = "Edible Score : " + _atenEdibleCounter;
        }

        private void OnDestroy()
        {
            UnSubscribeToHeadPartActions();
        }
    }
}
