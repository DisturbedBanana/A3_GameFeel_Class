using IIMEngine.Effects;
using IIMEngine.Entities.Target;
using UnityEngine;

namespace IIMEngine.Movements2D.Effects.Modifiers
{
    public class EffectModifierMoveSpeed : AEffectModifierFloat
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Movable")]
        [SerializeField] private EntityTarget _targetGameObject;
        private IMove2DSpeedReader _moveSpeedReader;
        private IMove2DSpeedMaxReader _moveSpeedMaxReader;
        
        #pragma warning restore 0414
        #endregion

        protected override void OnModifierInit()
        {
            _moveSpeedReader = _targetGameObject.FindFirstResult<IMove2DSpeedReader>();
            _moveSpeedMaxReader = _targetGameObject.FindFirstResult<IMove2DSpeedMaxReader>();
        }

        public override float GetValue()
        {
            //TODO: Calculate and return Percentage according to MoveSpeed and MoveSpeedMax
            return 0f;
        }
    }
}