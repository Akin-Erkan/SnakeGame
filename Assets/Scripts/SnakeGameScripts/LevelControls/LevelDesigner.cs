using UnityEngine;

namespace SnakeGameScripts.LevelControls
{
    public class LevelDesigner : MonoBehaviour
    {
        [Range(8,64)]
        public int playAreaWidth = 32;
        [Range(8,64)]
        public int playAreaHeight = 32;

        [HideInInspector]
        public float leftBorderRestrictionPoint;
        [HideInInspector]
        public float rightBorderRestrictionPoint;
        [HideInInspector]
        public float upperBorderRestrictionPoint;
        [HideInInspector]
        public float lowerBorderRestrictionPoint;


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
            leftBorderRestrictionPoint = -playAreaWidth / 2;
            for (int i = 0; i < playAreaHeight/2+1; i++)
            {
                Instantiate(borderGameObject, new Vector3(leftBorderRestrictionPoint,0.1f,i),borderGameObject.transform.rotation,borderParent.transform);
            } 
            for (int i = 0; i < playAreaHeight/2+1; i++)
            {
                Instantiate(borderGameObject, new Vector3(leftBorderRestrictionPoint,0.1f,-i),borderGameObject.transform.rotation,borderParent.transform);
            } 
        }
    
        private void CreateRightVerticalBorder()
        {
            rightBorderRestrictionPoint = playAreaWidth / 2;
            for (int i = 0; i < playAreaHeight/2+1; i++)
            {
                Instantiate(borderGameObject, new Vector3(rightBorderRestrictionPoint,0.1f,i),borderGameObject.transform.rotation,borderParent.transform);
            } 
            for (int i = 0; i < playAreaHeight/2+1; i++)
            {
                Instantiate(borderGameObject, new Vector3(rightBorderRestrictionPoint,0.1f,-i),borderGameObject.transform.rotation,borderParent.transform);
            } 
        }
    
        private void CreateUpperHorizontalBorder()
        {
            upperBorderRestrictionPoint = playAreaHeight / 2;
            for (int i = 0; i < playAreaWidth/2; i++)
            {
                Instantiate(borderGameObject, new Vector3(i,0.1f,upperBorderRestrictionPoint),borderGameObject.transform.rotation,borderParent.transform);
            } 
            for (int i = 0; i < playAreaWidth/2; i++)
            {
                Instantiate(borderGameObject, new Vector3(-i,0.1f,upperBorderRestrictionPoint),borderGameObject.transform.rotation,borderParent.transform);
            } 
        }
    
        private void CreateLowerHorizontalBorder()
        {
            lowerBorderRestrictionPoint = -playAreaHeight / 2;
            for (int i = 0; i < playAreaWidth/2; i++)
            {
                Instantiate(borderGameObject, new Vector3(i,0.1f,lowerBorderRestrictionPoint),borderGameObject.transform.rotation,borderParent.transform);
            } 
            for (int i = 0; i < playAreaWidth/2; i++)
            {
                Instantiate(borderGameObject, new Vector3(-i,0.1f,lowerBorderRestrictionPoint),borderGameObject.transform.rotation,borderParent.transform);
            } 
        }
    
    }
}
