using UnityEngine;

namespace IIMEngine.Effects
{
    public class EffectsController : MonoBehaviour
    {
        [SerializeField] private AEffect[] _effects;

        public T GetEffect<T>(string effectID) where T : AEffect
        {
            foreach (AEffect effect in _effects) {
                if (effect.EffectID == effectID) {
                    return effect as T;
                }
            }
            return null;
        }

        public void PlayEffect(string effectID, bool forceReset = false)
        {
            AEffect effect = GetEffect<AEffect>(effectID);
            if (effect == null) return;
            if (effect.IsRunning && forceReset) {
                effect.ResetEffect();
            }
            effect.Play();
        }

        public void StopEffect(string effectID)
        {
            AEffect effect = GetEffect<AEffect>(effectID);
            if (effect == null) return;
            effect.Stop();
        }
    }
}