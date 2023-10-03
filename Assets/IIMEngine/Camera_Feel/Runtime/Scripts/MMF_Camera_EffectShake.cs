using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace IIMEngine.Camera.Feel
{
    [AddComponentMenu("")]
    [FeedbackPath("Camera/Camera Effect Shake")]
    public class MMF_Camera_EffectShake : MMF_Feedback
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [MMFInspectorGroup("Shake", true)]
        [SerializeField] private float _shakeDuration = 1.0f;
        [SerializeField] private float _shakePower = 0.1f;
        [SerializeField] private float _shakePeriod = 0.05f;

        private CameraEffect _cameraEffect = new CameraEffect();

        #pragma warning restore 0414
        #endregion
        
        //TODO: Override FeedbackDuration Property (using _shakeDuration)

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            Owner.StartCoroutine(_CoroutineShake());
        }

        private IEnumerator _CoroutineShake()
        {
            //Add _cameraEffect into CameraEffects
            //Implements shake effects using _shakeDuration / _shakePeriod / _shakePower
            yield break;
        }
    }
}