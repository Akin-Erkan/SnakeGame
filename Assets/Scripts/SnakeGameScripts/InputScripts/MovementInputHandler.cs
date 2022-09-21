using System;
using SnakeGameScripts.SnakeScripts;
using UnityEngine;
using UnityEngine.UI;

namespace SnakeGameScripts.InputScripts
{
    public class MovementInputHandler : MonoBehaviour
    {
        
        public delegate void OnDirectionButtonClick(SnakeController.Direction headMovementDirection);

        public event OnDirectionButtonClick OnUpDirectionButtonClickEvent;
        public event OnDirectionButtonClick OnDownDirectionButtonClickEvent;
        public event OnDirectionButtonClick OnLeftDirectionButtonClickEvent;
        public event OnDirectionButtonClick OnRightDirectionButtonClickEvent;
        
        
        [SerializeField]
        private Button upButton;
        [SerializeField]
        private Button downButton;
        [SerializeField]
        private Button leftButton;
        [SerializeField]
        private Button rightButton;

        private void Awake()
        {
            SubscribeToActionsWithButtons();
        }

        private void SubscribeToActionsWithButtons()
        {
            upButton.onClick.AddListener(OnUpButtonClick);
            downButton.onClick.AddListener(OnDownButtonClick);
            leftButton.onClick.AddListener(OnLeftButtonClick);
            rightButton.onClick.AddListener(OnRightButtonClick); 
        }
        
        private void UnSubscribeToActionsWithButtons()
        {
            upButton.onClick.RemoveListener(OnUpButtonClick);
            downButton.onClick.RemoveListener(OnDownButtonClick);
            leftButton.onClick.RemoveListener(OnLeftButtonClick);
            rightButton.onClick.RemoveListener(OnRightButtonClick);
        }
        

        private void OnUpButtonClick() => OnUpDirectionButtonClickEvent?.Invoke(SnakeController.Direction.North);
        private void OnDownButtonClick() => OnDownDirectionButtonClickEvent?.Invoke(SnakeController.Direction.South);
        private void OnLeftButtonClick() => OnLeftDirectionButtonClickEvent?.Invoke(SnakeController.Direction.West);
        private void OnRightButtonClick() => OnRightDirectionButtonClickEvent?.Invoke(SnakeController.Direction.East);
        
        private void OnDestroy()
        {
            UnSubscribeToActionsWithButtons();
        }

    }
}
