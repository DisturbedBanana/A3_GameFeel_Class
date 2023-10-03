using System.Collections;
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
            //Remove scale delta from objectToScale localScale
            //Reset scale delta X/Y
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
            
            yield break;
        }

        protected override void OnEffectEnd()
        {
            //Reset Timer
            //Remove scale delta from objectToScale localScale
            //Reset scale delta X/Y
        }
        
        protected override void OnEffectUpdate()
        {
            //Remove scale delta from objectToScale localScale
            //Increment timer with delta time (bonus : Applying factor to deltaTime using timeModifier)
            //If effect is looping, timer must loop between [0, bouncePeriod]
            //Calculating percentage between timer and bouncePeriod
            //Set scale delta X/Y according to percentage and bounceFactorX/bounceFactorY
            //Add scale delta from objectToScale localScale
        }
    }
}