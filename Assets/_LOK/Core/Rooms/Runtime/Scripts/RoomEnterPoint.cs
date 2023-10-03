using UnityEngine;

namespace LOK.Core.Room
{
    public class RoomEnterPoint : MonoBehaviour
    {
        [SerializeField] private string _enterID = "";
        [SerializeField] private RoomSide _side = RoomSide.Undefined;

        public RoomEnterType EnterType {
            get {
                if (!string.IsNullOrEmpty(_enterID)) return RoomEnterType.EnterID;
                if (_side != RoomSide.Undefined) return RoomEnterType.EnterSide;
                return RoomEnterType.Undefined;
            }
        }

        public string EnterID => _enterID;
        public RoomSide Side => _side;
    }
}