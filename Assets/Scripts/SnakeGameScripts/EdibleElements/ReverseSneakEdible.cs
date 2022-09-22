using UnityEngine;

namespace SnakeGameScripts.EdibleElements
{
    public class ReverseSneakEdible : EdibleElementBase
    {
        protected override void Awake()
        {
            base.Awake();
            partColor = Color.blue;
        }
    }
}
