using System;
using System.Collections.Generic;
using System.Linq;
using SnakeGameScripts.LevelControls;
using SnakeGameScripts.SnakeScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SnakeGameScripts.EdibleElements
{
    public class EdibleElementsSpawnHandler : MonoBehaviour
    {
        private List<SnakeBodyPartHandler> _snakeBodyPartHandlers = new();
        private SnakeHeadPartHandler _snakeHeadPartHandler;
        private LevelDesigner _levelDesigner;
        private List<GameObject> _edibleElements = new();

        [SerializeField]
        private GameObject edibleElementInstance;
        
        private enum EdibleElements
        {
            IncreaseSpeed,
            DecreaseSpeed,
            SlowDownSnake,
            SpeedUpSnake,
            ReverseSnake
        }

        private void Awake()
        {
            FindSnakeHead();
            FindSnakeBodyParts();
            FindLevelDesign();
            SubscribeBodyPartOnInstantiate();
            InvokeRepeating(nameof(CreateRandomEdibleElement),0,2f);
        }
        

        private void FindSnakeHead() => _snakeHeadPartHandler = FindObjectOfType<SnakeHeadPartHandler>();
        private void FindSnakeBodyParts() => _snakeBodyPartHandlers = FindObjectsOfType<SnakeBodyPartHandler>().OrderBy(sp =>sp.currentPartIndex).ToList();
        private void FindLevelDesign() => _levelDesigner = FindObjectOfType<LevelDesigner>();
        private void SubscribeBodyPartOnInstantiate() => SnakeBodyPartHandler.OnSnakeBodyInstantiated += AddBodyPartToList;
        private void UnSubscribeBodyPartOnInstantiate() => SnakeBodyPartHandler.OnSnakeBodyInstantiated -= AddBodyPartToList;


        private void AddBodyPartToList(SnakeBodyPartHandler obj)
        {
            if(!_snakeBodyPartHandlers.Contains(obj))
                _snakeBodyPartHandlers.Add(obj);
        }

        private void CreateRandomEdibleElement()
        {
            var randomVector2d = FindProperPositionForEdible();
            Array values = Enum.GetValues(typeof(EdibleElements));
            System.Random random = new System.Random();
            EdibleElements randomEdibleEnum = (EdibleElements)values.GetValue(random.Next(values.Length));
            switch (randomEdibleEnum)
            {
                case EdibleElements.IncreaseSpeed:
                    var edibleIncreaseSpeed =  Instantiate(edibleElementInstance, new Vector3(randomVector2d.x, 0.1f, randomVector2d.y),edibleElementInstance.transform.rotation);
                    edibleIncreaseSpeed.AddComponent<IncreaseSnakeLenghtEdible>();
                    _edibleElements.Add(edibleIncreaseSpeed.gameObject);
                    break;
                case EdibleElements.DecreaseSpeed:
                    var edibleDecreaseSpeed =  Instantiate(edibleElementInstance, new Vector3(randomVector2d.x, 0.1f, randomVector2d.y),edibleElementInstance.transform.rotation);
                    edibleDecreaseSpeed.AddComponent<DecreaseSnakeLenghtEdible>();
                    _edibleElements.Add(edibleDecreaseSpeed.gameObject);
                    break;
                case EdibleElements.SlowDownSnake:
                    var edibleSlowDownSnake =  Instantiate(edibleElementInstance, new Vector3(randomVector2d.x, 0.1f, randomVector2d.y),edibleElementInstance.transform.rotation);
                    edibleSlowDownSnake.AddComponent<SlowDownSnakeEdible>();
                    _edibleElements.Add(edibleSlowDownSnake.gameObject);
                    break;
                case EdibleElements.SpeedUpSnake:
                    var edibleSpeedUpSnake =  Instantiate(edibleElementInstance, new Vector3(randomVector2d.x, 0.1f, randomVector2d.y),edibleElementInstance.transform.rotation);
                    edibleSpeedUpSnake.AddComponent<SpeedUpSnakeEdible>();
                    _edibleElements.Add(edibleSpeedUpSnake.gameObject);
                    break;
                case EdibleElements.ReverseSnake:
                    var edibleReverseSnake =  Instantiate(edibleElementInstance, new Vector3(randomVector2d.x, 0.1f, randomVector2d.y),edibleElementInstance.transform.rotation);
                    edibleReverseSnake.AddComponent<ReverseSneakEdible>();
                    _edibleElements.Add(edibleReverseSnake.gameObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private Vector2 FindProperPositionForEdible()
        {
            var inSlotGameobjectPoints = new List<Vector2>();
            List<GameObject> inSlotGameobjects = new List<GameObject>();
            _snakeBodyPartHandlers.ForEach(x=> inSlotGameobjects.Add(x.gameObject));
            _edibleElements.ForEach(x=> inSlotGameobjects.Add(x.gameObject));
            inSlotGameobjects.Add(_snakeHeadPartHandler.gameObject);
            
            for (int i = 0; i < inSlotGameobjects.Count; i++)
            {
                inSlotGameobjectPoints.Add(new Vector2(inSlotGameobjects[i].transform.position.x, inSlotGameobjects[i].transform.position.z));
            }

            var randomVector2d = GetRandomPosition();
            foreach (var sneakPartPoint in inSlotGameobjectPoints)
            {
                while (sneakPartPoint.Equals(randomVector2d))
                {
                    randomVector2d = GetRandomPosition();
                }
            }
            return randomVector2d;
        }

        private Vector2 GetRandomPosition()
        {
            var randomVector2d = new Vector2(0, 0);
            var randomX = Random.Range(-_levelDesigner.playAreaWidth/2+1,_levelDesigner.playAreaWidth/2-1);
            var randomZ = Random.Range(-_levelDesigner.playAreaHeight/2+1,_levelDesigner.playAreaHeight/2-1);
            randomVector2d = new Vector2(randomX, randomZ);
            return randomVector2d;
        }

        private void OnDestroy()
        {
            UnSubscribeBodyPartOnInstantiate();
        }
    }
}
