using System;
using UnityEngine;

namespace LOK.Common.Doors
{
    public class DoorEntity : MonoBehaviour
    {
        public Action<DoorEntity> OnOpened { get; set; } = null;
        public Action<DoorEntity> OnClosed { get; set; } = null;
        
        [SerializeField] private bool _isOpened = true;
        public bool IsOpened => _isOpened;
        
        public void Open()
        {
            if (_isOpened) return;
            _isOpened = true;
            OnOpened?.Invoke(this);
            DoorEvents.OnDoorOpened?.Invoke(this);
        }

        public void Close()
        {
            if(!_isOpened) return;
            _isOpened = false;
            OnClosed?.Invoke(this);
            DoorEvents.OnDoorClosed?.Invoke(this);
        }
    }
}