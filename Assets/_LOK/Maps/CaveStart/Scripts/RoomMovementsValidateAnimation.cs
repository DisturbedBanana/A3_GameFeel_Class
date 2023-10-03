using System.Collections;
using LOK.Common.Characters.Kenney;
using LOK.Core.Room;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace LOK.CaveStart
{
    public class RoomMovementsValidateAnimation : MonoBehaviour, IRoomInitHandler, IRoomEnableHandler, IRoomValidateHandler
    {
        [Header("Frame Validators")]
        [SerializeField] private AnimationValidatorEntity[] _animationValidators;

        [Header("Blocks")]
        [SerializeField] private GameObject[] _blocksToRemoveWhenCompleted = null;
        
        [Header("Validate Feedbacks")]
        [SerializeField] private MMF_Player _validateFeedbacks;
        
        [Header("Kenney Movements")]
        [SerializeField] private KenneyMovementsData _movementsData;

        private Room _room;
        
        private void Update()
        {
            if (_room.IsCompleted) return;
            
            if (_AreAllAnimationValidatorsValidated()) {
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

        private bool _AreAllAnimationValidatorsValidated()
        {
            foreach (AnimationValidatorEntity animationValidator in _animationValidators) {
                if (!animationValidator.IsValidated) return false;
            }
            
            return true;
        }

        public void OnRoomInit(Room room)
        {
            _room = room;
            if (_room.IsCompleted) {
                RemoveBlock();
                _MarkAnimationValidatorsAsValidated();
            }
        }

        public void OnRoomEnable(Room room)
        {
            _movementsData.SpeedMode = KenneySpeedMode.ConstantSpeed;
        }

        public void RemoveBlock()
        {
            foreach (GameObject block in _blocksToRemoveWhenCompleted) {
                block.SetActive(false);
            }
        }
        
        private void _MarkAnimationValidatorsAsValidated()
        {
            foreach (AnimationValidatorEntity animationValidator in _animationValidators) {
                animationValidator.MarkAsValidated();
            }
        }

    }
}