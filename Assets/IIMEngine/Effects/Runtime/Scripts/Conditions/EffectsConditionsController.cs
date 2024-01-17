using UnityEngine;

namespace IIMEngine.Effects
{
    public class EffectsConditionsController : MonoBehaviour
    {
        #region DO NOT MODIFY
        
        [Header("Conditions")]
        [SerializeField] private AEffectCondition[] _conditions;

        private AEffect[] _effects;
        
        #endregion

        private void Awake()
        {
            //Find All effects attached to this gameObject
            //Call ConditionInit() method for all conditions stored
            foreach (AEffectCondition condition in _conditions)
            {
                condition.ConditionInit();
            }
       
            
        }

        private void Update()
        {
            int invalidConditions = 0;
            //TODO: call Play() method in attached playing effects if ALL conditions are valid
            foreach (AEffectCondition condition in _conditions)
            {
                switch (condition.IsValid())
                {
                    case true:
                        break;
                    case false:
                        invalidConditions++;
                        break;
                }
            }
            if (invalidConditions > 0)
            {
                foreach (AEffect effect in _effects)
                {
                    effect.Stop();
                }
            }
            else
            {
                foreach (AEffect effect in _effects)
                {
                    effect.Play();
                }
            }
            //TODO: call Stop() method in attached non playing effects if conditions are not valid
        }
    }
}