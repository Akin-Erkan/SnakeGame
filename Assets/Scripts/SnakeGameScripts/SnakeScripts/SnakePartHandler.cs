using UnityEngine;

namespace SnakeGameScripts.SnakeScripts
{
    public class SnakePartHandler : MonoBehaviour
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

        /// <summary>
        /// We are setting body color whenever enabled because we need to change head and tail in gameplay.
        /// </summary>
        protected virtual void OnEnableSetColor()
        {
            var mat = GetComponent<MeshRenderer>().material;
            mat.color = partColor;
        }
    }
}
