using System;
using IIMEngine.Camera;
using LOK.Common.Chest;
using LOK.Core.Room;
using LOK.Core.Switch;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace LOK.CaveStart
{
    public class RoomCameraValidateProfileSystem : MonoBehaviour, IRoomInitHandler, IRoomEnableHandler, IRoomEndHandler, IRoomValidateHandler
    {
        [Serializable]
        public class SwitchCameraMapping
        {
            [SerializeField] private SwitchEntity _switch;
            [SerializeField] private MMF_Player _enterFeedbacks;
            [SerializeField] private MMF_Player _exitFeedbacks;

            public SwitchEntity Switch => _switch;
            public MMF_Player EnterFeedbacks => _enterFeedbacks;
            public MMF_Player ExitFeedbacks => _exitFeedbacks;
        }

        [SerializeField] private ChestEntity _chestWithCamera;
        [SerializeField] private SwitchCameraMapping[] _switchCameraMappings;
        
        private SwitchEntity[] _allSwitches;

        private RoomQRCode _roomQRCode;

        public void OnRoomInit(Room room)
        {
            _roomQRCode = room.GetComponent<RoomQRCode>();
            _allSwitches = room.GetComponentsInChildren<SwitchEntity>();
        }

        public void OnRoomEnable(Room room)
        {
            if (RoomSaveSystem.IsRoomCompleted(room)) {
                _chestWithCamera.Open(false);
            } else {
                _chestWithCamera.OnOpenEnd += _OnChestOpened;
                foreach (SwitchCameraMapping mapping in _switchCameraMappings) {
                    mapping.Switch.OnSwitchOn += _OnSwitchOn;
                    mapping.Switch.OnSwitchOff += _OnSwitchOff;
                }
            }
        }

        public void OnRoomEnd(Room room)
        {
            _chestWithCamera.OnOpenEnd -= _OnChestOpened;
            foreach (SwitchCameraMapping mapping in _switchCameraMappings) {
                mapping.Switch.OnSwitchOn -= _OnSwitchOn;
                mapping.Switch.OnSwitchOff -= _OnSwitchOff;
            }
        }

        public void OnRoomValidated(Room room)
        {
            foreach (SwitchEntity switchEntity in _allSwitches) {
                switchEntity.SwitchDisable();
            }
        }

        private void _OnSwitchOn(SwitchEntity switchEntity)
        {
            MMF_Player feedbacks = _GetEnterFeedbacksFromSwitch(switchEntity);
            if (feedbacks != null) {
                feedbacks.PlayFeedbacks();
            }
        }

        private void _OnSwitchOff(SwitchEntity switchEntity)
        {
            MMF_Player feedbacks = _GetExitFeedbacksFromSwitch(switchEntity);
            if (feedbacks != null) {
                feedbacks.PlayFeedbacks();
            }
        }

        private void _OnChestOpened(ChestEntity chest)
        {
            _ActivateAllSwitches();
            _roomQRCode.ShowQRCode();
        }

        private void _ActivateAllSwitches()
        {
            foreach (SwitchEntity switchEntity in _allSwitches) {
                switchEntity.SwitchOff();
            }
        }

        private MMF_Player _GetEnterFeedbacksFromSwitch(SwitchEntity switchEntity)
        {
            foreach (SwitchCameraMapping mapping in _switchCameraMappings) {
                if (mapping.Switch == switchEntity) {
                    return mapping.EnterFeedbacks;
                }
            }

            return null;
        }

        private MMF_Player _GetExitFeedbacksFromSwitch(SwitchEntity switchEntity)
        {
            foreach (SwitchCameraMapping mapping in _switchCameraMappings) {
                if (mapping.Switch == switchEntity) {
                    return mapping.ExitFeedbacks;
                }
            }

            return null;
        }
    }
}