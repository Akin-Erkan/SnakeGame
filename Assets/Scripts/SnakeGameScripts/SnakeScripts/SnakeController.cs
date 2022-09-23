using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SnakeGameScripts.EdibleElements;
using SnakeGameScripts.InputScripts;
using SnakeGameScripts.LevelControls;
using UnityEngine;

namespace SnakeGameScripts.SnakeScripts
{
    public class SnakeController : MonoBehaviour
    {
        public enum Direction
        {
            South,
            North,
            West,
            East
        }

        public Action GameOverAction;
        
        [SerializeField]
        private Direction movementStartDirection = Direction.East;
        private Direction _headDirection;
        private Direction _tailDirection;

        private SnakeHeadPartHandler _snakeHeadPartHandler;
        private List<SnakeBodyPartHandler> _snakeBodyPartHandlers;

        private MovementInputHandler _movementInputHandler;
        private LevelDesigner _levelDesigner;

        private float _currentSnakeSpeed = 0.25f;
        private float _originalSnakeSpeed = 0.25f;
        [SerializeField]
        private float _tempSpeedUpTime = 2f;
        
        
        private void Awake()
        {
            FindSnakeHead();
            FindSnakeBodyParts();
            FindMovementInputHandler();
            FindLevelDesign();
            SetStartBodyDirections(movementStartDirection);
            SubscribeToMovementInputs();
            SubscribeToHeadPartActions();
            SubscribeToBodyPartActions();
        }

        private void Start()
        {
            StartCoroutine(nameof(StartSnakeMovementControlCoroutine));
        }

        private void FindSnakeHead() => _snakeHeadPartHandler = FindObjectOfType<SnakeHeadPartHandler>();
        private void FindSnakeBodyParts() => _snakeBodyPartHandlers = FindObjectsOfType<SnakeBodyPartHandler>().OrderBy(sp =>sp.currentPartIndex).ToList();
        private void FindMovementInputHandler() => _movementInputHandler = FindObjectOfType<MovementInputHandler>();
        private void FindLevelDesign() => _levelDesigner = FindObjectOfType<LevelDesigner>();

        private void SetStartBodyDirections(Direction startDirection)
        {
            _headDirection = startDirection;
            _tailDirection = startDirection;
        }

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
        private void SubscribeToHeadPartActions()
        {
            if (_snakeHeadPartHandler)
            {
                _snakeHeadPartHandler.OnIncreaseSnakeLenghtEdible += OnIncreaseSnakeLenghtEdible;
                _snakeHeadPartHandler.OnDecreaseSnakeLenghtEdible += OnDecreaseSnakeLenghtEdible;
                _snakeHeadPartHandler.OnReverseSneakEdible += OnReverseSneakEdible;
                _snakeHeadPartHandler.OnSlowDownSnakeEdible += OnSlowDownSnakeEdible;
                _snakeHeadPartHandler.OnSpeedUpSnakeEdible += OnSpeedUpSnakeEdible;
                _snakeHeadPartHandler.OnGameOver += OnGameOver;
            }
        }

        private void OnGameOver()
        {
            GameOverAction?.Invoke();
            StopAllCoroutines();
        }

        private void SubscribeToBodyPartActions() => SnakeBodyPartHandler.OnSnakeBodyInstantiated += AddBodyPartToList;
        private void UnSubscribeToBodyPartActions() => SnakeBodyPartHandler.OnSnakeBodyInstantiated -= AddBodyPartToList;


        private void AddBodyPartToList(SnakeBodyPartHandler obj)
        {
            if(!_snakeBodyPartHandlers.Contains(obj))
                _snakeBodyPartHandlers.Add(obj);
        }

        private void OnSpeedUpSnakeEdible(SpeedUpSnakeEdible obj)
        {
            StartCoroutine(OnSpeedUpSnakeEdibleCoroutine());
            Destroy(obj.gameObject);
        }

        private IEnumerator OnSpeedUpSnakeEdibleCoroutine()
        {
            _currentSnakeSpeed /= 2;
            print("Current Snake Speed: " + _currentSnakeSpeed);
            yield return new WaitForSeconds(_tempSpeedUpTime);
            _currentSnakeSpeed = _originalSnakeSpeed;
        }

        private void OnSlowDownSnakeEdible(SlowDownSnakeEdible obj)
        {
            StartCoroutine(OnSlowDownSnakeEdibleCoroutine());
            Destroy(obj.gameObject);
        }

        private IEnumerator OnSlowDownSnakeEdibleCoroutine()
        {
            _currentSnakeSpeed *= 2;
            print("Current Snake Speed: " + _currentSnakeSpeed);
            yield return new WaitForSeconds(_tempSpeedUpTime);
            _currentSnakeSpeed = _originalSnakeSpeed;
        }

        private void OnReverseSneakEdible(ReverseSneakEdible obj)
        {
            var transform1 = _snakeHeadPartHandler.transform;
            (transform1.position, _snakeBodyPartHandlers[^1].transform.position) = (_snakeBodyPartHandlers[^1].transform.position, transform1.position);
            ReverseHeadDirection();
            ReverseBodyParts();
            Destroy(obj.gameObject);
        }

        private void ReverseBodyParts() => _snakeBodyPartHandlers.Reverse();

        private void ReverseHeadDirection()
        {
            if (_headDirection == Direction.East)
                SetHeadDirection(Direction.West);
            if (_headDirection == Direction.West)
                SetHeadDirection(Direction.East);
            if (_headDirection == Direction.North)
                SetHeadDirection(Direction.South);
            if (_headDirection == Direction.South)
                SetHeadDirection(Direction.North);
        }

        private void OnDecreaseSnakeLenghtEdible(DecreaseSnakeLenghtEdible obj)
        {
            var snakeBodyPartHandler = _snakeBodyPartHandlers[^1];
            _snakeBodyPartHandlers.Remove(snakeBodyPartHandler);
            Destroy(snakeBodyPartHandler.gameObject);
            Destroy(obj.gameObject);
            if (_snakeBodyPartHandlers.Count < 2)
            {
                OnGameOver();
            }
        }

        private void OnIncreaseSnakeLenghtEdible(IncreaseSnakeLenghtEdible obj)
        {
            var directionOfTail = _snakeBodyPartHandlers[^2].transform.position - _snakeBodyPartHandlers[^1].transform.position;
            var snakeBodyPartHandler = Instantiate(_snakeBodyPartHandlers[^1],_snakeBodyPartHandlers[^1].transform.position - directionOfTail,_snakeBodyPartHandlers[^1].transform.rotation,_snakeBodyPartHandlers[^1].transform.parent);
            snakeBodyPartHandler.currentPartIndex = _snakeBodyPartHandlers[^1].currentPartIndex + 1;
            Destroy(obj.gameObject);
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
        
        private void UnSubscribeToHeadPartActions()
        {
            if (_snakeHeadPartHandler)
            {
                _snakeHeadPartHandler.OnIncreaseSnakeLenghtEdible -= OnIncreaseSnakeLenghtEdible;
                _snakeHeadPartHandler.OnDecreaseSnakeLenghtEdible -= OnDecreaseSnakeLenghtEdible;
                _snakeHeadPartHandler.OnReverseSneakEdible -= OnReverseSneakEdible;
                _snakeHeadPartHandler.OnSlowDownSnakeEdible -= OnSlowDownSnakeEdible;
                _snakeHeadPartHandler.OnSpeedUpSnakeEdible -= OnSpeedUpSnakeEdible;
                _snakeHeadPartHandler.OnGameOver -= OnGameOver;
            }
        }

        private void SetHeadDirection(Direction headMovementDirection)
        {
            if (headMovementDirection == _headDirection)
                return;
            if(headMovementDirection == Direction.East && _headDirection == Direction.West)
                return;
            if(headMovementDirection == Direction.West && _headDirection == Direction.East)
                return;
            if(headMovementDirection == Direction.North && _headDirection == Direction.South)
                return;
            if(headMovementDirection == Direction.South && _headDirection == Direction.North)
                return;
            _headDirection = headMovementDirection;
        }


        private IEnumerator StartSnakeMovementControlCoroutine()
        {
            yield return new WaitForSeconds(_currentSnakeSpeed);
            if (_snakeBodyPartHandlers.Count > 2)
            {
                for (int i = _snakeBodyPartHandlers.Count-1; i > 0; i--)
                {
                    if(_snakeBodyPartHandlers[i] == null)
                        continue;
                    _snakeBodyPartHandlers[i].transform.position = _snakeBodyPartHandlers[i - 1].transform.position;
                }
            }

            if (_snakeBodyPartHandlers.Count > 0)
            {
                if (_snakeBodyPartHandlers[0])
                    _snakeBodyPartHandlers[0].transform.position = _snakeHeadPartHandler.transform.position;   
            }

            var firstBorderTransform = _levelDesigner.borderList[0].transform;
            var lastBorderTransform = _levelDesigner.borderList[^1].transform;
            var snakeTransform = _snakeHeadPartHandler.transform;
            switch (_headDirection)
            {
                case Direction.South:
                    if ((Math.Abs(_snakeHeadPartHandler.transform.position.z - firstBorderTransform.position.z) < 1.25f))
                        snakeTransform.position = new Vector3(snakeTransform.position.x, snakeTransform.position.y, lastBorderTransform.position.z - 1);
                    else
                        snakeTransform.position += Vector3.back;
                    break;
                case Direction.North:
                    if ((Math.Abs(snakeTransform.position.z - lastBorderTransform.position.z) < 1.25f))
                        snakeTransform.position = new Vector3(snakeTransform.position.x,  snakeTransform.position.y, firstBorderTransform.position.z + 1);
                    else
                        snakeTransform.position += Vector3.forward;                    
                    break;
                case Direction.West:
                    if (Math.Abs(snakeTransform.position.x - firstBorderTransform.position.x) < 1.25f)
                        snakeTransform.position = new Vector3(lastBorderTransform.position.x -1, snakeTransform.position.y, snakeTransform.position.z);
                    else
                        snakeTransform.position += Vector3.left;                    
                    break;
                case Direction.East:
                    if (Math.Abs(snakeTransform.position.x - lastBorderTransform.position.x) < 1.25f)
                        snakeTransform.position = new Vector3(firstBorderTransform.transform.position.x +1, snakeTransform.position.y, snakeTransform.position.z);
                    else
                        snakeTransform.position += Vector3.right;                       
                    break;
            }

            StartCoroutine(nameof(StartSnakeMovementControlCoroutine));
        }

        private void OnDestroy()
        {
            UnSubscribeToMovementInputs();
            UnSubscribeToHeadPartActions();
            UnSubscribeToBodyPartActions();
            StopAllCoroutines();
        }
    }
}
