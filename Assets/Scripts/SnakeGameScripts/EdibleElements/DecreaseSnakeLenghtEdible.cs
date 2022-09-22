using UnityEngine;

namespace SnakeGameScripts.EdibleElements
{
    public class DecreaseSnakeLenghtEdible : EdibleElementBase
    {
        protected override void Awake()
        {
            base.Awake();
            partColor = Color.red;
        }
    }
}
