using System.Collections;
using IIMEngine.Camera;
using IIMEngine.Music;
using IIMEngine.Save;
using IIMEngine.ScreenTransitions;
using UnityEngine;

namespace LOK.Core.Room
{
    public class RoomsManager : MonoBehaviour
    {
        public static RoomsManager Instance { get; private set; } = null;

        [GlobalSaveKey(SaveKeyType.String)] private const string SAVEKEY_LAST_MAP = "Rooms_LastMap";
        [GlobalSaveKey(SaveKeyType.String)] private const string SAVEKEY_LAST_ROOM = "Rooms_LastRoom";
        [GlobalSaveKey(SaveKeyType.Int)] private const string SAVEKEY_LAST_ENTER_TYPE = "Rooms_LastEnterType";
        [GlobalSaveKey(SaveKeyType.String)] private const string SAVEKEY_LAST_ENTER_ID = "Rooms_LastEnterID";
        [GlobalSaveKey(SaveKeyType.Int)] private const string SAVEKEY_LAST_ENTER_SIDE = "Rooms_LastEnterSide";

        [Header("Maps")]
        [SerializeField] private GameObject _mapsParent = null;
        [SerializeField] private string _startMapID = "";

        private RoomsMap[] _allMaps = null;

        public RoomsMap CurrentMap { get; private set; } = null;

        private Room _currentRoom = null;
        public Room CurrentRoom => _currentRoom;

        [Header("Camera Transition")]
        [SerializeField] private CameraProfileTransition _changeRoomCameraTransition;

        [Header("Map Change Transition")]
        [SerializeField] private string _exitMapScreenTransitionID = "";
        [SerializeField] private string _enterMapScreenTransitionID = "";
        [SerializeField] private float _mapChangeWaitDuration = 0.5f;
        [SerializeField] private float _mapMusicFadeOutDuration = 1f;

        private void Awake()
        {
            Instance = this;
        }

        public void Init()
        {
            RoomEvents.OnRoomExitTrigger += _OnRoomExit;
            RoomEvents.OnMapExit += _OnMapExit;

            _allMaps = _mapsParent.GetComponentsInChildren<RoomsMap>(true);
            foreach (RoomsMap map in _allMaps) {
                map.Init();
                map.gameObject.SetActive(false);
            }

            CurrentMap = GetStartMap();
            CurrentMap.gameObject.SetActive(true);
            if (!string.IsNullOrEmpty(CurrentMap.MusicName)) {
                MusicsGlobals.PlaylistManager.PlayMusic(CurrentMap.MusicName);
            }

            Room startRoom = GetStartRoom(CurrentMap);
            RoomEnterPoint startEnterPoint = GetStartEnterPoint(startRoom);
            _GoToRoom(startRoom, startEnterPoint, false);
        }

        private RoomsMap GetStartMap()
        {
            string mapID = SaveSystem.ReadGlobalString(SAVEKEY_LAST_MAP, _startMapID);
            RoomsMap map = FindRoomsMap(mapID);
            if (map != null) return map;

            return FindRoomsMap(_startMapID);
        }

        private Room GetStartRoom(RoomsMap map)
        {
            string roomName = SaveSystem.ReadGlobalString(SAVEKEY_LAST_ROOM, string.Empty);
            if (string.IsNullOrEmpty(roomName)) return map.DefaultRoom;

            Room room = map.FindRoom(roomName);
            if (room != null) return room;

            return map.DefaultRoom;
        }

        private RoomEnterPoint GetStartEnterPoint(Room room)
        {
            RoomEnterType enterType = (RoomEnterType)SaveSystem.ReadGlobalInt(SAVEKEY_LAST_ENTER_TYPE, -1);
            switch (enterType) {
                case RoomEnterType.EnterID:
                    string enterID = SaveSystem.ReadGlobalString(SAVEKEY_LAST_ENTER_ID, string.Empty);
                    if (string.IsNullOrEmpty(enterID)) return null;
                    return room.FindEnterPoint(enterID);

                case RoomEnterType.EnterSide:
                    RoomSide enterSide = (RoomSide)SaveSystem.ReadGlobalInt(SAVEKEY_LAST_ENTER_SIDE, -1);
                    if (enterSide == RoomSide.Undefined) return null;
                    return room.FindEnterPoint(enterSide);
            }

            return null;
        }

        private void OnDestroy()
        {
            RoomEvents.OnRoomExitTrigger -= _OnRoomExit;
            RoomEvents.OnMapExit -= _OnMapExit;
        }

        private void _OnRoomExit(Room room, RoomSide exitSide)
        {
            Room nextRoom = null;
            switch (exitSide) {
                case RoomSide.Left:
                    nextRoom = CurrentMap.GetClosestRoomOnLeft(room.transform.position, room);
                    break;

                case RoomSide.Right:
                    nextRoom = CurrentMap.GetClosestRoomOnRight(room.transform.position, room);
                    break;

                case RoomSide.Up:
                    nextRoom = CurrentMap.GetClosestRoomOnTop(room.transform.position, room);
                    break;

                case RoomSide.Down:
                    nextRoom = CurrentMap.GetClosestRoomOnBottom(room.transform.position, room);
                    break;
            }

            if (null != nextRoom) {
                RoomEnterPoint enterPoint = nextRoom.FindEnterPoint(exitSide.GetOppositeSide());
                _GoToRoom(nextRoom, enterPoint, true);
            }
        }

        private void _OnMapExit(Room room, string nextMapID, string nextMapEnterID)
        {
            if (string.IsNullOrEmpty(nextMapID)) return;
            RoomsMap nextMap = FindRoomsMap(nextMapID);
            if (nextMap != null) {
                _GoToMap(nextMap, nextMapEnterID, true);
            }
        }

        private void _GoToMap(RoomsMap map, string enterID, bool withTransition)
        {
            StartCoroutine(_CoroutineGoToMap(map, enterID, withTransition));
        }

        private IEnumerator _CoroutineGoToMap(RoomsMap map, string enterID, bool withTransition)
        {
            if (_currentRoom != null) {
                _currentRoom.RoomExit();
                _currentRoom.RoomEnd();
            }

            if (withTransition) {
                MusicsGlobals.VolumeFader.FadeOut(_mapMusicFadeOutDuration);
                yield return ScreenTransitionsManager.Instance.PlayAndWaitTransition(_exitMapScreenTransitionID);
                MusicsGlobals.PlaylistManager.StopMusic();
                CameraGlobals.Bounds.DisableBounds();
            }

            if (CurrentMap != null) {
                CurrentMap.gameObject.SetActive(false);
            }

            CurrentMap = map;
            CurrentMap.gameObject.SetActive(true);
            Room room = map.FindRoomWithEnterID(enterID);
            if (room == null) {
                room = map.AllRooms[0];
            }

            if (_currentRoom != null) {
                _currentRoom.RoomDisable();
            }

            _currentRoom = room;
            _currentRoom.RefreshAllFeedbacksCache();

            if (withTransition) {
                yield return new WaitForSeconds(_mapChangeWaitDuration);
            }

            RoomEnterPoint enterPoint = room.FindEnterPoint(enterID);
            _currentRoom.RoomEnter(enterPoint);

            CameraGlobals.Profiles.SetProfile(room.EnterCameraProfile);

            if (room.HasCameraBounds) {
                Rect boundsRect = new Rect((Vector2)room.transform.position - room.CameraBoundsSize / 2f, room.CameraBoundsSize);
                CameraGlobals.Bounds.EnableBounds(boundsRect);
            }

            room.RoomEnable();

            _SaveLastMap(CurrentMap);
            _SaveLastRoom(_currentRoom);
            _SaveLastRoomEnter(enterPoint);

            _currentRoom.RoomStart();

            if (!string.IsNullOrEmpty(map.MusicName)) {
                MusicsGlobals.PlaylistManager.PlayMusic(map.MusicName);
            }

            if (withTransition) {
                MusicsGlobals.VolumeFader.ResetToStartVolume();
                yield return ScreenTransitionsManager.Instance.PlayAndWaitTransition(_enterMapScreenTransitionID);
            }
        }

        private void _GoToRoom(Room room, RoomEnterPoint enterPoint, bool withAnimation)
        {
            StartCoroutine(_CoroutineGoToRoom(room, enterPoint, withAnimation));
        }

        private IEnumerator _CoroutineGoToRoom(Room room, RoomEnterPoint enterPoint, bool withAnimation)
        {
            room.RoomEnable();

            if (_currentRoom != null) {
                _currentRoom.RoomExit();
                _currentRoom.RoomEnd();
            }

            CameraGlobals.Bounds.DisableBounds();
            CameraProfile cameraProfile = null;
            if (enterPoint != null) {
                cameraProfile = room.GetCameraProfileFromSide(enterPoint.Side);
            }
            if (cameraProfile == null) {
                cameraProfile = room.EnterCameraProfile;
            }

            if (withAnimation) {
                yield return CameraGlobals.Profiles.SetProfileAndWaitForTransition(cameraProfile, _changeRoomCameraTransition);
            } else {
                CameraGlobals.Profiles.SetProfile(cameraProfile);
            }

            if (_currentRoom != null) {
                _currentRoom.RoomDisable();
            }

            _currentRoom = room;
            _currentRoom.RefreshAllFeedbacksCache();

            _currentRoom.RoomEnter(enterPoint);

            if (room.HasCameraBounds) {
                Rect boundsRect = new Rect((Vector2)room.transform.position - room.CameraBoundsSize / 2f, room.CameraBoundsSize);
                CameraGlobals.Bounds.EnableBounds(boundsRect);
            }

            _SaveLastMap(CurrentMap);
            _SaveLastRoom(room);
            _SaveLastRoomEnter(enterPoint);

            _currentRoom.RoomStart();
        }

        public RoomsMap FindRoomsMap(string mapID)
        {
            foreach (RoomsMap map in _allMaps) {
                if (mapID == map.MapID) {
                    return map;
                }
            }

            return null;
        }

        private void _SaveLastMap(RoomsMap map)
        {
            SaveSystem.WriteGlobalString(SAVEKEY_LAST_MAP, map.MapID);
        }

        private void _SaveLastRoom(Room room)
        {
            SaveSystem.WriteGlobalString(SAVEKEY_LAST_ROOM, room.name);
        }

        private void _SaveLastRoomEnter(RoomEnterPoint enterPoint)
        {
            if (enterPoint == null) return;

            SaveSystem.WriteGlobalInt(SAVEKEY_LAST_ENTER_TYPE, (int)enterPoint.EnterType);
            switch (enterPoint.EnterType) {
                case RoomEnterType.EnterSide:
                    SaveSystem.WriteGlobalInt(SAVEKEY_LAST_ENTER_SIDE, (int)enterPoint.Side);
                    break;

                case RoomEnterType.EnterID:
                    SaveSystem.WriteGlobalString(SAVEKEY_LAST_ENTER_ID, enterPoint.EnterID);
                    break;
            }
        }

        private Vector3 _GetPlayerOrientDirFromEnterPoint(RoomEnterPoint enterPoint)
        {
            switch (enterPoint.Side) {
                case RoomSide.Right: return Vector3.left;
                case RoomSide.Left: return Vector3.right;
                case RoomSide.Up: return Vector3.down;
                case RoomSide.Down: return Vector3.up;
            }

            return Vector3.right;
        }
    }
}