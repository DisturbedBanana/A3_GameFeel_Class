using UnityEngine;

namespace LOK.Core.Room
{
    public class RoomsMap : MonoBehaviour
    {
        private const float DELTA_MARGIN = 0.1f;

        [Header("ID")]
        [SerializeField] private string _mapID = "";

        //Music
        [Header("Music")]
        [SerializeField] private string _musicName = "";

        public string MusicName => _musicName;

        public string MapID => _mapID;

        public Room[] AllRooms { get; private set; }

        public Room DefaultRoom => AllRooms[0];

        public void Init()
        {
            AllRooms = GetComponentsInChildren<Room>(true);
            foreach (Room room in AllRooms) {
                room.Init();
                room.gameObject.SetActive(false);
            }
        }

        public Room FindRoom(string roomName)
        {
            foreach (Room room in AllRooms) {
                if (room.name == roomName) return room;
            }

            return null;
        }

        public Room FindRoomWithEnterID(string enterID)
        {
            foreach (Room room in AllRooms) {
                RoomEnterPoint enterPoint = room.FindEnterPoint(enterID);
                if (enterPoint != null) {
                    return room;
                }
            }

            return null;
        }

        public Room GetClosestRoomOnTop(Vector3 position, Room ignoredRoom)
        {
            Room closestRoom = null;
            float closestDeltaY = Mathf.Infinity;
            foreach (Room room in AllRooms) {
                if (ignoredRoom == room) continue;
                Vector3 roomPos = room.transform.position;
                float deltaX = roomPos.x - position.x;
                float deltaY = roomPos.y - position.y;
                if (Mathf.Abs(deltaX) <= DELTA_MARGIN && deltaY > 0f && deltaY < closestDeltaY) {
                    closestDeltaY = deltaY;
                    closestRoom = room;
                }
            }

            return closestRoom;
        }

        public Room GetClosestRoomOnBottom(Vector3 position, Room ignoredRoom)
        {
            Room closestRoom = null;
            float closestDeltaY = -Mathf.Infinity;
            foreach (Room room in AllRooms) {
                if (ignoredRoom == room) continue;
                Vector3 roomPos = room.transform.position;
                float deltaX = roomPos.x - position.x;
                float deltaY = roomPos.y - position.y;
                if (Mathf.Abs(deltaX) <= DELTA_MARGIN && deltaY < 0f && deltaY > closestDeltaY) {
                    closestDeltaY = deltaY;
                    closestRoom = room;
                }
            }

            return closestRoom;
        }

        public Room GetClosestRoomOnLeft(Vector3 position, Room ignoredRoom)
        {
            Room closestRoom = null;
            float closestDeltaX = -Mathf.Infinity;
            foreach (Room room in AllRooms) {
                Vector3 roomPos = room.transform.position;
                float deltaX = roomPos.x - position.x;
                float deltaY = roomPos.y - position.y;
                if (Mathf.Abs(deltaY) < DELTA_MARGIN && deltaX < 0f && deltaX > closestDeltaX) {
                    closestDeltaX = deltaX;
                    closestRoom = room;
                }
            }

            return closestRoom;
        }

        public Room GetClosestRoomOnRight(Vector3 position, Room ignoredRoom)
        {
            Room closestRoom = null;
            float closestDeltaX = Mathf.Infinity;
            foreach (Room room in AllRooms) {
                if (ignoredRoom == room) continue;
                Vector3 roomPos = room.transform.position;
                float deltaX = roomPos.x - position.x;
                float deltaY = roomPos.y - position.y;
                if (Mathf.Abs(deltaY) < DELTA_MARGIN && deltaX > 0f && deltaX < closestDeltaX) {
                    closestDeltaX = deltaX;
                    closestRoom = room;
                }
            }

            return closestRoom;
        }
    }
}