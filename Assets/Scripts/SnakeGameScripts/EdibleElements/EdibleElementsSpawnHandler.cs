using System;
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
        
        private enum EdibleElements
        {
            IncreaseSpeed,
            DecreaseSpeed,
            SlowDownSnake,
            SpeedUpSnake,
            ReverseSnake
        }

        private void Start()
        {
            FindSnakeController();
            FindSnakeBodyParts();
            FindSlotObjects();
            SubscribeBodyPartOnInstantiate();
            SubscribeSnakeControllerGameOverAction();
            InvokeRepeating(nameof(CreateRandomEdibleElement),2f,5f);
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
            Array values = Enum.GetValues(typeof(EdibleElements));
            System.Random random = new System.Random();
            EdibleElements randomEdibleEnum = (EdibleElements)values.GetValue(random.Next(values.Length));
            switch (randomEdibleEnum)
            {
                case EdibleElements.IncreaseSpeed:
                    var edibleIncreaseSpeed =  Instantiate(edibleElementInstance, new Vector3(randomSlot.transform.position.x, 0.1f, randomSlot.transform.position.z),edibleElementInstance.transform.rotation,edibleParent.transform);
                    edibleIncreaseSpeed.AddComponent<IncreaseSnakeLenghtEdible>();
                    _edibleElements.Add(edibleIncreaseSpeed.gameObject);
                    break;
                case EdibleElements.DecreaseSpeed:
                    var edibleDecreaseSpeed =  Instantiate(edibleElementInstance, new Vector3(randomSlot.transform.position.x, 0.1f, randomSlot.transform.position.z),edibleElementInstance.transform.rotation,edibleParent.transform);
                    edibleDecreaseSpeed.AddComponent<DecreaseSnakeLenghtEdible>();
                    _edibleElements.Add(edibleDecreaseSpeed.gameObject);
                    break;
                case EdibleElements.SlowDownSnake:
                    var edibleSlowDownSnake =  Instantiate(edibleElementInstance, new Vector3(randomSlot.transform.position.x, 0.1f, randomSlot.transform.position.z),edibleElementInstance.transform.rotation,edibleParent.transform);
                    edibleSlowDownSnake.AddComponent<SlowDownSnakeEdible>();
                    _edibleElements.Add(edibleSlowDownSnake.gameObject);
                    break;
                case EdibleElements.SpeedUpSnake:
                    var edibleSpeedUpSnake =  Instantiate(edibleElementInstance, new Vector3(randomSlot.transform.position.x, 0.1f, randomSlot.transform.position.z),edibleElementInstance.transform.rotation,edibleParent.transform);
                    edibleSpeedUpSnake.AddComponent<SpeedUpSnakeEdible>();
                    _edibleElements.Add(edibleSpeedUpSnake.gameObject);
                    break;
                case EdibleElements.ReverseSnake:
                    var edibleReverseSnake =  Instantiate(edibleElementInstance, new Vector3(randomSlot.transform.position.x, 0.1f, randomSlot.transform.position.z),edibleElementInstance.transform.rotation,edibleParent.transform);
                    edibleReverseSnake.AddComponent<ReverseSneakEdible>();
                    _edibleElements.Add(edibleReverseSnake.gameObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
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
