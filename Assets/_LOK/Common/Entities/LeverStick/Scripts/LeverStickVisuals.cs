using UnityEngine;

namespace LOK.Common.LeverStick
{
    public class LeverStickVisuals : MonoBehaviour
    {
        [Header("Entity")]
        [SerializeField] private LeverStickEntity _entity;

        [Header("Sprite")]
        [SerializeField] private SpriteRenderer _spriteRenderer = null;
        [SerializeField] private Sprite _leftSprite = null;
        [SerializeField] private Sprite _rightSprite = null;

        private void OnEnable()
        {
            _entity.OnToggleOrient += _OnToggleOrient;
            _UpdateSprite(_entity.CurrentOrient);
        }

        private void _OnToggleOrient(LeverStickEntity entity, LeverStickEntity.StickOrient orient)
        {
            _UpdateSprite(orient);
        }

        private void _UpdateSprite(LeverStickEntity.StickOrient orient)
        {
            switch (orient) {
                case LeverStickEntity.StickOrient.Left: 
                    _spriteRenderer.sprite = _leftSprite;
                    break;
                
                case LeverStickEntity.StickOrient.Right: 
                    _spriteRenderer.sprite = _rightSprite;
                    break;
            }
        }
    }
}