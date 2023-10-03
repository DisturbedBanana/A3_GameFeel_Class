using UnityEngine;

namespace IIMEngine.Camera
{
    public class CameraPOIDetector : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        
        [Header("Offset")]
        [SerializeField] private Vector3 _offset = Vector3.zero;
        public Vector3 Offset => _offset;

        [Header("Range")]
        [SerializeField] private float _range = 1f;
        public float Range => _range;

        public Vector3 Position => transform.position;

        public Vector3 PositionWithOffset => Position + Offset;
        
        #pragma warning restore 0414
        #endregion
        
        private void OnEnable()
        {
            //Register this object into static class CameraPOIs
        }

        private void OnDisable()
        {
            //Unregister this object into static class CameraPOIs
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(PositionWithOffset, _range);
        }
    }
}