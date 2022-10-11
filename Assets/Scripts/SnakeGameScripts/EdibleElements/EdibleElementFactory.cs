using System;
using System.Linq;
using UnityEngine;

namespace SnakeGameScripts.EdibleElements
{
    public class EdibleElementFactory : MonoBehaviour
    {
        [Serializable]
        private class MyEdibleColourHolder
        {
            public Color edibleColor;
        }
        
        [SerializeField]
        private MyEdibleColourHolder[] myEdibleColourHolders;

        public GameObject CreateRandomEdibleElement(GameObject edibleElementInstance,Transform edibleParent, Vector3 spawnPosition)
        {
            Array values = Enum.GetValues(typeof(EdibleElementsEnum));
            System.Random random = new System.Random();
            EdibleElementsEnum randomEdibleEnum = (EdibleElementsEnum)values.GetValue(random.Next(values.Length));
            var edibleGameObject = InstantiateEdible(edibleElementInstance, edibleParent, spawnPosition);

            switch (randomEdibleEnum)
            {
                case EdibleElementsEnum.IncreaseSpeed:
                    edibleGameObject.AddComponent<IncreaseSnakeLenghtEdible>().objectColor = myEdibleColourHolders[0].edibleColor;
                    break;
                case EdibleElementsEnum.DecreaseSpeed:
                    edibleGameObject.AddComponent<DecreaseSnakeLenghtEdible>().objectColor = myEdibleColourHolders[1].edibleColor;
                    break;
                case EdibleElementsEnum.SlowDownSnake:
                    edibleGameObject.AddComponent<SlowDownSnakeEdible>().objectColor = myEdibleColourHolders[2].edibleColor;
                    break;
                case EdibleElementsEnum.SpeedUpSnake:
                    edibleGameObject.AddComponent<SpeedUpSnakeEdible>().objectColor = myEdibleColourHolders[3].edibleColor;
                    break;
                case EdibleElementsEnum.ReverseSnake:
                    edibleGameObject.AddComponent<ReverseSneakEdible>().objectColor = myEdibleColourHolders[4].edibleColor;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return edibleGameObject;
        }

        protected GameObject InstantiateEdible(GameObject edibleElementInstance,Transform edibleParent, Vector3 spawnPosition) => Instantiate(edibleElementInstance, new Vector3(spawnPosition.x, 0.1f, spawnPosition.z),edibleElementInstance.transform.rotation,edibleParent);
    }
}
