using System.Collections;
using LOK.Core.Room;
using LOK.Common.Characters.Kenney;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace LOK.CaveStart
{
    public class RoomStart : MonoBehaviour, IRoomInitHandler, IRoomStartHandler, IRoomValidateHandler
    {
        [Header("Arrow Validators")]
        private ArrowValidatorEntity[] _arrowsToValidate = null;

        [Header("Blocks")]
        [SerializeField] private GameObject[] _blocksToRemoveWhenCompleted = null;

        [Header("Validate Feedbacks")]
        [SerializeField] private MMF_Player _validateFeedbacks;

        [Header("Kenney Movements")]
        [SerializeField] private KenneyMovementsData _movementsData;

        private Room _room;

        public void OnRoomInit(Room room)
        {
            _room = room;

            _arrowsToValidate = GetComponentsInChildren<ArrowValidatorEntity>();

            if (room.IsCompleted) {
                RemoveBlock();
                _MarkArrowValidatorsAsValidated();
            }
        }

        private void Update()
        {
            if (_room.IsCompleted) return;

            if (_AreAllArrowValidatorsValidated()) {
                _room.RoomValidate();
            }
        }

        public void OnRoomValidated(Room room)
        {
            StartCoroutine(_CoroutineRoomValidate());
        }

        private IEnumerator _CoroutineRoomValidate()
        {
            yield return _validateFeedbacks.PlayFeedbacksCoroutine(transform.position);
        }

        public void OnRoomStart(Room room)
        {
            _movementsData.SpeedMode = KenneySpeedMode.ConstantSpeed;
        }

        public void RemoveBlock()
        {
            foreach (GameObject block in _blocksToRemoveWhenCompleted) {
                block.SetActive(false);
            }
        }

        private void _MarkArrowValidatorsAsValidated()
        {
            foreach (ArrowValidatorEntity arrowValidator in _arrowsToValidate) {
                arrowValidator.MarkAsValidated();
            }
        }

        private bool _AreAllArrowValidatorsValidated()
        {
            foreach (ArrowValidatorEntity arrowValidator in _arrowsToValidate) {
                if (!arrowValidator.IsValidated) return false;
            }

            return true;
        }
    }
}