using UnityEngine;
using UnityEngine.Audio;

namespace IIMEngine.Music
{
    public class MusicsVolumeFader : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414

        private const string AUDIOMIXER_PARAM_MUSICVOLUME = "MusicVolume";

        [SerializeField] private AudioMixer _audioMixer;
        
        [SerializeField] private float _minVolume = -80f;

        private float _startVolume = 0f;

        public bool IsFadingIn { get; private set; }
        public bool IsFadingOut { get; private set; }

        #pragma warning restore 0414
        #endregion

        private void Awake()
        {
            MusicsGlobals.VolumeFader = this;
            ResetStartVolumeFromAudioMixer();
        }

        public void ResetStartVolumeFromAudioMixer()
        {
            //TODO : Set _startVolume From AudioMixer
        }

        public void ResetToStartVolume()
        {
            //TODO: Interrupt FadeIn or FadeOut if running
            //TODO: Reset AudioMixer MusicVolume to _startVolume
        }

        public void FadeIn(float duration)
        {
            //TODO: Interrupt FadeIn or FadeOut if running
            //TODO: Lerp Value between Current Volume from AudioMixer to _startVolume
            //You can use coroutines if you want
            //Don't Forget to set IsFadingIn while transition is running
        }

        public void FadeOut(float duration)
        {
            //TODO: Interrupt FadeIn or FadeOut if running
            //TODO: Lerp Value between Current Volume from AudioMixer to _minVolume
            //You can use coroutines if you want
            //Don't Forget to set IsFadingIn while transition is running
        }
    }
}