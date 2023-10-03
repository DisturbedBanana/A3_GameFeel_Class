using System;
using UnityEngine;

namespace IIMEngine.SFX
{
    [Serializable]
    public class SFXData
    {
        [SerializeField] private string _name = "";
        [SerializeField] private AudioClip _clip = null;
        [SerializeField] private int _sizeMax = 1;
        [SerializeField] private bool _isLooping = false;
        [SerializeField] private SFXOverflowOperation _overflowOperation = SFXOverflowOperation.ReuseOldest;

        public string Name => _name;
        public AudioClip Clip => _clip;
        public int SizeMax => _sizeMax;
        
        public bool IsLooping => _isLooping;
        public SFXOverflowOperation OverflowOperation => _overflowOperation;
    }
}