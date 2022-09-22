using UnityEngine;

namespace SnakeGameScripts.EdibleElements
{
    public class SpeedUpSnakeEdible : EdibleElementBase
    {
        protected override void Awake()
        {
            base.Awake();
            partColor = Color.cyan;
        }
    }
}
