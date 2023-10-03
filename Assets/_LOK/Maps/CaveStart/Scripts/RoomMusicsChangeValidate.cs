using System;
using IIMEngine.Music;
using LOK.Core.Room;
using LOK.Core.Switch;
using UnityEngine;

namespace LOK.CaveStart
{
    public class RoomMusicsChangeValidate : MonoBehaviour, IRoomStartHandler, IRoomEndHandler
    {
        [Serializable]
        public class SwitchMusicMapping
        {
            [SerializeField] private SwitchEntity _switch;
            [SerializeField] private string _musicName;

            public SwitchEntity Switch => _switch;
            public string MusicName => _musicName;
        }

        [SerializeField] private SwitchMusicMapping[] _switchMusicMappings;

        public void OnRoomStart(Room room)
        {
            foreach (SwitchMusicMapping mapping in _switchMusicMappings) {
                mapping.Switch.OnSwitchOn += _OnSwitchOn;
            }
        }

        public void OnRoomEnd(Room room)
        {
            foreach (SwitchMusicMapping mapping in _switchMusicMappings) {
                mapping.Switch.OnSwitchOn -= _OnSwitchOn;
            }

            MusicsGlobals.PlaylistManager.PlayMusic(RoomsManager.Instance.CurrentMap.MusicName);
        }

        private void _OnSwitchOn(SwitchEntity switchTriggered)
        {
            switchTriggered.SwitchDisable();
            foreach (SwitchMusicMapping mapping in _switchMusicMappings) {
                if (mapping.Switch != switchTriggered) {
                    mapping.Switch.SwitchOff();
                }
            }

            MusicsGlobals.PlaylistManager.PlayMusic(_GetMusicIDFromSwitch(switchTriggered));
        }

        private string _GetMusicIDFromSwitch(SwitchEntity switchEntity)
        {
            foreach (SwitchMusicMapping mapping in _switchMusicMappings) {
                if (mapping.Switch == switchEntity) {
                    return mapping.MusicName;
                }
            }

            return string.Empty;
        }
    }
}