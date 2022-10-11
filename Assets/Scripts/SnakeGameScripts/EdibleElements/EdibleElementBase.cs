using SnakeGameScripts.Generic.Interfaces;
using UnityEngine;

namespace SnakeGameScripts.EdibleElements
{
    public class EdibleElementBase : MonoBehaviour, IColourObject
    {
        public Color objectColor;
        protected virtual void Start()
        {
            var mat = GetComponent<MeshRenderer>().material;
            SetObjectColor(mat,objectColor);
        }

        
        public void SetObjectColor(Material mat, Color color)
        {
            mat.color = color;
            print("Setting colour to: " + color);
        }
    }
}
