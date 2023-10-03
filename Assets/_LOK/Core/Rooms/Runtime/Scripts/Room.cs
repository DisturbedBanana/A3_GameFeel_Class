using System;
using IIMEngine.Camera;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace LOK.Core.Room
{
    public class Room : MonoBehaviour
    {
        public const string ENTER_CAMERA_PROFILE_NAME = "EnterCameraProfile";

        public event Action<Room> OnRoomStart;
        public event Action<Room, RoomEnterPoint> OnRoomEnter;
        public event Action<Room> OnRoomExit;
        public event Action<Room> OnRoomEnd;

        [Header("Start Player Point")]
        [SerializeField] private Transform _startPlayerPoint = null;

        //Player Start Point
        public Transform StartPlayerPoint => _startPlayerPoint;

        //Enter Points
        private RoomEnterPoint[] _enterPoints;
        private MMF_Player[] _feedbacks;

        //Camera Profiles
        [Serializable]
        public class CameraProfileExit
        {
            public CameraProfile profile;
            public RoomSide exitSide;
        }

        [Header("Camera Profiles")]
        [SerializeField] private CameraProfileExit[] _cameraProfilePerExits;

        private CameraProfile[] _cameraProfiles;
        public CameraProfile EnterCameraProfile { get; private set; }

        //Camera Bounds
        [Header("Camera Bounds")]
        [SerializeField] private bool _hasCameraBounds = false;
        [SerializeField] private Vector2 _cameraBoundsSize = new Vector2(10f, 6f);

        //Handlers
        private IRoomInitHandler[] _initHandlers;
        private IRoomStartHandler[] _startHandlers;
        private IRoomEnableHandler[] _enableHandlers;
        private IRoomDisableHandler[] _disableHandlers;
        private IRoomEnterHandler[] _enterHandlers;
        private IRoomExitHandler[] _exitHandlers;
        private IRoomEndHandler[] _endHandlers;
        private IRoomValidateHandler[] _validateHandlers;

        public bool HasCameraBounds => _hasCameraBounds;

        public bool IsCompleted { get; private set; }

        public Vector2 CameraBoundsSize => _cameraBoundsSize;

        public void Init()
        {
            IsCompleted = RoomSaveSystem.IsRoomCompleted(this);

            _enterPoints = GetComponentsInChildren<RoomEnterPoint>(true);
            _feedbacks = GetComponentsInChildren<MMF_Player>(true);
            _cameraProfiles = GetComponentsInChildren<CameraProfile>(true);
            foreach (CameraProfile cameraProfile in _cameraProfiles) {
                cameraProfile.Init();
                if (cameraProfile.name == ENTER_CAMERA_PROFILE_NAME) {
                    EnterCameraProfile = cameraProfile;
                }
            }

            _initHandlers = GetComponentsInChildren<IRoomInitHandler>(true);
            _startHandlers = GetComponentsInChildren<IRoomStartHandler>(true);
            _enableHandlers = GetComponentsInChildren<IRoomEnableHandler>(true);
            _disableHandlers = GetComponentsInChildren<IRoomDisableHandler>(true);
            _enterHandlers = GetComponentsInChildren<IRoomEnterHandler>(true);
            _exitHandlers = GetComponentsInChildren<IRoomExitHandler>(true);
            _endHandlers = GetComponentsInChildren<IRoomEndHandler>(true);
            _validateHandlers = GetComponentsInChildren<IRoomValidateHandler>(true);

            foreach (IRoomInitHandler initHandler in _initHandlers) {
                initHandler.OnRoomInit(this);
            }
        }

        public CameraProfile GetCameraProfileFromSide(RoomSide side)
        {
            foreach (CameraProfileExit profileExit in _cameraProfilePerExits) {
                if (profileExit.exitSide == side) {
                    return profileExit.profile;
                }
            }

            return null;
        }

        public RoomEnterPoint FindEnterPoint(string enterID)
        {
            foreach (RoomEnterPoint enterPoint in _enterPoints) {
                if (enterPoint.EnterID == enterID) return enterPoint;
            }

            return null;
        }

        public RoomEnterPoint FindEnterPoint(RoomSide side)
        {
            foreach (RoomEnterPoint enterPoint in _enterPoints) {
                if (enterPoint.Side == side) return enterPoint;
            }

            return null;
        }

        public void RefreshAllFeedbacksCache()
        {
            foreach (MMF_Player feedback in _feedbacks) {
                feedback.RefreshCache();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, _cameraBoundsSize);
        }

        public void RoomEnable()
        {
            gameObject.SetActive(true);
            foreach (IRoomEnableHandler enableHandler in _enableHandlers) {
                enableHandler.OnRoomEnable(this);
            }
        }

        public void RoomDisable()
        {
            foreach (IRoomDisableHandler disableHandler in _disableHandlers) {
                disableHandler.OnRoomDisable(this);
            }

            gameObject.SetActive(false);
        }

        public void RoomStart()
        {
            foreach (IRoomStartHandler startHandler in _startHandlers) {
                startHandler.OnRoomStart(this);
            }

            OnRoomStart?.Invoke(this);
            RoomEvents.OnRoomStart?.Invoke(this);
        }

        public void RoomEnter(RoomEnterPoint enterPoint)
        {
            foreach (IRoomEnterHandler enterHandler in _enterHandlers) {
                enterHandler.OnRoomEnter(this, enterPoint);
            }

            OnRoomEnter?.Invoke(this, enterPoint);
            RoomEvents.OnRoomEnter?.Invoke(this, enterPoint);
        }

        public void RoomExit()
        {
            foreach (IRoomExitHandler exitHandler in _exitHandlers) {
                exitHandler.OnRoomExit(this);
            }

            OnRoomExit?.Invoke(this);
            RoomEvents.OnRoomExit?.Invoke(this);
        }

        public void RoomEnd()
        {
            foreach (IRoomEndHandler endHandler in _endHandlers) {
                endHandler.OnRoomEnd(this);
            }

            OnRoomEnd?.Invoke(this);
            RoomEvents.OnRoomEnd?.Invoke(this);
        }

        public void RoomValidate()
        {
            foreach (IRoomValidateHandler validateHandler in _validateHandlers) {
                validateHandler.OnRoomValidated(this);
            }

            IsCompleted = true;
            RoomSaveSystem.MarkRoomAsCompleted(this);
        }
    }
}