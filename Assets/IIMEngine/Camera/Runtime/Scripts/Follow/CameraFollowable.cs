using UnityEngine;

namespace IIMEngine.Camera
{
    public class CameraFollowable : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Groups")]
        [SerializeField] private string[] _targetGroups;

        public string[] TargetGroups => _targetGroups;

        [Header("Weight")]
        [SerializeField] private float _weight = 1f;
        public float Weight => _weight;

        public Vector3 Position => transform.position;
        
        #pragma warning restore 0414
        #endregion

        private void OnEnable()
        {
            //Register this object into static class CameraFollowables
        }

        private void OnDisable()
        {
            //Unregister this object into static class CameraFollowables
        }
    }
}