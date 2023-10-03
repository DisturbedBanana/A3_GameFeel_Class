using System.Collections.Generic;
using UnityEngine;

namespace IIMEngine.SFX
{
    public class SFXsManager : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414

        public static SFXsManager Instance { get; private set; }

        [Header("Bank")]
        [SerializeField] private SFXsBank _bank;

        [Header("Audio Source")]
        [SerializeField] private AudioSource _audioSourceTemplate = null;

        private Dictionary<string, List<SFXInstance>> _poolInstancesDict = new Dictionary<string, List<SFXInstance>>();
        private Dictionary<string, List<SFXInstance>> _playingInstancesDict = new Dictionary<string, List<SFXInstance>>();
        private Dictionary<string, SFXData> _datasDict = new Dictionary<string, SFXData>();
        
        #pragma warning restore 0414
        #endregion

        private void Awake()
        {
            Instance = this;
            Init();
        }

        private void Update()
        {
            _CleanupNonPlayingInstances();
        }

        public void Init()
        {
            _InitDatasDict();
            _InitPoolDict();
            _InitPlayingInstancesDict();
            _LoadAllAudiosData();
        }

        private void _CleanupNonPlayingInstances()
        {
            //Loop over all playing instance
                //If SFXInstance audiosource is playing
                    //Destroy Instance if DestroyWhenComplete is true
                    //Reset Instance and move it to pool is DestroyWhenComplete is false
        }

        private void _InitDatasDict()
        {
            //Loop over all SFXsData inside bank and fill _datasDict dictionary
        }

        private void _InitPoolDict()
        {
            //Loop over all SFXsData inside bank
            //Create multiple SFXsInstance using SizeMax property inside SFXData
            //And store it into _poolInstancesDict
        }
        
        private void _InitPlayingInstancesDict()
        {
            //Loop over all SFXsData inside bank
            //Init PlayingInstances Dictionary using SizeMax property inside SFXData
        }

        public SFXInstance PlaySound(string name)
        {
            SFXInstance sfxInstance = _PikUpInstanceFromPool(name);
            if (sfxInstance == null) return null;
            sfxInstance.Transform.position = Vector2.zero;
            //Forcing SetActive for a gameobject containing an AudioSource replay the sound inside
            sfxInstance.GameObject.SetActive(false);
            sfxInstance.GameObject.SetActive(true);
            return sfxInstance;
        }

        private SFXInstance _PikUpInstanceFromPool(string name)
        {
            //Try to find an SFXInstance inside Pool Dictionary
            
            //If an Instance is available
                //Remove sfx instance from Pool Dictionary
                //Add sfx instance from PlayingSFX Dictionary
                //return sfx instance
            //Else
                //Check Overflow operation
                //If Overflow is cancel
                    //Do nothing, cancel means we do not play sounds if there is no sounds available in the pool
                //If Overflow is ReuseOldest
                    //Find sfx instance from PlayingSFX Dictionary
                //If Overflow is Create And Destroy
                    //Create sfx instance using SFXData
                    //Mark sfx instance as Destroyable (DestroyOnComplete = true)
                //Add Found sfx instance to PlayingSFX Dictionary
                //return Instance

            return null;
        }

        private void _LoadAllAudiosData()
        {
            //AudioClips are not load by default
            //We need to load it using LoadAudioData
            //See : https://docs.unity3d.com/ScriptReference/AudioClip.LoadAudioData.html
        }
    }
}