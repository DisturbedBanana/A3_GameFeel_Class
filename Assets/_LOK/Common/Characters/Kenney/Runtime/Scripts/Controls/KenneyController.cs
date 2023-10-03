using IIMEngine.Movements2D;
using LOK.Core.UserProfiles;
using LOK.Core.Interactions;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyController : MonoBehaviour
    {
        [SerializeField] private GameObject _kenneyRoot = null;

        private IMove2DDirWriter _moveDirWriter;
        private Interactor _interactor;

        private void Start()
        {
            _moveDirWriter = _kenneyRoot.GetComponent<IMove2DDirWriter>();
            _interactor = _kenneyRoot.GetComponent<Interactor>();
        }

        void Update()
        {
            if (UIPopupPasswordValidator.Instance.IsOpened
                || UIPopupPasswordEnterUserName.Instance.IsOpened) {
                _moveDirWriter.MoveDir = Vector2.zero;
            } else {
                _MovePlayerFromInputs();
            }

            _ManageInputAction();
        }

        private void _ManageInputAction()
        {
            if (UIPopupPasswordValidator.Instance.IsOpened) return;
            if (UIPopupPasswordEnterUserName.Instance.IsOpened) return;

            if (_GetInputDownAction()) {
                Vector3 position = _kenneyRoot.transform.position;
                IInteractable interactable = _interactor.FindClosestInteractableNearBy(position);
                if (_interactor.CanInteract && interactable != null) {
                    interactable.Interact();
                }
            }
        }

        private void _MovePlayerFromInputs()
        {
            //TODO: Write MoveDir according to inputs
            //You can _GetInputMoveLeft /  _GetInputMoveRight() / _GetInputMoveUp() / _GetInputMoveDown()
            //Don't forget to normalize ;)
        }

        private bool _GetInputDownAction()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        private bool _GetInputMoveLeft()
        {
            return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow);
        }

        private bool _GetInputMoveRight()
        {
            return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        }

        private bool _GetInputMoveDown()
        {
            return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        }

        private bool _GetInputMoveUp()
        {
            return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow);
        }
    }
}