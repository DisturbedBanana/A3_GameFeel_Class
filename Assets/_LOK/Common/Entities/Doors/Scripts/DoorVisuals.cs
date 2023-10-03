using UnityEngine;

namespace LOK.Common.Doors
{
    public class DoorVisuals : MonoBehaviour
    {
        [Header("Door Entity")]
        [SerializeField] private DoorEntity _doorEntity = null;

        [Header("Sprite")]
        [SerializeField] private SpriteRenderer _spriteRenderer = null;
        [SerializeField] private Sprite _spriteOpened = null;
        [SerializeField] private Sprite _spriteClosed = null;

        private void OnEnable()
        {
            _UpdateDoorSprite(_doorEntity);
            _doorEntity.OnOpened += _UpdateDoorSprite;
            _doorEntity.OnClosed += _UpdateDoorSprite;
        }

        private void OnDisable()
        {
            _doorEntity.OnOpened -= _UpdateDoorSprite;
            _doorEntity.OnClosed -= _UpdateDoorSprite;
        }

        private void _UpdateDoorSprite(DoorEntity door)
        {
            _spriteRenderer.sprite = door.IsOpened ? _spriteOpened : _spriteClosed;
        }
    }
}