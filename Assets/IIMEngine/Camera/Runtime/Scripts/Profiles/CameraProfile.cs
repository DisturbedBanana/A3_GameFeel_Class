using UnityEngine;

namespace IIMEngine.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraProfile : MonoBehaviour
    {
        [Header("Type")]
        [SerializeField] private CameraProfileType _profileType;

        public CameraProfileType ProfileType => _profileType;

        [Header("Follow")]
        [SerializeField] private string[] _followTargetGroups;
        [SerializeField] private float _followLerpSpeed = 5f;

        public string[] FollowTargetGroups => _followTargetGroups;
        public float FollowLerpSpeed => _followLerpSpeed;
        
        [Header("POI")]
        [SerializeField] private bool _usePOIs = false;
        [SerializeField] private float _followPOIsDamping = 1f;
        public bool UsePOIs => _usePOIs;
        public float FollowPOIsDamping => _followPOIsDamping;

        private UnityEngine.Camera _camera;

        public Vector3 Position => transform.position;

        public Quaternion Rotation => transform.rotation;

        public float OrthographicSize => _camera.orthographicSize;

        public void Init()
        {
            _camera = GetComponent<UnityEngine.Camera>();
            _camera.enabled = false;
        }
    }
}