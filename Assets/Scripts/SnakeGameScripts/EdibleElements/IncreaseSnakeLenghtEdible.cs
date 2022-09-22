using UnityEngine;

namespace SnakeGameScripts.EdibleElements
{
    public class IncreaseSnakeLenghtEdible : EdibleElementBase
    {
        protected override void Awake()
        {
            base.Awake();
            partColor = Color.green;
        }
    }
}
