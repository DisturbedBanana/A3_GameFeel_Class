using IIMEngine.Effects.Triggers;
using IIMEngine.Entities.Target;
using UnityEngine;

namespace IIMEngine.Movements2D.Effects.Triggers
{
    public class EffectTriggerOrientXChanged : AEffectTrigger
    {
        [Header("Target")]
        [SerializeField] private EntityTarget _target;
        private IMove2DOrientReader _orientReader;

        private float _lastOrientX = 0f;

        protected override void OnTriggerInit()
        {
            _orientReader = _target.FindFirstResult<IMove2DOrientReader>();
        }

        private void Start()
        {
            if (_orientReader == null) return;
            _lastOrientX = _orientReader.OrientX;
        }

        private void Update()
        {
            if (_orientReader == null) return;

            if (_lastOrientX * _orientReader.OrientX < 0f) {
                InvokeTrigger();
                _lastOrientX = _orientReader.OrientX;
            }
        }
    }
}