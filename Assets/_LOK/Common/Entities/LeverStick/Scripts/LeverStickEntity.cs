using System;
using UnityEngine;

namespace LOK.Common.LeverStick
{
    public class LeverStickEntity : MonoBehaviour
    {
        public Action<LeverStickEntity, StickOrient> OnToggleOrient { get; set; }
        
        public enum StickOrient
        {
            Left,
            Right
        }

        [Header("Start Orient")]
        [SerializeField] private StickOrient _startOrient = StickOrient.Left;
        
        public StickOrient CurrentOrient { get; private set; }

        private void Awake()
        {
            CurrentOrient = _startOrient;
        }

        public void ToggleOrient()
        {
            switch (CurrentOrient) {
                case StickOrient.Left: 
                    CurrentOrient = StickOrient.Right;
                    break;
                
                case StickOrient.Right: 
                    CurrentOrient = StickOrient.Left;
                    break;
            }
            
            OnToggleOrient?.Invoke(this, CurrentOrient);
        }
    }
}