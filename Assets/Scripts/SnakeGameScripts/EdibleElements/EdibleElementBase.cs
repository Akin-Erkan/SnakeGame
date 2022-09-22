using UnityEngine;

namespace SnakeGameScripts.EdibleElements
{
    public class EdibleElementBase : MonoBehaviour
    {
        protected virtual void Awake()
        {
            
        }

        [SerializeField]
        protected Color partColor;
    
        protected virtual void OnEnable()
        {
            OnEnableSetColor();
        }


        protected virtual void OnEnableSetColor()
        {
            var mat = GetComponent<MeshRenderer>().material;
            mat.color = partColor;
        }

    }
}
