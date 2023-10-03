using UnityEngine;

namespace LOK.Core.Switch
{
    public class SwitchDetectionTrigger : MonoBehaviour
    {
        [Header("Entity")]
        [SerializeField] private SwitchEntity _switchEntity = null;

        [Header("Tag")]
        [SerializeField] [TagSelector] private string _tagToCheck = "";

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(_tagToCheck)) return;
            if (_switchEntity == null) return;
            if (_switchEntity.CurrentState == SwitchEntity.State.Disabled) return;
            _switchEntity.SwitchOn();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(_tagToCheck)) return;
            if (_switchEntity == null) return;
            if (_switchEntity.CurrentState == SwitchEntity.State.Disabled) return;
            _switchEntity.SwitchOff();
        }
    }
}