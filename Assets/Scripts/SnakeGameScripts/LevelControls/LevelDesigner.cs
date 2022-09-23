using System.Collections.Generic;
using UnityEngine;

namespace SnakeGameScripts.LevelControls
{
    public class LevelDesigner : MonoBehaviour
    {
        [Range(8,64)]
        public int playAreaWidth = 32;
        [Range(8,64)]
        public int playAreaHeight = 32;

        public List<BorderObject> borderList = new();
        public List<SlotObject> slotList = new();
        [SerializeField] 
        private GameObject borderParent;
        [SerializeField] 
        private BorderObject borderGameObject;
        [SerializeField]
        private GameObject slotParent;
        [SerializeField] 
        private SlotObject slotGameObject;
        
        void Awake()
        {
            CreateLevel();
        }
        

        private void CreateLevel()
        {
            for (int i = 0; i < playAreaWidth ; i++)
            {
                for (int j = 0; j < playAreaHeight ; j++)
                {
                    if (i == 0 || i == playAreaWidth - 1 || j==0 || j == playAreaHeight-1)
                    {
                        var borderGameobject = Instantiate(borderGameObject, new Vector3(i,0f,j),borderGameObject.transform.rotation,borderParent.transform);
                        borderGameobject.borderPoint = new Vector2(i, j);
                        borderGameobject.name = "Border " + i + " - " + j;
                        borderList.Add(borderGameobject);
                    }
                    else
                    {
                        var slotGameobject = Instantiate(slotGameObject, new Vector3(i,0f,j),slotGameObject.transform.rotation,slotParent.transform);
                        slotGameobject.slotPoint = new Vector2(i, j);
                        slotGameobject.name = "Slot " + i + " - " + j;
                        slotList.Add(slotGameobject);
                    }
                }
            }
        }
        
    
    }
}
