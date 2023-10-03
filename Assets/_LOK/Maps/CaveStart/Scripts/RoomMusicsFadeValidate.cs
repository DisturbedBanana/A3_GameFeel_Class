using IIMEngine.Music;
using LOK.Core.Room;
using LOK.Core.Switch;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace LOK.CaveStart
{
    public class RoomMusicsFadeValidate : MonoBehaviour, IRoomStartHandler, IRoomEndHandler
    {
        [SerializeField] private SwitchEntity _switchMusicFadeIn;
        [SerializeField] private SwitchEntity _switchMusicFadeOut;

        [SerializeField] private MMF_Player _musicFadeInFeedbacks;
        [SerializeField] private MMF_Player _musicFadeOutFeedbacks;
        [SerializeField] private float _exitMusicFadeInDuration = 0.5f;


        public void OnRoomStart(Room room)
        {
            _switchMusicFadeOut.SwitchOff();
            _switchMusicFadeIn.SwitchDisable();
            
            _switchMusicFadeIn.OnSwitchOn += _OnSwitchMusicFadeInOn;
            _switchMusicFadeOut.OnSwitchOn += _OnSwitchMusicFadeOutOn;
        }

        public void OnRoomEnd(Room room)
        {
            _switchMusicFadeIn.OnSwitchOn -= _OnSwitchMusicFadeInOn;
            _switchMusicFadeOut.OnSwitchOn -= _OnSwitchMusicFadeOutOn;
            
            _musicFadeInFeedbacks.StopFeedbacks();
            _musicFadeOutFeedbacks.StopFeedbacks();
            MusicsGlobals.PlaylistManager.ResumeMusic();
            MusicsGlobals.VolumeFader.FadeIn(_exitMusicFadeInDuration);
        }

        private void _OnSwitchMusicFadeOutOn(SwitchEntity switchEntity)
        {
            _musicFadeOutFeedbacks.PlayFeedbacks();
            _musicFadeInFeedbacks.StopFeedbacks();
            _switchMusicFadeIn.SwitchOff();
            _switchMusicFadeOut.SwitchDisable();
        }

        private void _OnSwitchMusicFadeInOn(SwitchEntity switchEntity)
        {
            _musicFadeInFeedbacks.PlayFeedbacks();
            _musicFadeOutFeedbacks.StopFeedbacks();
            _switchMusicFadeOut.SwitchOff();
            _switchMusicFadeIn.SwitchDisable();
        }
    }
}