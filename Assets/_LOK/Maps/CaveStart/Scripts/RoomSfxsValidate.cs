using System;
using IIMEngine.SFX;
using LOK.Core.Room;
using LOK.Core.Switch;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace LOK.CaveStart
{
    public class RoomSfxsValidate : MonoBehaviour, IRoomStartHandler, IRoomEndHandler

    {
        [Serializable]
        public class SwitchSFXMapping
        {
            [SerializeField] private SwitchEntity _switch;
            [SerializeField] private MMF_Player _enterFeedbacks;

            public SwitchEntity Switch => _switch;

            public MMF_Player EnterFeedbacks => _enterFeedbacks;
        }

        [SerializeField] private SwitchSFXMapping[] _switchSFXMappings;

        [SerializeField] private SwitchEntity _switchAlarmOn;
        [SerializeField] private SwitchEntity _switchAlarmOff;
        [SerializeField] private string _sfxIdAlarm = "";
        private SFXInstance _sfxInstanceAlarm;

        public void OnRoomStart(Room room)
        {
            foreach (SwitchSFXMapping mapping in _switchSFXMappings) {
                mapping.Switch.OnSwitchOn += _OnSwitchOn;
            }

            _switchAlarmOn.OnSwitchOn += _OnSwitchAlarmStartOn;
            _switchAlarmOff.OnSwitchOn += _OnSwitchAlarmStopOn;
        }

        public void OnRoomEnd(Room room)
        {
            foreach (SwitchSFXMapping mapping in _switchSFXMappings) {
                mapping.Switch.OnSwitchOn -= _OnSwitchOn;
            }

            _switchAlarmOn.OnSwitchOn -= _OnSwitchAlarmStartOn;
            _switchAlarmOff.OnSwitchOn -= _OnSwitchAlarmStopOn;
        }

        private void _OnSwitchAlarmStartOn(SwitchEntity obj)
        {
            _sfxInstanceAlarm = SFXsManager.Instance.PlaySound(_sfxIdAlarm);
            _switchAlarmOn.SwitchDisable();
            _switchAlarmOff.SwitchOff();
        }

        private void _OnSwitchAlarmStopOn(SwitchEntity obj)
        {
            if (_sfxInstanceAlarm == null) return;
            if (_sfxInstanceAlarm.AudioSource == null) return;
            _sfxInstanceAlarm.AudioSource.Stop();
            _switchAlarmOff.SwitchDisable();
            _switchAlarmOn.SwitchOff();
        }

        private void _OnSwitchOn(SwitchEntity switchEntity)
        {
            SwitchSFXMapping mapping = _GetMappingFromSwitch(switchEntity);
            if (mapping == null) return;
            if (mapping.EnterFeedbacks == null) return;
            mapping.EnterFeedbacks.PlayFeedbacks();
        }

        private SwitchSFXMapping _GetMappingFromSwitch(SwitchEntity switchEntity)
        {
            foreach (SwitchSFXMapping mapping in _switchSFXMappings) {
                if (mapping.Switch == switchEntity) {
                    return mapping;
                }
            }

            return null;
        }
    }
}