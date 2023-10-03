using IIMEngine.Entities;
using IIMEngine.SFX;
using LOK.Core.Globals;
using LOK.Core.Room;
using UnityEngine;

namespace LOK.CaveStart
{
    public class AnimationValidatorEntity : MonoBehaviour, IRoomInitHandler
    {
        private const string MATERIAL_PARAM_COLOR = "_Color1";

        private const string ANIMATION_STATE_ID_IDLE = "Idle";
        private const string ANIMATION_STATE_ID_MOVE = "Move";

        public enum AnimationType
        {
            Idle = 0,
            Move,
        }

        [Header("Animation To Check")]
        [SerializeField] private AnimationType _animationToCheck = AnimationType.Idle;
        private Animator _playerUnityAnimator = null;

        [Header("Visuals")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _validatedColor = Color.green;

        [Header("Sounds")]
        [SerializeField] private string _sfxIDValidated = SFXs.VALIDATION_DEFAULT;


        public bool IsValidated { get; set; } = false;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (IsValidated) return;
            if (!other.gameObject.CompareTag(Tags.PLAYER)) return;

            if (_AnimatorHasValidAnimation()) {
                MarkAsValidated();
                SFXsManager.Instance.PlaySound(_sfxIDValidated);
            }
        }

        public void MarkAsValidated()
        {
            IsValidated = true;
            _spriteRenderer.material.SetColor(MATERIAL_PARAM_COLOR, _validatedColor);
        }

        private bool _AnimatorHasValidAnimation()
        {
            if (_playerUnityAnimator == null) return false;
            AnimatorStateInfo stateInfo = _playerUnityAnimator.GetCurrentAnimatorStateInfo(0);

            switch (_animationToCheck) {
                case AnimationType.Idle: return stateInfo.IsName(ANIMATION_STATE_ID_IDLE);
                case AnimationType.Move: return stateInfo.IsName(ANIMATION_STATE_ID_MOVE);
            }

            return false;
        }


        public void OnRoomInit(Room room)
        {
            _playerUnityAnimator = EntitiesGlobal.GetEntityByID("Kenney").GetComponentInChildren<Animator>();
        }
    }
}