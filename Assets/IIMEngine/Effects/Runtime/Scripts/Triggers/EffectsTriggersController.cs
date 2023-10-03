using UnityEngine;

namespace IIMEngine.Effects.Triggers
{
    public class EffectsTriggersController : MonoBehaviour
    {
        [Header("Triggers")]
        [SerializeField] private AEffectTrigger[] _triggers;

        private AEffect[] _effects;

        private void Awake()
        {
            _effects = GetComponents<AEffect>();

            foreach (AEffectTrigger trigger in _triggers) {
                trigger.OnTrigger += _OnEffectTrigger;
            }
        }

        private void _OnEffectTrigger(AEffectTrigger trigger)
        {
            foreach (AEffect effect in _effects) {
                effect.Play();
            }
        }
    }
}