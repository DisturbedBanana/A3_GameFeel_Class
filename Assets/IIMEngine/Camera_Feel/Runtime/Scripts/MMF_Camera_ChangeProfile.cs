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

        public override float FeedbackDuration => (_transition != null) ? _transition.Duration : base.FeedbackDuration;

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            //TODO: Change Camera Profile (with transition)
            if (_profile != null)
            {
                // Create a new CameraProfileManager reference (replace with your actual manager class)
                CameraProfilesManager profilesManager = CameraGlobals.Profiles;

                if (profilesManager != null)
                {
                    // Start the transition coroutine
                    profilesManager.SetProfileAndWaitForTransition(_profile, _transition);
                }
                else
                {
                    Debug.LogError("Camera Profiles Manager is not assigned or available.");
                }
            }
            else
            {
                Debug.LogWarning("Camera Profile is not assigned.");
            }
        }
    }
}