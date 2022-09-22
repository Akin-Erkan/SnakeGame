using UnityEngine;

namespace SnakeGameScripts.EdibleElements
{
    public class SlowDownSnakeEdible : EdibleElementBase
    {
        protected override void Awake()
        {
            base.Awake();
            partColor = Color.yellow;
        }
    }
}
