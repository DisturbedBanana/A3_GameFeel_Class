using IIMEngine.SFX;
using LOK.Core.Room;
using UnityEngine;

namespace LOK.Core.Switch
{
    public class SwitchSounds : MonoBehaviour, IRoomStartHandler, IRoomEndHandler
    {
        [Header("Entity")]
        [SerializeField] private SwitchEntity _switchEntity;

        [Header("SFXs")]
        [SerializeField] private string _onSfxName = "";
        [SerializeField] private string _offSfxName = "";
        [SerializeField] private string _disabledSfxName = "";

        public void OnRoomStart(Room.Room room)
        {
            _switchEntity.OnSwitchOn += _OnSwitchOn;
            _switchEntity.OnSwitchOff += _OnSwitchOff;
            _switchEntity.OnSwitchDisable += _OnSwitchDisable;
        }

        public void OnRoomEnd(Room.Room room)
        {
            _switchEntity.OnSwitchOn -= _OnSwitchOn;
            _switchEntity.OnSwitchOff -= _OnSwitchOff;
            _switchEntity.OnSwitchDisable -= _OnSwitchDisable;
        }

        private void _OnSwitchDisable(SwitchEntity switchEntity)
        {
            if (!gameObject.activeInHierarchy) return;
            SFXsManager.Instance.PlaySound(_disabledSfxName);
        }

        private void _OnSwitchOff(SwitchEntity switchEntity)
        {
            if (!gameObject.activeInHierarchy) return;
            SFXsManager.Instance.PlaySound(_offSfxName);
        }

        private void _OnSwitchOn(SwitchEntity switchEntity)
        {
            if (!gameObject.activeInHierarchy) return;
            SFXsManager.Instance.PlaySound(_onSfxName);
        }
    }
}