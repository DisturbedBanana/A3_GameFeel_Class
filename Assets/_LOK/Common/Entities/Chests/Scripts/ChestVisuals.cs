using UnityEngine;

namespace LOK.Common.Chest
{
    public class ChestVisuals : MonoBehaviour
    {
        [Header("Entity")]
        [SerializeField] private ChestEntity _chestEntity;

        [Header("Sprite")]
        [SerializeField] private SpriteRenderer _spriteRenderer = null;
        [SerializeField] private Sprite _spriteOpened = null;
        [SerializeField] private Sprite _spriteClosed = null;

        private void OnEnable()
        {
            _UpdateChestSprite(_chestEntity);
            _chestEntity.OnOpenEnd += _UpdateChestSprite;
            _chestEntity.OnClosed += _UpdateChestSprite;
        }

        private void OnDisable()
        {
            _chestEntity.OnOpenEnd -= _UpdateChestSprite;
            _chestEntity.OnClosed -= _UpdateChestSprite;
        }

        private void _UpdateChestSprite(ChestEntity chest)
        {
            _spriteRenderer.sprite = chest.IsOpened ? _spriteOpened : _spriteClosed;
        }
    }
}