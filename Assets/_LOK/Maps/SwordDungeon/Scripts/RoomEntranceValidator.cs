using IIMEngine.SFX;
using LOK.Common.Doors;
using LOK.Common.LeverStick;
using LOK.Core.Globals;
using UnityEngine;

namespace LOK.DungeonSwordAttack
{
    public class RoomEntranceValidator : MonoBehaviour
    {
        [SerializeField] private DoorEntity _doorToOpen = null;
        [SerializeField] private LeverStickEntity _leverStick = null;
        [SerializeField] private LeverStickEntity.StickOrient _validOrient = LeverStickEntity.StickOrient.Right;
        [SerializeField] private float _validationDelay = 0.25f;

        private bool _isResolved = false;
        
        private void OnEnable()
        {
            if (!_isResolved) {
                _leverStick.OnToggleOrient += _OnStickToggleOrient;
            }
        }

        private void OnDisable()
        {
            _leverStick.OnToggleOrient -= _OnStickToggleOrient;
        }

        private void _OnStickToggleOrient(LeverStickEntity stickEntity, LeverStickEntity.StickOrient orient)
        {
            if (orient != _validOrient) return;
            Invoke("ValidateRoom", _validationDelay);
            _isResolved = true;
            _leverStick.OnToggleOrient -= _OnStickToggleOrient;
        }

        public void ValidateRoom()
        {
            _doorToOpen.Open();
            SFXsManager.Instance.PlaySound(SFXs.PUZZLE_RESOLVED);
        }
    }
}