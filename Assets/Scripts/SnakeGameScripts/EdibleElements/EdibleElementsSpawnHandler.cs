using System.Collections.Generic;
using System.Linq;
using SnakeGameScripts.LevelControls;
using SnakeGameScripts.SnakeScripts;
using UnityEngine;

namespace SnakeGameScripts.EdibleElements
{
    public class EdibleElementsSpawnHandler : MonoBehaviour
    {
        private List<SnakeBodyPartHandler> _snakeBodyPartHandlers = new();
        private List<GameObject> _edibleElements = new();
        private List<SlotObject> _slotObjects = new();
        [SerializeField]
        private GameObject edibleElementInstance;
        [SerializeField]
        private GameObject edibleParent;

        private SnakeController _snakeController;

        private void Start()
        {
            FindSnakeController();
            FindSnakeBodyParts();
            FindSlotObjects();
            SubscribeBodyPartOnInstantiate();
            SubscribeSnakeControllerGameOverAction();
            InvokeRepeating(nameof(CreateRandomEdibleElement),2f,2f);
        }
        

        private void FindSnakeController() => _snakeController = FindObjectOfType<SnakeController>();

        private void FindSnakeBodyParts() => _snakeBodyPartHandlers = FindObjectsOfType<SnakeBodyPartHandler>().OrderBy(sp =>sp.currentPartIndex).ToList();
        private void FindSlotObjects() => _slotObjects = FindObjectsOfType<SlotObject>().ToList();
        private void SubscribeBodyPartOnInstantiate() => SnakeBodyPartHandler.OnSnakeBodyInstantiated += AddBodyPartToList;
        private void UnSubscribeBodyPartOnInstantiate() => SnakeBodyPartHandler.OnSnakeBodyInstantiated -= AddBodyPartToList;
        private void SubscribeSnakeControllerGameOverAction() => _snakeController.GameOverAction += OnGameOver;

        private void UnSubscribeSnakeControllerGameOverAction() => _snakeController.GameOverAction -= OnGameOver;
        
        private void OnGameOver()
        {
            CancelInvoke();
        }



        private void AddBodyPartToList(SnakeBodyPartHandler obj)
        {
            if(!_snakeBodyPartHandlers.Contains(obj))
                _snakeBodyPartHandlers.Add(obj);
        }

        private void CreateRandomEdibleElement()
        {
            var randomSlot = FindProperPositionForEdible();
            randomSlot.isEmpty = false;
            var edibleFactory = FindObjectOfType<EdibleElementFactory>();
            if (edibleFactory)
                _edibleElements.Add(edibleFactory.CreateRandomEdibleElement(edibleElementInstance, edibleParent.transform, randomSlot.transform.position));
        }

        private SlotObject FindProperPositionForEdible()
        {
            var emptySlots = _slotObjects.Where(s => s.isEmpty).ToList();
            var random = UnityEngine.Random.Range(0, emptySlots.Count - 1);
            return emptySlots[random];
        }

        private void OnDestroy()
        {
            UnSubscribeBodyPartOnInstantiate();
            UnSubscribeSnakeControllerGameOverAction();
        }
    }
}
