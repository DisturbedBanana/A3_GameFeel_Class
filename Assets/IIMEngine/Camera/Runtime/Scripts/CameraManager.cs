using UnityEngine;

namespace IIMEngine.Camera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _targetCamera = null;

        private void Awake()
        {
            CameraGlobals.Manager = this;
        }

        public void Init()
        {
            CameraGlobals.Profiles.Init(_targetCamera, _targetCamera.transform);
        }
        
        private void Update()
        {
            Transform targetCameraTransform = _targetCamera.transform;
            CameraGlobals.Profiles.ManualUpdate(_targetCamera, targetCameraTransform);
            CameraGlobals.Bounds.ManualUpdate(_targetCamera, targetCameraTransform);
            CameraGlobals.Effects.ManualUpdate(_targetCamera, targetCameraTransform);
        }
    }
}