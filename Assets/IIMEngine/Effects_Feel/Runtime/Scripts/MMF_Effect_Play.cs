using IIMEngine.Entities.Target;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace IIMEngine.Effects.Feel
{
    [AddComponentMenu("")]
    [FeedbackPath("Effects/Effect Play")]
    public class MMF_Effect_Play : MMF_Feedback
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [MMFInspectorGroup("Target", true)]
        [SerializeField] private EntityTarget _target;
        private EffectsController[] _effectsControllers = null;

        [MMFInspectorGroup("Effect", true)]
        [SerializeField] private string _effectID = "";
        
        #pragma warning restore 0414
        #endregion

        protected override void CustomInitialization(MMF_Player owner)
        {
            _effectsControllers = _target.FindResults<EffectsController>();
        }

        protected override void CustomReset()
        {
            _effectsControllers = _target.FindResults<EffectsController>();
        }

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            //PlayEffect with _effectId inside _effectsControllers
        }
    }
}