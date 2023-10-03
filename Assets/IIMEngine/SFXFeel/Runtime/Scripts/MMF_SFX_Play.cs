using MoreMountains.Feedbacks;
using UnityEngine;

namespace IIMEngine.SFX.Feel
{
    [AddComponentMenu("")]
    [FeedbackPath("SFX/SFX Play")]
    public class MMF_SFX_Play : MMF_Feedback
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [MMFInspectorGroup("SFX")]
        [SerializeField] private string _sfxName = "";
        
        #pragma warning restore 0414
        #endregion
        
        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            //TODO: Call SFXsManager PlaySound with _sfxName
        }
    }
}