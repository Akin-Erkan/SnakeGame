using System;

namespace SnakeGameScripts.SnakeScripts
{
    /// <summary>
    /// Snake body part handler.
    /// </summary>
    public class SnakeBodyPartHandler : SnakePartHandler
    {
        public int currentPartIndex;
        public static Action<SnakeBodyPartHandler> OnSnakeBodyInstantiated;

        protected override void Awake()
        {
            base.Awake();
            OnSnakeBodyInstantiated?.Invoke(this);
        }
        
    }
}
