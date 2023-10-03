using UnityEngine;

namespace LOK.CaveStart
{
    public class KenneyGhostEntity : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _startAnim = "";

        private void OnEnable()
        {
            if (!string.IsNullOrEmpty(_startAnim)) {
                _animator.Play(_startAnim);
            }
        }
    }
}