using System;
using UnityEngine;

namespace IIMEngine.Effects
{
    public abstract class AEffectCondition : MonoBehaviour
    {
        private void Awake()
        {
            ConditionInit();
        }

        public void ConditionInit()
        {
            OnConditionInit();
        }

        public abstract bool IsValid();

        protected virtual void OnConditionInit() { }
    }
}