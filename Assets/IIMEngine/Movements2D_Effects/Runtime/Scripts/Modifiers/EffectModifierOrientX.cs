using IIMEngine.Effects;
using IIMEngine.Entities.Target;
using UnityEngine;

namespace IIMEngine.Movements2D.Effects.Modifiers
{
    public class EffectModifierOrientX : AEffectModifierFloat
    {
        [Header("Target")]
        [SerializeField] private EntityTarget _target;
        private IMove2DOrientReader _orientReader;

        protected override void OnModifierInit()
        {
            _orientReader = _target.FindFirstResult<IMove2DOrientReader>();
        }

        public override float GetValue()
        {
            return _orientReader.OrientX;
        }
    }
}