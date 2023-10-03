using IIMEngine.Entities.Target;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace IIMEngine.Movements2D.Feel
{
    [AddComponentMenu("")]
    [FeedbackPath("Movements 2D/Movements Unlock")]
    public class MMF_Movements2D_Unlock : MMF_Feedback
    {
        [MMFInspectorGroup("Target", true)]
        [SerializeField] private EntityTarget _target;
        private IMove2DLockedWriter[] _moveLockedWriters;

        protected override void CustomInitialization(MMF_Player owner)
        {
            _moveLockedWriters = _target.FindResults<IMove2DLockedWriter>();
        }

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            foreach (IMove2DLockedWriter moveLockedWriter in _moveLockedWriters) {
                moveLockedWriter.AreMovementsLocked = false;
            }
        }
    }
}