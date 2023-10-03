using UnityEngine;

namespace IIMEngine.Movements2D
{
    public class Movable2D : MonoBehaviour,
        IMove2DDirReader, IMove2DDirWriter,
        IMove2DOrientReader, IMove2DOrientWriter,
        IMove2DSpeedReader, IMove2DSpeedWriter,
        IMove2DSpeedMaxReader, IMove2DSpeedMaxWriter,
        IMove2DTurnBackReader, IMove2DTurnBackWriter,
        IMove2DLockedReader, IMove2DLockedWriter
    {
        public Vector2 MoveDir { get; set; }

        private Vector2 _orientDir = Vector2.right;
        
        public Vector2 OrientDir {
            get => _orientDir;
            set {
                _orientDir = value;
                if (_orientDir.x != 0f) {
                    OrientX = Mathf.Sign(_orientDir.x);
                }
                if (_orientDir.y != 0f) {
                    OrientY = Mathf.Sign(_orientDir.y);
                }
            }
        }

        public float OrientX { get; set; } = 1f;
        public float OrientY { get; set; } = 0f;
        public float MoveSpeed { get; set; }
        public float MoveSpeedMax { get; set; }
        public bool IsTurningBack { get; set; }
        public bool AreMovementsLocked { get; set; }

        [Header("Rigidbody")]
        [SerializeField] private Rigidbody2D _rigidbody;
        private Vector2 _velocity = Vector2.zero;

        private void FixedUpdate()
        {
            if (AreMovementsLocked) {
                _velocity.x = 0f;
                _velocity.y = 0f;
            } else {
                _velocity = OrientDir * MoveSpeed;
            }

            _rigidbody.velocity = _velocity;
        }

    }
}