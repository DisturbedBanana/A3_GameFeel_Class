using System;
using UnityEngine;

namespace IIMEngine.Effects.Triggers
{
    public class AEffectTrigger : MonoBehaviour
    {
        public Action<AEffectTrigger> OnTrigger { get; set; }

        private void Awake()
        {
            _TriggerInit();
        }

        private void _TriggerInit()
        {
            OnTriggerInit();
        }

        protected void InvokeTrigger()
        {
            OnTrigger?.Invoke(this);
        }

        protected virtual void OnTriggerInit() { }
    }
}