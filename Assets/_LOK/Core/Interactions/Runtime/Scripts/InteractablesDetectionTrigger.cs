using UnityEngine;

namespace LOK.Core.Interactions
{
    public class InteractablesDetectionTrigger : MonoBehaviour
    {
        [SerializeField] private Interactor _interactor;
        [SerializeField] [TagSelector] private string[] _validTags;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_IsGameObjectTagValid(other.gameObject)) return;
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (null == interactable) return;
            if (!_interactor.InteractablesNearBy.Contains(interactable)) {
                _interactor.InteractablesNearBy.Add(interactable);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_IsGameObjectTagValid(other.gameObject)) return;
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (null == interactable) return;
            _interactor.InteractablesNearBy.Remove(interactable);
        }

        private bool _IsGameObjectTagValid(GameObject gameObject)
        {
            foreach (string validTag in _validTags) {
                if (gameObject.CompareTag(validTag)) return true;
            }

            return false;
        }
    }
}