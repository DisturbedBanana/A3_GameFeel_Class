using IIMEngine.Effects;
using IIMEngine.Entities.Target;
using UnityEngine;

namespace IIMEngine.Movements2D.Effects.Conditions
{
    public class EffectConditionHasOrientDirX : AEffectCondition
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Movable")]
        [SerializeField] private EntityTarget _target;
        private IMove2DOrientReader _orientReader;
        
        #pragma warning restore 0414
        #endregion

        protected override void OnConditionInit()
        {
            _orientReader = _target.FindFirstResult<IMove2DOrientReader>();
        }

        public override bool IsValid()
        {
            //TODO: Check if target OrientDir.X is not null (using OrientReader)
            return false;
        }
    }
}