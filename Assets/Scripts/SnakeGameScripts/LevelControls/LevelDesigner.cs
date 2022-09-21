using UnityEngine;

namespace SnakeGameScripts.LevelControls
{
    public class LevelDesigner : MonoBehaviour
    {
        [Range(8,64)]
        public int playAreaWidth = 32;
        [Range(8,64)]
        public int playAreaHeight = 32;

        [SerializeField] 
        private GameObject borderParent;
        [SerializeField] 
        private GameObject borderGameObject;
    
        void Awake()
        {
            CreateBorders();
        }

        private void CreateBorders()
        {
            CreateLeftVerticalBorder();
            CreateRightVerticalBorder();
            CreateUpperHorizontalBorder();
            CreateLowerHorizontalBorder();
        }

        private void CreateLeftVerticalBorder()
        {
            for (int i = 0; i < playAreaHeight/2+1; i++)
            {
                Instantiate(borderGameObject, new Vector3(-playAreaWidth/2,0.1f,i),borderGameObject.transform.rotation,borderParent.transform);
            } 
            for (int i = 0; i < playAreaHeight/2+1; i++)
            {
                Instantiate(borderGameObject, new Vector3(-playAreaWidth/2,0.1f,-i),borderGameObject.transform.rotation,borderParent.transform);
            } 
        }
    
        private void CreateRightVerticalBorder()
        {
            for (int i = 0; i < playAreaHeight/2+1; i++)
            {
                Instantiate(borderGameObject, new Vector3(playAreaWidth/2,0.1f,i),borderGameObject.transform.rotation,borderParent.transform);
            } 
            for (int i = 0; i < playAreaHeight/2+1; i++)
            {
                Instantiate(borderGameObject, new Vector3(playAreaWidth/2,0.1f,-i),borderGameObject.transform.rotation,borderParent.transform);
            } 
        }
    
        private void CreateUpperHorizontalBorder()
        {
            for (int i = 0; i < playAreaWidth/2; i++)
            {
                Instantiate(borderGameObject, new Vector3(i,0.1f,playAreaHeight/2),borderGameObject.transform.rotation,borderParent.transform);
            } 
            for (int i = 0; i < playAreaWidth/2; i++)
            {
                Instantiate(borderGameObject, new Vector3(-i,0.1f,playAreaHeight/2),borderGameObject.transform.rotation,borderParent.transform);
            } 
        }
    
        private void CreateLowerHorizontalBorder()
        {
            for (int i = 0; i < playAreaWidth/2; i++)
            {
                Instantiate(borderGameObject, new Vector3(i,0.1f,-playAreaHeight/2),borderGameObject.transform.rotation,borderParent.transform);
            } 
            for (int i = 0; i < playAreaWidth/2; i++)
            {
                Instantiate(borderGameObject, new Vector3(-i,0.1f,-playAreaHeight/2),borderGameObject.transform.rotation,borderParent.transform);
            } 
        }
    
    }
}
