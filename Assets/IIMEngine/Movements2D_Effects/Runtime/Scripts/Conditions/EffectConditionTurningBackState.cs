using IIMEngine.Effects;
using IIMEngine.Entities.Target;
using UnityEngine;

namespace IIMEngine.Movements2D.Effects.Conditions
{
    public class EffectConditionTurningBackState : AEffectCondition
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Movable")]
        [SerializeField] private EntityTarget _target;
        private IMove2DTurnBackReader _turnBackReader;

        public enum TurnCheckState
        {
            TurningBack = 0,
            NotTurningBack,
        }

        [Header("Check State")]
        [SerializeField] private TurnCheckState _turnCheckState = TurnCheckState.TurningBack;

        #pragma warning restore 0414
        #endregion
        
        protected override void OnConditionInit()
        {
            _turnBackReader = _target.FindFirstResult<IMove2DTurnBackReader>();
        }

        public override bool IsValid()
        {
            //TODO: Check if target is turning back (using TurnBackReader)
            return false;
        }
    }
}