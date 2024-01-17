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
            _effects = GetComponents<AEffect>();
            foreach (AEffectCondition condition in _conditions)
            {
                condition.ConditionInit();
            }
       
            
        }

        private void Update()
        {
            //TODO: call Play() method in attached playing effects if ALL conditions are valid
            bool areAllConditionsMet = true;
            int i = 0;
            while (areAllConditionsMet && i < _conditions.Length)
            {
                areAllConditionsMet = _conditions[i].IsValid();
                i++;
            }
            
            if (areAllConditionsMet)
            {
                foreach (var effect in _effects)
                {
                    effect.Play();
                }
            }
            
            //TODO: call Stop() method in attached non playing effects if conditions are not valid
            else
            {
                foreach (var effect in _effects)
                {
                    effect.Stop();
                }
            }
        }
    }
}