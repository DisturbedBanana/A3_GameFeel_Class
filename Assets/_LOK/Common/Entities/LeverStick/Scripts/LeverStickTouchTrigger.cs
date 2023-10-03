using LOK.Core.Globals;
using UnityEngine;

namespace LOK.Common.LeverStick
{
    public class LeverStickTouchTrigger : MonoBehaviour
    {
        [Header("Entity")]
        [SerializeField] private LeverStickEntity _entity;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(Tags.PLAYER_SWORD)) {
                _entity.ToggleOrient();
            }
        }
    }
}