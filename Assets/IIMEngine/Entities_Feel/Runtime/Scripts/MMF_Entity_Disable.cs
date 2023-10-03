using IIMEngine.Entities.Target;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace IIMEngine.Entities.Feel
{
    [AddComponentMenu("")]
    [FeedbackPath("Entities/Entity Disable")]
    public class MMF_Entity_Disable : MMF_Feedback
    {
        [MMFInspectorGroup("Target", true)]
        [SerializeField] private EntityTarget _target;
        private Transform[] _targetTransforms;

        protected override void CustomInitialization(MMF_Player owner)
        {
            _targetTransforms = _target.FindResults<Transform>();
        }

        protected override void CustomReset()
        {
            _targetTransforms = _target.FindResults<Transform>();
        }

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            foreach (Transform targetTransform in _targetTransforms) {
                targetTransform.gameObject.SetActive(false);
            }
        }
    }
}