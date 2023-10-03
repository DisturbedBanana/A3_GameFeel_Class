using System;
using System.Collections;
using IIMEngine.Entities;
using LOK.Common.Characters.Kenney;
using IIMEngine.Effects.Common;
using LOK.Core.Room;
using LOK.Core.Switch;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace LOK.CaveStart
{
    public class RoomMovementsValidateEffects : MonoBehaviour, IRoomInitHandler, IRoomStartHandler, IRoomEndHandler, IRoomValidateHandler
    {
        [Header("Player Entity")]
        [SerializeField] private string _playerEntityID;
        private Entity _playerEntity = null;

        //Switches
        [Serializable]
        public class SwitchValidatorMapping
        {
            public EffectValidatorEntity.ValidatorType validatorType = EffectValidatorEntity.ValidatorType.Bounce;
            public SwitchEntity switchEntity;
        }
        [Header("Switches")]
        [SerializeField] private SwitchValidatorMapping[] _switchValidatorMappings;

        [Header("Blocks to Remove")]
        [SerializeField] private GameObject[] _blocksToRemove;

        [Header("Validate Feedbacks")]
        [SerializeField] private MMF_Player _validateFeedbacks;

        [Header("Kenney Movements")]
        [SerializeField] private KenneyMovementsData _movementsData;

        [Header("Effects")]
        [SerializeField] private string _effectJumpOnMoveDefaultName = "";
        private EffectJump _effectJumpOnMoveDefault = null;

        [SerializeField] private string _effectRotateOnMoveDefaultName = "";
        private EffectRotate _effectRotateOnMoveDefault = null;

        [SerializeField] private string _effectBounceOnMoveDefaultName = "";
        private EffectBounce _effectBounceOnMoveDefault = null;

        [SerializeField] private string _effectJumpOnMoveValidateName = "";
        private EffectJump _effectJumpOnMoveValidate = null;

        [SerializeField] private string _effectRotateOnMoveValidateName = "";
        private EffectRotate _effectRotateOnMoveValidate = null;

        [SerializeField] private string _effectBounceOnMoveValidateName = "";
        private EffectBounce _effectBounceOnMoveValidate = null;

        private EffectValidatorEntity[] _effectValidators;

        private bool _jumpEffectValidated = false;
        private bool _rotateEffectValidated = false;
        private bool _bounceEffectValidated = false;

        private int _currentBlockIndex = 0;

        private Room _room;

        private void Awake()
        {
            _effectValidators = GetComponentsInChildren<EffectValidatorEntity>();
        }

        private void Update()
        {
            _CheckJumpEffectValidation();
            _CheckRotateEffectValidation();
            _CheckBounceEffectValidation();
        }

        private void _CheckJumpEffectValidation()
        {
            if (_jumpEffectValidated) return;

            if (_AreEffectValidatorsValidated(EffectValidatorEntity.ValidatorType.Jump)) {
                RoomValidateOneLock(EffectValidatorEntity.ValidatorType.Jump);
                _jumpEffectValidated = true;
            }
        }

        private void _CheckRotateEffectValidation()
        {
            if (_rotateEffectValidated) return;

            if (_AreEffectValidatorsValidated(EffectValidatorEntity.ValidatorType.Rotate)) {
                RoomValidateOneLock(EffectValidatorEntity.ValidatorType.Rotate);
                _rotateEffectValidated = true;
            }
        }

        private void _CheckBounceEffectValidation()
        {
            if (_bounceEffectValidated) return;

            if (_AreEffectValidatorsValidated(EffectValidatorEntity.ValidatorType.Bounce)) {
                RoomValidateOneLock(EffectValidatorEntity.ValidatorType.Bounce);
                _bounceEffectValidated = true;
            }
        }

        public void RoomValidateOneLock(EffectValidatorEntity.ValidatorType validatorType)
        {
            StartCoroutine(_CoroutineRoomValidateOneBlock(validatorType));
        }

        private IEnumerator _CoroutineRoomValidateOneBlock(EffectValidatorEntity.ValidatorType validatorType)
        {
            yield return _validateFeedbacks.PlayFeedbacksCoroutine(transform.position);
            _DisableSwitch(validatorType);
            if (_bounceEffectValidated && _jumpEffectValidated && _rotateEffectValidated) {
                _room.RoomValidate();
            }
        }

        public void OnRoomValidated(Room room)
        {
            _effectJumpOnMoveDefault.gameObject.SetActive(true);
            _effectRotateOnMoveDefault.gameObject.SetActive(true);
            _effectBounceOnMoveDefault.gameObject.SetActive(true);

            _effectJumpOnMoveValidate.gameObject.SetActive(false);
            _effectRotateOnMoveValidate.gameObject.SetActive(false);
            _effectBounceOnMoveValidate.gameObject.SetActive(false);
            _DisableAllSwitches();
        }

        public void OnRoomInit(Room room)
        {
            _room = room;
            if (_room.IsCompleted) {
                _MarkAllValidatorsAsValidated();
                _DisableAllSwitches();
                RemoveAllBlocks();
                _jumpEffectValidated = true;
                _rotateEffectValidated = true;
                _bounceEffectValidated = true;
            }

            _playerEntity = EntitiesGlobal.GetEntityByID(_playerEntityID);

            foreach (EffectJump effect in _playerEntity.GetComponentsInChildren<EffectJump>(true)) {
                if (effect.name == _effectJumpOnMoveValidateName) {
                    _effectJumpOnMoveValidate = effect;
                } else if (effect.name == _effectJumpOnMoveDefaultName) {
                    _effectJumpOnMoveDefault = effect;
                }
            }

            foreach (EffectRotate effect in _playerEntity.GetComponentsInChildren<EffectRotate>(true)) {
                if (effect.name == _effectRotateOnMoveValidateName) {
                    _effectRotateOnMoveValidate = effect;
                } else if (effect.name == _effectRotateOnMoveDefaultName) {
                    _effectRotateOnMoveDefault = effect;
                }
            }

            foreach (EffectBounce effect in _playerEntity.GetComponentsInChildren<EffectBounce>(true)) {
                if (effect.name == _effectBounceOnMoveValidateName) {
                    _effectBounceOnMoveValidate = effect;
                } else if (effect.name == _effectBounceOnMoveDefaultName) {
                    _effectBounceOnMoveDefault = effect;
                }
            }
        }

        public void OnRoomStart(Room room)
        {
            if (!room.IsCompleted) {
                _effectJumpOnMoveDefault.gameObject.SetActive(false);
                _effectRotateOnMoveDefault.gameObject.SetActive(false);
                _effectBounceOnMoveDefault.gameObject.SetActive(false);
            }
        }

        public void OnRoomEnd(Room room)
        {
            _effectJumpOnMoveDefault.gameObject.SetActive(true);
            _effectRotateOnMoveDefault.gameObject.SetActive(true);
            _effectBounceOnMoveDefault.gameObject.SetActive(true);
        }

        public void StartCheckEffectJump()
        {
            if (_jumpEffectValidated) return;
            _effectJumpOnMoveValidate.gameObject.SetActive(true);
            _effectRotateOnMoveValidate.gameObject.SetActive(false);
            _effectBounceOnMoveValidate.gameObject.SetActive(false);
            _ResetValidatorsState(EffectValidatorEntity.ValidatorType.Jump);
        }

        public void StartCheckEffectRotate()
        {
            if (_rotateEffectValidated) return;
            _effectJumpOnMoveValidate.gameObject.SetActive(false);
            _effectRotateOnMoveValidate.gameObject.SetActive(true);
            _effectBounceOnMoveValidate.gameObject.SetActive(false);
            _ResetValidatorsState(EffectValidatorEntity.ValidatorType.Rotate);
        }

        public void StartCheckEffectBounce()
        {
            if (_bounceEffectValidated) return;
            _effectJumpOnMoveValidate.gameObject.SetActive(false);
            _effectRotateOnMoveValidate.gameObject.SetActive(false);
            _effectBounceOnMoveValidate.gameObject.SetActive(true);
            _ResetValidatorsState(EffectValidatorEntity.ValidatorType.Bounce);
        }

        public void RemoveOneBlock()
        {
            if (_currentBlockIndex >= _blocksToRemove.Length) return;
            _blocksToRemove[_currentBlockIndex].SetActive(false);
            _currentBlockIndex++;
        }

        public void RemoveAllBlocks()
        {
            foreach (GameObject block in _blocksToRemove) {
                block.gameObject.SetActive(false);
            }
        }

        private void _ResetValidatorsState(EffectValidatorEntity.ValidatorType validatorType)
        {
            foreach (EffectValidatorEntity validator in _effectValidators) {
                if (validator.CurrentValidatorType == validatorType) {
                    validator.ResetValidationState();
                }
            }
        }

        private bool _AreEffectValidatorsValidated(EffectValidatorEntity.ValidatorType validatorType)
        {
            foreach (EffectValidatorEntity validator in _effectValidators) {
                if (validator.CurrentValidatorType != validatorType) continue;
                if (!validator.IsValid) return false;
            }

            return true;
        }

        private void _MarkAllValidatorsAsValidated()
        {
            foreach (EffectValidatorEntity validator in _effectValidators) {
                validator.MarkAsValidated();
            }
        }

        private void _DisableSwitch(EffectValidatorEntity.ValidatorType validatorType)
        {
            foreach (SwitchValidatorMapping mapping in _switchValidatorMappings) {
                if(mapping.validatorType != validatorType) continue;
                mapping.switchEntity.StartState = SwitchEntity.State.Disabled;
                mapping.switchEntity.SwitchDisable();
            }
        }

        private void _DisableAllSwitches()
        {
            foreach (SwitchValidatorMapping mapping in _switchValidatorMappings) {
                mapping.switchEntity.StartState = SwitchEntity.State.Disabled;
                mapping.switchEntity.SwitchDisable();
            }
        }
    }
}