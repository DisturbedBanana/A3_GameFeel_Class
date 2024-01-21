using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

namespace IIMEngine.Camera
{
    public class CameraProfilesManager : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        private CameraProfile _defaultProfile = null;
        private CameraProfile _currentProfile = null;

        private Vector3 _position = Vector3.zero;
        private Vector3 _destination = Vector3.zero;
        private Quaternion _rotation = Quaternion.identity;
        private float _size = 0f;

        [Header("POIs")]
        [SerializeField] private int _maxPOIs = 10;
        [SerializeField] private float _POIDestinationDistanceThreshold = 0.3f;
        [SerializeField] private float _POIDestinationCompensateThreshold = 0.01f;
        private CameraPOI[] _cacheCameraPOIs;
        private POIMovementsState _POIMovementsState = POIMovementsState.Snap;
        
        private enum POIMovementsState
        {
            Snap = 0,
            Compensate
        } 

        public bool IsTransitionActive { get; private set; }

        private Coroutine _currentCoroutine = null;

        private UnityEngine.Camera _camera = null;

        #pragma warning restore 0414
        #endregion

        private void Awake()
        {
            CameraGlobals.Profiles = this;
            _cacheCameraPOIs = new CameraPOI[_maxPOIs];
            _cacheCameraPOIs = Object.FindObjectsOfType<CameraPOI>();
        }

        public void Init(UnityEngine.Camera camera, Transform cameraTransform)
        {
            _camera = camera;
            _position = cameraTransform.position;
            _rotation = cameraTransform.rotation;
            _size = camera.orthographicSize;
        }

        public void ManualUpdate(UnityEngine.Camera camera, Transform cameraTransform)
        {
            // /!\ Must be implemented into CameraFollow QRCode
            // --------------------------------------------------------
            //If Profile follow a group and transition not active
            //If Profile use POIs (Points of interest)
            //Calculate Centroid with POIs Destination
            //If Destination is too far (> POIDestinationDistanceThreshold)
            //Compensate Destination with CameraProfile.FollowPOIsDamping
            //Else
            //Snap Destination to Centroid
            //Tips : you can use enum POIMovementsState for compensation
            //Else
            //Calculate Centroid from follow groups.

            //Optional : Clamp Destination with Camera Bounds
            //Lerp position with destination using CameraProfile.FollowLerpSpeed
            // --------------------------------------------------------
            if (_currentProfile.FollowTargetGroups.Count() != 0 && _currentCoroutine == null)
            {
                if (!_currentProfile.UsePOIs)
                {
                    Debug.Log(_currentProfile.name);
                    //Calculate centroid from POIs destination
                    Vector3 centroid = Vector3.zero;
                    int count = 0;
                    for (int i = 0; i < _cacheCameraPOIs.Length; i++)
                    {
                        if (_cacheCameraPOIs[i] != null)
                        {
                            centroid += _cacheCameraPOIs[i].Position;
                            count++;
                        }
                    }
                    centroid /= count;
                    //If centroid is too far from destination
                    if (Vector3.Distance(centroid, _destination) > 10)
                    {
                        //Compensate destination with CameraProfile.FollowPOIsDamping
                        _destination = Vector3.Lerp(_destination, centroid, _currentProfile.FollowPOIsDamping * Time.deltaTime);
                        cameraTransform.position = Vector3.Lerp(cameraTransform.position, _destination, _currentProfile.FollowLerpSpeed * Time.deltaTime);
                    }
                    else
                    {
                        //Snap destination to centroid
                        _destination = centroid;
                        cameraTransform.position = Vector3.Lerp(cameraTransform.position, _destination, _currentProfile.FollowLerpSpeed * Time.deltaTime);
                    }
                }
                else
                {
                    //Calculate centroid from follow groups.
                    Vector3 centroid = Vector3.zero;
                    int count = 0;
                    foreach (var group in _currentProfile.FollowTargetGroups)
                    {
                        var followablesInGroup = CameraFollowables.FindByGroups(_currentProfile.FollowTargetGroups);
                        foreach (var followable in followablesInGroup)
                        {
                            centroid += followable.Position;
                            count++;
                        }
                    }
                    if (count > 0)
                    {
                        centroid /= count;
                        _destination = centroid;
                    }
                }
            }

            cameraTransform.position = new Vector3(_position.x, _position.y, cameraTransform.position.z);
            cameraTransform.rotation = _rotation;
            camera.orthographicSize = _size;
        }

        public IEnumerator SetProfileAndWaitForTransition(CameraProfile profile, CameraProfileTransition transition = null)
        {
            SetProfile(profile, transition);
            while (IsTransitionActive) {
                yield return null;
            }
        }

        public void SetProfile(CameraProfile profile, CameraProfileTransition transition = null)
        {
            _currentProfile = profile;
            if (profile.ProfileType == CameraProfileType.Default) {
                _defaultProfile = profile;
            }

            if (null != _currentCoroutine) {
                StopCoroutine(_currentCoroutine);
            }

            _currentCoroutine = StartCoroutine(_CoroutineChangeProfile(_currentProfile, transition));
        }

        public void ResetToDefaultProfile(CameraProfileTransition transition = null)
        {
            if (_defaultProfile == null) return;
            if (_currentProfile.ProfileType == CameraProfileType.Default) return;

            _currentProfile = _defaultProfile;

            if (null != _currentCoroutine) {
                StopCoroutine(_currentCoroutine);
            }

            StartCoroutine(_CoroutineChangeProfile(_currentProfile, transition));
        }

        private IEnumerator _CoroutineChangeProfile(CameraProfile profile, CameraProfileTransition transition = null)
        {
            IsTransitionActive = true;
            //TODO: Implements Transition 
            //Lerp between current position and profile position
            //Lerp between current rotation and profile rotation
            //Lerp between current size and profile Orthographic Size
            //Use profile animation curve to improve transition
            _position = profile.Position;
            float t = 0;
            while (t < transition.Duration)
            {
                t += Time.deltaTime;
                _position = Vector3.Lerp(_position, profile.Position, transition.Curve.Evaluate(t));
                _rotation = Quaternion.Lerp(_rotation, profile.Rotation, transition.Curve.Evaluate(t));
                _size = Mathf.Lerp(_size, profile.OrthographicSize, transition.Curve.Evaluate(t));
                yield return null;
            }
            _rotation = profile.Rotation;
            _size = profile.OrthographicSize;
            IsTransitionActive = false;
            yield break;
        }
    }
}