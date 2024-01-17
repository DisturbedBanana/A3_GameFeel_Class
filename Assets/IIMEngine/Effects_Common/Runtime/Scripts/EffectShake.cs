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
            _timer = 0f;
            //Remove position delta from objectToShake localPosition
            _objectToShake.localPosition -= _positionDelta;
            //Reset position delta X/Y
            _positionDelta.x = 0f;
            _positionDelta.y = 0f;
        }

        protected override void OnEffectUpdate()
        {
            //Remove position delta from objectToShake localPosition
            _objectToShake.localPosition -= _positionDelta;
            //Increment timer with delta time
            _timer += Time.deltaTime;
            //Calculating percentage between timer and shakePeriod (using Mathf.PingPong)
            float percentage = Mathf.PingPong(_timer, _shakePeriod) / _shakePeriod;
            //Set positionDelta X/Y according to percentage and shakePowerX/shakePowerY
            _positionDelta.x = _shakePowerX * percentage;
            _positionDelta.y = _shakePowerY * percentage;
            //Add position Delta to objectToShake localPosition
            _objectToShake.localPosition += _positionDelta;
        }

        protected override void OnEffectEnd()
        {
            //Reset Timer
            _timer = 0f;
            //Remove position delta from objectToShake localPosition
            _objectToShake.localPosition -= _positionDelta;
            //Reset position delta X/Y            
            _positionDelta.x = 0f;
            _positionDelta.y = 0f;
        }
    }
}