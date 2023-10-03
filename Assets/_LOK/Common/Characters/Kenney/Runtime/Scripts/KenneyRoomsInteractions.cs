using IIMEngine.Movements2D;
using LOK.Core.Room;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyRoomsInteractions : MonoBehaviour
    {
        private IMove2DOrientWriter _orientWriter;

        private void Awake()
        {
            _orientWriter = GetComponent<IMove2DOrientWriter>();
            RoomEvents.OnRoomExit += _OnRoomExit;
            RoomEvents.OnRoomEnter += _OnRoomEnter;
        }

        private void OnDestroy()
        {
            RoomEvents.OnRoomExit -= _OnRoomExit;
            RoomEvents.OnRoomEnter -= _OnRoomEnter;
        }

        private void _OnRoomEnter(Room room, RoomEnterPoint enterPoint = null)
        {
            Vector3 roomPos = room.transform.position;
            Vector3 enterPos = roomPos;
            if (enterPoint != null) {
                enterPos = enterPoint.transform.position;
            } else if (room.StartPlayerPoint != null) {
                enterPos = room.StartPlayerPoint.position;
            }

            Vector3 kenneyPos = transform.position;
            kenneyPos.x = enterPos.x;
            kenneyPos.y = enterPos.y;
            transform.position = kenneyPos;

            if (_TryGetPlayerOrientDirFromEnterPoint(enterPoint, out Vector2 orientDir)) {
                _orientWriter.OrientDir = orientDir;
            }

            gameObject.SetActive(true);
        }

        private void _OnRoomExit(Room exit)
        {
            gameObject.SetActive(false);
        }

        private bool _TryGetPlayerOrientDirFromEnterPoint(RoomEnterPoint enterPoint, out Vector2 orientDir)
        {
            orientDir = Vector2.right;

            if (enterPoint == null) return false;
            if (enterPoint.EnterType != RoomEnterType.EnterSide) return false;

            switch (enterPoint.Side) {
                case RoomSide.Right:
                    orientDir = Vector2.left;
                    return true;

                case RoomSide.Left:
                    orientDir = Vector2.right;
                    return true;

                case RoomSide.Up:
                    orientDir = Vector2.down;
                    break;

                case RoomSide.Down:
                    orientDir = Vector2.up;
                    break;
            }

            return false;
        }
    }
}