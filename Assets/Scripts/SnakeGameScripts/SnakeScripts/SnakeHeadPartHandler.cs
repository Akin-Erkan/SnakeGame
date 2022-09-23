using System;
using SnakeGameScripts.EdibleElements;
using SnakeGameScripts.InputScripts;
using UnityEngine;

namespace SnakeGameScripts.SnakeScripts
{
    /// <summary>
    /// Snake head part handler.
    /// Sends raycast and detects objects.
    /// </summary>
    public class SnakeHeadPartHandler : SnakePartHandler
    {
        public Action<IncreaseSnakeLenghtEdible> OnIncreaseSnakeLenghtEdible;
        public Action<DecreaseSnakeLenghtEdible> OnDecreaseSnakeLenghtEdible;
        public Action<SpeedUpSnakeEdible> OnSpeedUpSnakeEdible;
        public Action<SlowDownSnakeEdible> OnSlowDownSnakeEdible;
        public Action<ReverseSneakEdible> OnReverseSneakEdible;
        public Action OnGameOver;


        private MovementInputHandler _movementInputHandler;
        private SnakeController.Direction _headMovementDirection = SnakeController.Direction.East;
        
        
        protected override void Awake()
        {
            base.Awake();
            FindMovementInputHandler();
        }

        private void Start()
        {
            SubscribeToMovementInputs();
        }
        

        private void FindMovementInputHandler() => _movementInputHandler = FindObjectOfType<MovementInputHandler>();

        private void SubscribeToMovementInputs()
        {
            if (_movementInputHandler)
            {
                _movementInputHandler.OnUpDirectionButtonClickEvent += SetHeadDirection;
                _movementInputHandler.OnDownDirectionButtonClickEvent += SetHeadDirection;
                _movementInputHandler.OnLeftDirectionButtonClickEvent += SetHeadDirection;
                _movementInputHandler.OnRightDirectionButtonClickEvent += SetHeadDirection;
            }
        }
        
        private void UnSubscribeToMovementInputs()
        {
            if (_movementInputHandler)
            {
                _movementInputHandler.OnUpDirectionButtonClickEvent -= SetHeadDirection;
                _movementInputHandler.OnDownDirectionButtonClickEvent -= SetHeadDirection;
                _movementInputHandler.OnLeftDirectionButtonClickEvent -= SetHeadDirection;
                _movementInputHandler.OnRightDirectionButtonClickEvent -= SetHeadDirection;
            }
        }
        
        private void SetHeadDirection(SnakeController.Direction headMovementDirection)
        {
            if (headMovementDirection == _headMovementDirection)
                return;
            if(headMovementDirection == SnakeController.Direction.East && _headMovementDirection == SnakeController.Direction.West)
                return;
            if(headMovementDirection == SnakeController.Direction.West && _headMovementDirection == SnakeController.Direction.East)
                return;
            if(headMovementDirection == SnakeController.Direction.North && _headMovementDirection == SnakeController.Direction.South)
                return;
            if(headMovementDirection == SnakeController.Direction.South && _headMovementDirection == SnakeController.Direction.North)
                return;
            _headMovementDirection = headMovementDirection;
                
            switch (headMovementDirection)
            {
                case SnakeController.Direction.South:
                    transform.rotation = Quaternion.Euler(90,0,270);
                    break;
                case SnakeController.Direction.North:
                    transform.rotation = Quaternion.Euler(90,0,90);
                    break;
                case SnakeController.Direction.West:
                    transform.rotation = Quaternion.Euler(90,0,180);
                    break;
                case SnakeController.Direction.East:
                    transform.rotation = Quaternion.Euler(90,0,0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var increaseSnakeLenghtEdible = other.GetComponent<IncreaseSnakeLenghtEdible>();
            if (increaseSnakeLenghtEdible)
                OnIncreaseSnakeLenghtEdible?.Invoke(increaseSnakeLenghtEdible);
            var decreaseSnakeLenghtEdible = other.GetComponent<DecreaseSnakeLenghtEdible>();
            if (decreaseSnakeLenghtEdible)
                OnDecreaseSnakeLenghtEdible?.Invoke(decreaseSnakeLenghtEdible);
            var speedUpSnakeEdible = other.GetComponent<SpeedUpSnakeEdible>();
            if (speedUpSnakeEdible)
                OnSpeedUpSnakeEdible?.Invoke(speedUpSnakeEdible);
            var slowDownSnakeEdible = other.GetComponent<SlowDownSnakeEdible>();
            if (slowDownSnakeEdible)
                OnSlowDownSnakeEdible?.Invoke(slowDownSnakeEdible);
            var reverseSneakEdible = other.GetComponent<ReverseSneakEdible>();
            if (reverseSneakEdible)
                OnReverseSneakEdible?.Invoke(reverseSneakEdible);
            
            var bodyPart = other.GetComponent<SnakeBodyPartHandler>();
            if (bodyPart)
            {
                OnGameOver?.Invoke();
                Debug.Log("Game Over!!");
            }
        }


        private void OnDestroy()
        {
            UnSubscribeToMovementInputs();
        }
    }
}
