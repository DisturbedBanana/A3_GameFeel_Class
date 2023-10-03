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
        }

        private void Update()
        {
            //TODO: call Play() method in attached playing effects if ALL conditions are valid
            //TODO: call Stop() method in attached non playing effects if conditions are not valid
        }
    }
}