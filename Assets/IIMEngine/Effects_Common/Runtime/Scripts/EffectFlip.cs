using UnityEngine;

namespace IIMEngine.Effects.Common
{
    public class EffectFlip : AEffect
    {
        [Header("Object to Flip")]
        [SerializeField] private Transform _objectToFlip;
        private float _objectScaleStartX = 1f;

        [Header("Orient")]
        [SerializeField] private AEffectModifierFloat _orientModifier;

        [Header("Flip")]
        [SerializeField] private float _flipDuration = 0.5f;

        private float _timer = 0f;
        private float _flipScaleStartX = 1f;
        private float _flipScaleEndX = 0f;

        protected override void OnEffectInit()
        {
            _objectScaleStartX = _objectToFlip.localScale.x;
        }

        protected override void OnEffectStart()
        {
            _flipScaleStartX = _objectToFlip.localScale.x;
            _flipScaleEndX = _objectScaleStartX * Mathf.Sign(_orientModifier.GetValue());
            _timer = 0f;
        }

        protected override void OnEffectUpdate()
        {
            _timer += Time.deltaTime;
            if (_timer >= _flipDuration) {
                Stop();
                return;
            }

            Vector3 newScale = _objectToFlip.localScale;
            float ratio = Mathf.Clamp01(_timer / _flipDuration);
            newScale.x = Mathf.Lerp(_flipScaleStartX, _flipScaleEndX, ratio);
            _objectToFlip.localScale = newScale;
        }

        protected override void OnEffectEnd()
        {
            Vector3 newScale = _objectToFlip.localScale;
            newScale.x = _flipScaleEndX;
            _objectToFlip.localScale = newScale;
        }
    }
}