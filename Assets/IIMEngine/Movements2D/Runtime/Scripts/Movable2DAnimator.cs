using UnityEngine;

namespace IIMEngine.Movements2D
{
    public class Movable2DAnimator : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Movable")]
        [SerializeField] private GameObject _movableGameObject;

        [Header("Parameters")]
        [SerializeField] private string _isMovingParameter = "IsMoving";
        private int _isMovingParameterHash;
        
        [Header("Animation Speed")]
        [SerializeField] private float _animatorSpeedMin = 0.5f;
        [SerializeField] private float _animatorSpeedMax = 1f;
        
        #pragma warning restore 0414
        #endregion

        private IMove2DSpeedReader _speedReader;
        private IMove2DLockedReader _lockedReader;
        private Animator _animator;
        private bool _isMoving;

        private void Awake()
        {
            //Convert _isMovingParameter to Hash 
            //(Improve performance when calling Animator.SetParameter)
            _isMovingParameterHash = Animator.StringToHash(_isMovingParameter);
            
            //Find Movable Interfaces inside _movableGameObject needed to check if object is moving
            //(You'll probably need to check if object movements are locked and if object move speed > 0)
            _speedReader = _movableGameObject.GetComponent<IMove2DSpeedReader>();
            _lockedReader = _movableGameObject.GetComponent<IMove2DLockedReader>();
            
            //Find Animator (attached to this gameObject)
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            //Check if object is moving (store it inside a bool)
            if(_speedReader.MoveSpeed != 0 && !_lockedReader.AreMovementsLocked)
                _isMoving = true;
            else
                _isMoving = false;


            //Bonus : Get Object movement speed and speed max to interpolate animator speed
            //Set animator parameter bool "IsMoving" according to movements infos
            _animator.SetBool(_isMovingParameterHash, _isMoving);
        }
    }
}