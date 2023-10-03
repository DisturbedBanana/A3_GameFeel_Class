using UnityEngine;

namespace LOK.Common.Doors
{
    public class DoorCollisions : MonoBehaviour
    {
        [Header("Door Entity")]
        [SerializeField] private DoorEntity _doorEntity = null;

        [Header("Collider")]
        [SerializeField] private Collider2D _collider = null;
        
        private void OnEnable()
        {
            _UpdateDoorCollider(_doorEntity);
            _doorEntity.OnOpened += _UpdateDoorCollider;
            _doorEntity.OnClosed += _UpdateDoorCollider;
        }

        private void OnDisable()
        {
            _doorEntity.OnOpened -= _UpdateDoorCollider;
            _doorEntity.OnClosed -= _UpdateDoorCollider;
        }

        private void _UpdateDoorCollider(DoorEntity door)
        {
            _collider.enabled = !door.IsOpened;
        }
    }
}