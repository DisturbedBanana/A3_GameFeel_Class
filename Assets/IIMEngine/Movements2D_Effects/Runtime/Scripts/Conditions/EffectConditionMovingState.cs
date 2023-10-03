using IIMEngine.Effects;
using IIMEngine.Entities.Target;
using UnityEngine;

namespace IIMEngine.Movements2D.Effects.Conditions
{
    public class EffectConditionMovingState : AEffectCondition
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Target")]
        [SerializeField] private EntityTarget _target;
        private IMove2DLockedReader _moveLockedReader;
        private IMove2DDirReader _moveDirReader;

        public enum MoveCheckState
        {
            Moving = 0,
            NotMoving,
        }
        
        [Header("Move Check State")]
        [SerializeField] private MoveCheckState _moveCheckState = MoveCheckState.Moving;

        #pragma warning restore 0414
        #endregion
        
        protected override void OnConditionInit()
        {
            _moveLockedReader = _target.FindFirstResult<IMove2DLockedReader>();
            _moveDirReader = _target.FindFirstResult<IMove2DDirReader>();
        }

        public override bool IsValid()
        {
            //TODO: Check if target is moving (using MoveLockedReader and MoveDirReader)
            return false;
        }
    }
}