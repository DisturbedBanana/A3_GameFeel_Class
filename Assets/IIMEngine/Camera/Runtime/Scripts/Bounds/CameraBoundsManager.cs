using UnityEngine;

namespace IIMEngine.Camera
{
    public class CameraBoundsManager : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        private bool _boundsEnabled = false;
        private Rect _boundsRect;
        
        #pragma warning restore 0414
        #endregion

        private void Awake()
        {
            CameraGlobals.Bounds = this;
        }

        public void EnableBounds(Rect boundsRect)
        {
            _boundsEnabled = true;
            _boundsRect = boundsRect;
        }

        public void DisableBounds()
        {
            _boundsEnabled = false;
        }
        
        public void ManualUpdate(UnityEngine.Camera camera, Transform cameraTransform)
        {
            cameraTransform.position = ClampPosition(cameraTransform.position, camera);
        }

        public Vector3 ClampPosition(Vector3 position, UnityEngine.Camera camera)
        {
            //Check if bounds are enabled

            //Get BottomLeft Point using camera.ScreenToWorldPoint (using pos (0,0))
            //Get TopRight Point using camera.ScreenToWorldPoint / camera.pixelWidth / camera.pixelHeight
            //Calculate screenSize with TopRight and BottomLeft points
            //Clamp position according to bounds and screenSize

            return position;
        }
    }
}