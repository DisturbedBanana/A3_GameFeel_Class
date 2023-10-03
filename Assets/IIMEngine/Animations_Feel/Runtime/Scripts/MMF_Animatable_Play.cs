using IIMEngine.Entities.Target;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace IIMEngine.Animations.Feel
{
    [AddComponentMenu("")]
    [FeedbackPath("Animations/Animation Play")]
    public class MMF_Animatable_Play : MMF_Feedback
    {
        [MMFInspectorGroup("Target", true)]
        [SerializeField] private EntityTarget _target;
        private IAnimatable[] _animatables;

        [MMFInspectorGroup("Animation", true)]
        [SerializeField] private string _animationName = "";

        protected override void CustomInitialization(MMF_Player owner)
        {
            _animatables = _target.FindResults<Animatable>();
        }

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            foreach (IAnimatable animatable in _animatables) {
                animatable.PlayAnimation(_animationName);
            }
        }
    }
}