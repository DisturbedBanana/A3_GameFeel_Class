using UnityEngine;

namespace IIMEngine.Effects.Common
{
    public class EffectShake : AEffect
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414

        [SerializeField] private Transform _objectToShake;

        [SerializeField] private float _shakePowerX = 0.1f;
        [SerializeField] private float _shakePowerY = 0.1f;
        [SerializeField] private float _shakePeriod = 0.1f;

        private Vector3 _positionDelta = Vector3.zero;

        private float _timer = 0f;
        
        #pragma warning restore 0414
        #endregion

        protected override void OnEffectStart()
        {
            //Reset Timer
            //Remove position delta from objectToShake localPosition
            //Reset position delta X/Y
        }

        protected override void OnEffectUpdate()
        {
            //Remove position delta from objectToShake localPosition
            //Increment timer with delta time
            //Calculating percentage between timer and shakePeriod (using Mathf.PingPong)
            //Set positionDelta X/Y according to percentage and shakePowerX/shakePowerY
            //Add position Delta to objectToShake localPosition
        }

        protected override void OnEffectEnd()
        {
            //Reset Timer
            //Remove position delta from objectToShake localPosition
            //Reset position delta X/Y            
        }
    }
}