using System.Collections;
using System.Threading;
using IIMEngine.Effects;
using UnityEngine;

namespace IIMEngine.Effects.Common
{
    public class EffectBounce : AEffect
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Object to Scale")]
        [SerializeField] private Transform _objectToScale;

        [Header("Modifiers")]
        [SerializeField] private AEffectModifierFloat _timeModifier;
        
        public Transform ObjectToScale => _objectToScale;

        [Header("Bounce")]
        [SerializeField] private float _bounceFactorX = 0.1f;
        [SerializeField] private float _bounceFactorY = 0.1f;
        [SerializeField] private float _bouncePeriod = 0.5f;
        [SerializeField] private AnimationCurve _bounceCurveX = AnimationCurve.Constant(0f, 1f, 0f);
        [SerializeField] private AnimationCurve _bounceCurveY = AnimationCurve.Constant(0f, 1f, 0f);

        public float BounceFactorX => _bounceFactorX;
        public float BounceFactorY => _bounceFactorY;
        
        [Header("Looping")]
        [SerializeField] private bool _isLooping = true;

        private Vector3 _scaleDelta = Vector3.zero;
        private float _timer = 0f;
        
        #pragma warning restore 0414
        #endregion

        protected override void OnEffectReset()
        {
            //Reset Timer
            _timer = 0f;
            //Remove scale delta from objectToScale localScale
            _objectToScale.localScale -= _scaleDelta;
            //Reset scale delta X/Y
            _scaleDelta.x = 0f;
            _scaleDelta.y = 0f;
        }

        protected override IEnumerator OnEffectEndCoroutine()
        {
            //TODO: Do not interrupt bouncing effect
            //Wait for bounce period
            //Remove scale delta from objectToScale localScale
            //Increment Timer with delta time
            //Calculating percentage between timer and bouncePeriod
            //Applying animation curve on percentage
            //Set scale delta X/Y according to percentage and bounceFactorX/bounceFactorY
            //Add scale delta from objectToScale localScale
            //Wait for next frame (with yield instruction)
            while (_timer < _bouncePeriod)
            {
                _objectToScale.localScale -= _scaleDelta;
                _timer += Time.deltaTime;
                float percentage = _timer / _bouncePeriod;
                _scaleDelta.x = _bounceFactorX * _bounceCurveX.Evaluate(percentage);
                _scaleDelta.y = _bounceFactorY * _bounceCurveY.Evaluate(percentage);
                _objectToScale.localScale += _scaleDelta;
                yield return null;
            }

            yield break;
        }

        protected override void OnEffectEnd()
        {
            //Reset Timer
            _timer = 0f;
            //Remove scale delta from objectToScale localScale
            _objectToScale.localScale -= _scaleDelta;
            //Reset scale delta X/Y
            _scaleDelta.x = 0f;
            _scaleDelta.y = 0f;
        }
        
        protected override void OnEffectUpdate()
        {
            //Remove scale delta from objectToScale localScale
            _objectToScale.localScale -= _scaleDelta;
            //Increment timer with delta time (bonus : Applying factor to deltaTime using timeModifier)
            _timer += Time.deltaTime * _timeModifier.GetValue();
            //If effect is looping, timer must loop between [0, bouncePeriod]
            if (_isLooping)
            {
                _timer = Mathf.Repeat(_timer, _bouncePeriod);
            }
            //Calculating percentage between timer and bouncePeriod
            float percentage = _timer / _bouncePeriod;
            //Set scale delta X/Y according to percentage and bounceFactorX/bounceFactorY
            _scaleDelta.x = _bounceFactorX * _bounceCurveX.Evaluate(percentage);
            _scaleDelta.y = _bounceFactorY * _bounceCurveY.Evaluate(percentage);
            //Add scale delta from objectToScale localScale
            _objectToScale.localScale += _scaleDelta;
        }
    }
}