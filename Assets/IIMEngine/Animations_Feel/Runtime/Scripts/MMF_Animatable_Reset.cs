using IIMEngine.Entities.Target;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace IIMEngine.Animations.Feel
{
    [AddComponentMenu("")]
    [FeedbackPath("Animations/Animation Reset")]
    public class MMF_Animatable_Reset : MMF_Feedback
    {
        [MMFInspectorGroup("Target", true)]
        [SerializeField] private EntityTarget _target;
        private IAnimatable[] _animatables;
        
        protected override void CustomInitialization(MMF_Player owner)
        {
            _animatables = _target.FindResults<Animatable>();
        }

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            foreach (IAnimatable animatable in _animatables) {
                animatable.ResetToDefault();
            }
        }
    }
}