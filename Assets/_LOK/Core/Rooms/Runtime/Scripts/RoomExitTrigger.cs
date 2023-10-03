using UnityEngine;

namespace LOK.Core.Room
{
    public class RoomExitTrigger : MonoBehaviour
    {
        [SerializeField] private RoomExitAction _exitAction = RoomExitAction.GoToRoomSide;
        [SerializeField] private RoomSide _exitSide = RoomSide.Undefined;
        [SerializeField] private string _exitMapID = "";
        [SerializeField] private string _exitEnterID = "";

        private Room _room = null;
        
        private void Awake()
        {
            _room = GetComponentInParent<Room>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            if (null == _room) return;
            switch (_exitAction) {
                case RoomExitAction.GoToRoomSide:
                    if (_exitSide != RoomSide.Undefined) {
                        RoomEvents.OnRoomExitTrigger?.Invoke(_room, _exitSide);
                    }
                    break;
                
                case RoomExitAction.ChangeMap:
                    if (!string.IsNullOrEmpty(_exitMapID)) {
                        RoomEvents.OnMapExit?.Invoke(_room, _exitMapID, _exitEnterID);
                    }
                    break;
            }
        }
    }
}