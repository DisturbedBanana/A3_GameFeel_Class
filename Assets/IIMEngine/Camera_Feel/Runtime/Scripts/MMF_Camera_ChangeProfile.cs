using MoreMountains.Feedbacks;
using UnityEngine;

namespace IIMEngine.Camera.Feel
{
    [AddComponentMenu("")]
    [FeedbackPath("Camera/Camera Change Profile")]
    public class MMF_Camera_ChangeProfile : MMF_Feedback
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [MMFInspectorGroup("Profile", true)]
        [SerializeField] private CameraProfile _profile;
        
        [MMFInspectorGroup("Transition", true)]
        [SerializeField] private CameraProfileTransition _transition;
        
        #pragma warning restore 0414
        #endregion

        //TODO: Override FeedbackDuration Property (using _transition)
        //Don't forget to check if transition is null (can be null when adding effect at the first time in the inspector)

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            //TODO: Change Camera Profile (with transition)
        }
    }
}