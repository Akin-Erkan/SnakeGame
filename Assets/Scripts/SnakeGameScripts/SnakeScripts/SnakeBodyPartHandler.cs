using System;
using SnakeGameScripts.LevelControls;
using UnityEngine;

namespace SnakeGameScripts.SnakeScripts
{
    /// <summary>
    /// Snake body part handler.
    /// </summary>
    public class SnakeBodyPartHandler : SnakePartHandler
    {
        public int currentPartIndex;
        public static Action<SnakeBodyPartHandler> OnSnakeBodyInstantiated;
        private SlotObject _lastSlotObject;
        protected override void Awake()
        {
            base.Awake();
            OnSnakeBodyInstantiated?.Invoke(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            var slot = other.GetComponent<SlotObject>();
            if (slot)
            {
                if (_lastSlotObject)
                    _lastSlotObject.isEmpty = true;
                _lastSlotObject = slot;
                _lastSlotObject.isEmpty = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_lastSlotObject)
                _lastSlotObject.isEmpty = true;
            
        }
    }
}
