using System.Collections;
using IIMEngine.Effects;
using UnityEngine;

namespace IIMEngine.Effects.Common
{
    public class EffectJump : AEffect
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414

        [Header("Object to Move")]
        [SerializeField] private Transform _objectToMove;

        public Transform ObjectToMove => _objectToMove;

        [Header("Modifiers")]
        [SerializeField] private AEffectModifierFloat _timeModifier;

        [Header("Jump")]
        [SerializeField] private float _jumpHeight = 3f;
        [SerializeField] private float _jumpPeriod = 0.5f;
        [SerializeField] private AnimationCurve _jumpCurve = AnimationCurve.Constant(0f, 1f, 0f);

        public float JumpHeight => _jumpHeight;
        
        [Header("Looping")]
        [SerializeField] private bool _isLooping = true;

        private Vector3 _positionDelta = Vector3.zero;
        private float _timer = 0f;
        
        #pragma warning restore 0414
        #endregion

        protected override void OnEffectReset()
        {
            //Reset Timer
            //Remove position delta from objectToMove localPosition
            //Reset position delta Y
        }

        protected override IEnumerator OnEffectEndCoroutine()
        {
            //TODO: Do not interrupt current jump
            //Wait for jump period (while loop)
                //Remove position delta from objectToMove localPosition
                //Increment Timer with delta time
                //Calculating percentage between timer and jumpPeriod
                //Applying animation curve on percentage
                //Set positionDelta Y according to percentage and jumpHeight
                //Add position Delta to objectToMove localPosition
                //Wait for next frame (with yield instruction)
            yield break;
        }
        
        protected override void OnEffectEnd()
        {
            //Reset Timer
            //Remove position delta from objectToMove localPosition
            //Reset position delta Y
        }

        protected override void OnEffectUpdate()
        {
            //Remove position delta from objectToMove localPosition
            //Increment timer with delta time (bonus : Applying factor to deltaTime using timeModifier)
            //If effect is looping, timer must loop between [0, jumpPeriod]
            //Calculating percentage between timer and jumpPeriod
            //Set positionDelta Y according to percentage and jumpHeight
            //Add position Delta to objectToMove localPosition
        }
    }
}