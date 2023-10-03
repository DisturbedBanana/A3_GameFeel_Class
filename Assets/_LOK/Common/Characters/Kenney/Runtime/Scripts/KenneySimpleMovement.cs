using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneySimpleMovement : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Movements")]
        [SerializeField] private KenneyMovementsData _movementsData;

#pragma warning restore 0414
        #endregion

        IMove2DOrientWriter _orientWriter = null;
        IMove2DSpeedWriter _speedWriter = null;
        IMove2DLockedReader _lockedReader = null;
        IMove2DDirReader _moveDirReader = null;
        Vector2 _moveDir = Vector2.zero;

        private void Awake()
        {
            //Find Movable Interfaces (at the root of this gameObject)
            //You will need to :
            // - Check if movements are locked
            // - Read Move Dir
            // - Write Move Orient
            // - Write Move Speed

            _orientWriter = GetComponent<IMove2DOrientWriter>();
            _speedWriter = GetComponent<IMove2DSpeedWriter>();
            _lockedReader = GetComponent<IMove2DLockedReader>();
            _moveDirReader = GetComponent<IMove2DDirReader>();

            if (!_lockedReader.AreMovementsLocked)
            {
                
            }
            _orientWriter.OrientDir = Vector2.zero;
            _speedWriter.MoveSpeed = 5f;
        }

        private void Update()
        {
            //If Movements are locked
            //Force MoveSpeed to 0

            //If there is MoveDir
                //Set MoveSpeed to MovementsData.SpeedMax
                //Set Move OrientDir to Movedir
            //Else
                //Set MoveSpeed to 0

            if (_lockedReader.AreMovementsLocked)
                _speedWriter.MoveSpeed = 0f;

            if (_moveDirReader.MoveDir != Vector2.zero)
            {
                _speedWriter.MoveSpeed = _movementsData.SpeedMax;
                _orientWriter.OrientDir = _moveDirReader.MoveDir;
            }
            else
            {
                _speedWriter.MoveSpeed = 0f;
            }
        }
    }
}