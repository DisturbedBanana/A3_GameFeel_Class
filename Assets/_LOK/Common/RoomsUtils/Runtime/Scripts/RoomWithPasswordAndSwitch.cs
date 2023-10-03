using LOK.Core.Room;
using LOK.Core.Switch;
using LOK.Core.UserProfiles;
using UnityEngine;

namespace LOK.Common.RoomUtils
{
    public class RoomWithPasswordAndSwitch : MonoBehaviour, IRoomInitHandler, IRoomEnableHandler, IRoomDisableHandler
    {
        [Header("Password Validator")]
        [SerializeField] private string _passwordValidatorID = "";

        [Header("Switch")]
        [SerializeField] private SwitchEntity _switch;

        [Header("Lock")]
        [SerializeField] private GameObject _lock;
        
        private Room _room;
        
        public void OnRoomInit(Room room)
        {
            _room = room;
        }

        public void OnRoomEnable(Room room)
        {
            if (room.IsCompleted) {
                if (_lock != null) {
                    _lock.gameObject.SetActive(false);
                }

                if (_switch != null) {
                    _switch.SwitchDisable();
                }
            } else {
                if (_switch != null) {
                    _switch.OnSwitchOn += _OnSwitchOn;
                }

                UIPopupPasswordValidator.Instance.OnPasswordValidated += RoomValidate;
            }
        }

        public void OnRoomDisable(Room room)
        {
            if (_switch != null) {
                _switch.OnSwitchOn -= _OnSwitchOn;
            }

            UIPopupPasswordValidator.Instance.OnPasswordValidated -= RoomValidate;
        }

        public void RoomValidate()
        {
            _room.RoomValidate();
            if (_switch != null) {
                _switch.OnSwitchOn -= _OnSwitchOn;
                _switch.SwitchDisable();
            }
        }

        private void _OnSwitchOn(SwitchEntity switchEntity)
        {
            OpenPasswordPopup();
        }

        public void OpenPasswordPopup()
        {
            if (RoomSaveSystem.IsRoomCompleted(_room)) return;
            if (UIPopupPasswordValidator.Instance.IsOpened) return;
            UIPopupPasswordValidator.Instance.Open(_passwordValidatorID);
        }
    }
}