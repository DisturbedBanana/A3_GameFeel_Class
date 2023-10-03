using IIMEngine.Entities;
using IIMEngine.SFX;
using LOK.Core.Globals;
using LOK.Core.Room;
using LOK.Common.Characters.Kenney;
using UnityEngine;

namespace LOK.CaveStart
{
    public class ArrowValidatorEntity : MonoBehaviour, IRoomInitHandler
    {
        public enum DirectionType
        {
            Up = 0,
            Down,
            Left,
            Right,
        }

        [Header("Direction")]
        [SerializeField] private DirectionType _directionType = DirectionType.Left;

        [Header("Player Entity")]
        [SerializeField] private string _playerEntityID;
        private Entity _playerEntity = null;
        
        [Header("Visuals")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _validatedColor = Color.green;

        [Header("Box Collider")]
        [SerializeField] private BoxCollider2D _boxCollider;

        [Header("Sounds")]
        [SerializeField] private string _sfxIDValidated = SFXs.VALIDATION_DEFAULT;

        [Header("Kenney Move Settings")]
        [SerializeField] private KenneyMovementsData _movementsData;
        
        public bool IsValidated { get; set; } = false;

        public bool IsPlayerInside { get; set; } = false;
        private Rigidbody2D _entityRigidbody = null;
        private float _playerValidSpeed = 0f;


        private void Awake()
        {
            _spriteRenderer.material.SetFloat("_FillRatio", 0f);
        }

        private void Update()
        {
            if (!IsPlayerInside) return;
            if (!_IsPlayerMovementsValid()) return;

            float ratio = 0f;
            Bounds boxBounds = _boxCollider.bounds;
            switch (_directionType) {
                case DirectionType.Right:
                    ratio = Mathf.InverseLerp(
                        boxBounds.min.x,
                        boxBounds.max.x,
                        _playerEntity.transform.position.x
                    );
                    break;

                case DirectionType.Left:
                    ratio = Mathf.InverseLerp(
                        boxBounds.max.x,
                        boxBounds.min.x,
                        _playerEntity.transform.position.x
                    );
                    break;

                case DirectionType.Up:
                    ratio = Mathf.InverseLerp(
                        boxBounds.min.y,
                        boxBounds.max.y,
                        _playerEntity.transform.position.y
                    );
                    break;

                case DirectionType.Down:
                    ratio = Mathf.InverseLerp(
                        boxBounds.max.y,
                        boxBounds.min.y,
                        _playerEntity.transform.position.y
                    );
                    break;
            }

            _spriteRenderer.material.SetFloat("_FillRatio", ratio);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsValidated) return;
            if (other.gameObject.CompareTag(Tags.PLAYER)) {
                IsPlayerInside = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsValidated) return;
            if (other.gameObject.CompareTag(Tags.PLAYER)) {
                IsPlayerInside = false;
                if (_IsPlayerMovementsValid()) {
                    MarkAsValidated();
                    SFXsManager.Instance.PlaySound(_sfxIDValidated);
                } else {
                    _spriteRenderer.material.SetFloat("_FillRatio", 0f);
                }
            }
        }

        public void OnRoomInit(Room room)
        {
            _playerEntity = EntitiesGlobal.GetEntityByID(_playerEntityID);
            _entityRigidbody = _playerEntity.GetComponent<Rigidbody2D>();
            _playerValidSpeed = _movementsData.SpeedMax;
        }

        private bool _IsPlayerMovementsValid()
        {
            switch (_directionType) {
                case DirectionType.Right: return _entityRigidbody.velocity == Vector2.right * _playerValidSpeed;
                case DirectionType.Left: return _entityRigidbody.velocity == Vector2.left * _playerValidSpeed;
                case DirectionType.Up: return _entityRigidbody.velocity == Vector2.up * _playerValidSpeed;
                case DirectionType.Down: return _entityRigidbody.velocity == Vector2.down * _playerValidSpeed;
            }

            return false;
        }

        public void MarkAsValidated()
        {
            IsValidated = true;
            _spriteRenderer.material.SetFloat("_FillRatio", 1f);
            _spriteRenderer.material.SetColor("_FillColor", _validatedColor);
        }
    }
}