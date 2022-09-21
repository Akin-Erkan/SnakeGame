using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using SnakeGameScripts.InputScripts;
using SnakeGameScripts.LevelControls;

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
        
        [SerializeField]
        private Direction movementStartDirection = Direction.East;
        private Direction _headDirection;
        private Direction _tailDirection;

        private SnakeHeadPartHandler _snakeHeadPartHandler;
        private List<SnakeBodyPartHandler> _snakeBodyPartHandlers;

        private MovementInputHandler _movementInputHandler;
        private LevelDesigner _levelDesigner;

        private float _currentSnakeSpeed = 0.5f;
        
        private void Awake()
        {
            FindSnakeHead();
            FindSnakeBodyParts();
            FindMovementInputHandler();
            FindLevelDesign();
            SetStartBodyDirections(movementStartDirection);
            SubscribeToMovementInputs();
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

        private void SetHeadDirection(Direction headMovementDirection)
        {
            _headDirection = headMovementDirection;
        }


        private IEnumerator StartSnakeMovementControlCoroutine()
        {
            yield return new WaitForSeconds(_currentSnakeSpeed);
            if (_snakeBodyPartHandlers.Count > 2)
            {
                for (int i = _snakeBodyPartHandlers.Count-1; i > 0; i--)
                {
                    _snakeBodyPartHandlers[i].transform.position = _snakeBodyPartHandlers[i - 1].transform.position;
                }
            }
            if (_snakeBodyPartHandlers[0])
                _snakeBodyPartHandlers[0].transform.position = _snakeHeadPartHandler.transform.position;
            switch (_headDirection)
            {
                case Direction.South:
                    if ((Math.Abs(_snakeHeadPartHandler.transform.position.z - _levelDesigner.lowerBorderRestrictionPoint) < 1.25f))
                        _snakeHeadPartHandler.transform.position = new Vector3(_snakeHeadPartHandler.transform.position.x, _snakeHeadPartHandler.transform.position.y, _levelDesigner.upperBorderRestrictionPoint - 1);
                    else
                        _snakeHeadPartHandler.transform.position += Vector3.back;
                    break;
                case Direction.North:
                    if ((Math.Abs(_snakeHeadPartHandler.transform.position.z - _levelDesigner.upperBorderRestrictionPoint) < 1.25f))
                        _snakeHeadPartHandler.transform.position = new Vector3(_snakeHeadPartHandler.transform.position.x,  _snakeHeadPartHandler.transform.position.y, _levelDesigner.lowerBorderRestrictionPoint + 1);
                    else
                        _snakeHeadPartHandler.transform.position += Vector3.forward;                    
                    break;
                case Direction.West:
                    if (Math.Abs(_snakeHeadPartHandler.transform.position.x - _levelDesigner.leftBorderRestrictionPoint) < 1.25f)
                        _snakeHeadPartHandler.transform.position = new Vector3(_levelDesigner.rightBorderRestrictionPoint-1, _snakeHeadPartHandler.transform.position.y, _snakeHeadPartHandler.transform.position.z);
                    else
                        _snakeHeadPartHandler.transform.position += Vector3.left;                    
                    break;
                case Direction.East:
                    if (Math.Abs(_snakeHeadPartHandler.transform.position.x - _levelDesigner.rightBorderRestrictionPoint) < 1.25f)
                        _snakeHeadPartHandler.transform.position = new Vector3(_levelDesigner.leftBorderRestrictionPoint+1, _snakeHeadPartHandler.transform.position.y, _snakeHeadPartHandler.transform.position.z);
                    else
                        _snakeHeadPartHandler.transform.position += Vector3.right;                       
                    break;
            }

            StartCoroutine(nameof(StartSnakeMovementControlCoroutine));
        }

        private void OnDestroy()
        {
            UnSubscribeToMovementInputs();
            StopAllCoroutines();
        }
    }
}
