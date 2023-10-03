using System;
using IIMEngine.Entities;
using IIMEngine.SFX;
using IIMEngine.Effects.Common;
using LOK.Core.Globals;
using LOK.Core.Room;
using UnityEngine;

namespace LOK.CaveStart
{
    public class EffectValidatorEntity : MonoBehaviour, IRoomInitHandler
    {
        private const string EFFECT_VALIDATE_SUFFIX = "_EffectValidate";

        [Header("Player Entity")]
        [SerializeField] private string _playerEntityID;
        private Entity _playerEntity = null;

        public enum ValidatorType
        {
            Jump = 0,
            Rotate,
            Bounce
        }

        [Header("Validator")]
        [SerializeField] private ValidatorType _validatorType = ValidatorType.Jump;

        public ValidatorType CurrentValidatorType => _validatorType;

        [Header("Visuals")]
        [SerializeField] private SpriteRenderer _validSpriteRenderer = null;
        [SerializeField] private SpriteRenderer _invalidSpriteRenderer = null;

        [Header("Sounds")]
        [SerializeField] private string _sfxIDValidated = SFXs.VALIDATION_DEFAULT;
        [SerializeField] private string _sfxIDInvalid = "";

        private EffectJump _effectJumpOnMove = null;
        private EffectRotate _effectRotateOnMove = null;
        private EffectBounce _effectBounceOnMove = null;

        public bool IsValid { get; private set; } = false;

        public bool IsValidationChecked { get; private set; } = false;

        private void Awake()
        {
            HideVisuals();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsValidationChecked) return;
            if (!other.gameObject.CompareTag(Tags.PLAYER)) return;

            IsValid = _CheckEffectValid();
            IsValidationChecked = true;
            if (IsValid) {
                _validSpriteRenderer.gameObject.SetActive(true);
                _invalidSpriteRenderer.gameObject.SetActive(false);
                SFXsManager.Instance.PlaySound(_sfxIDValidated);
            } else {
                _validSpriteRenderer.gameObject.SetActive(false);
                _invalidSpriteRenderer.gameObject.SetActive(true);
                SFXsManager.Instance.PlaySound(_sfxIDInvalid);
            }
        }

        private void HideVisuals()
        {
            _validSpriteRenderer.gameObject.SetActive(false);
            _invalidSpriteRenderer.gameObject.SetActive(false);
        }

        public void ResetValidationState()
        {
            IsValid = false;
            IsValidationChecked = false;
            HideVisuals();
        }

        public void MarkAsValidated()
        {
            IsValid = true;
            IsValidationChecked = true;
            _validSpriteRenderer.gameObject.SetActive(true);
            _invalidSpriteRenderer.gameObject.SetActive(false);
        }

        public void OnRoomInit(Room room)
        {
            _playerEntity = EntitiesGlobal.GetEntityByID(_playerEntityID);

            _effectJumpOnMove = _FindFakeEffect<EffectJump>(_playerEntity);
            _effectRotateOnMove = _FindFakeEffect<EffectRotate>(_playerEntity);
            _effectBounceOnMove = _FindFakeEffect<EffectBounce>(_playerEntity);
        }

        private T _FindFakeEffect<T>(Entity playerEntity) where T : Component
        {
            foreach (T effect in playerEntity.GetComponentsInChildren<T>(true)) {
                if (effect.name.Contains(EFFECT_VALIDATE_SUFFIX)) {
                    return effect;
                }
            }

            return null;
        }

        private bool _CheckEffectValid()
        {
            switch (_validatorType) {
                case ValidatorType.Jump: return _IsEffectJumpValid();
                case ValidatorType.Rotate: return _IsEffectRotationValid();
                case ValidatorType.Bounce: return _IsEffectBounceValid();
            }

            return false;
        }

        private bool _IsEffectJumpValid()
        {
            Vector3 position = _effectJumpOnMove.ObjectToMove.localPosition;
            return position.y > 0f && position.y <= _effectJumpOnMove.JumpHeight;
        }

        private bool _IsEffectRotationValid()
        {
            Vector3 eulerAngles = _effectRotateOnMove.ObjectToRotate.localEulerAngles;
            if (eulerAngles.z == 0f) return false;
            if (eulerAngles.z > _effectRotateOnMove.RotationAngle && eulerAngles.z < 360f - _effectRotateOnMove.RotationAngle) return false;
            return true;
        }

        private bool _IsEffectBounceValid()
        {
            Vector3 scale = _effectBounceOnMove.ObjectToScale.localScale;

            if (Math.Abs(scale.x - 1f) < Mathf.Epsilon) return false;
            if (scale.x < 1f - Mathf.Abs(_effectBounceOnMove.BounceFactorX)) return false;
            if (scale.x > 1f + Mathf.Abs(_effectBounceOnMove.BounceFactorX)) return false;

            if (Math.Abs(scale.y - 1f) < Mathf.Epsilon) return false;
            if (scale.y < 1f - Mathf.Abs(_effectBounceOnMove.BounceFactorY)) return false;
            if (scale.y > 1f + Mathf.Abs(_effectBounceOnMove.BounceFactorY)) return false;

            return true;
        }
    }
}