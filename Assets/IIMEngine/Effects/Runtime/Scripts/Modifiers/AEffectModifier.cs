using UnityEngine;

namespace IIMEngine.Effects
{
    public abstract class AEffectModifierFloat : MonoBehaviour
    {
        private void Awake()
        {
            ModifierInit();
        }

        public void ModifierInit()
        {
            OnModifierInit();
        }

        public abstract float GetValue();
        
        protected virtual void OnModifierInit() { }
    }
}