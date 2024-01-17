using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateIdle : AKenneyState
    {
        IMove2DSpeedWriter _speedWriter = null;
        IMove2DLockedReader _lockedReader = null;
        IMove2DDirReader _moveDirReader = null;
        
        protected override void OnStateInit()
        {
            //Find Movable Interfaces inside StateMachine
            _speedWriter = StateMachine.GetComponent<IMove2DSpeedWriter>();
            _lockedReader = StateMachine.GetComponent<IMove2DLockedReader>();
            _moveDirReader = StateMachine.GetComponent<IMove2DDirReader>();

            //You will need to write Speed and check if movements are locked and read MoveDir
            if (!_lockedReader.AreMovementsLocked && _moveDirReader.MoveDir != Vector2.zero)
            {
                _speedWriter.MoveSpeed = 5f;
            }
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            //Force MoveSpeed to 0
            _speedWriter.MoveSpeed = 0f;
        }

        protected override void OnStateUpdate()
        {
            //Do nothing if movements are locked
            if (!_lockedReader.AreMovementsLocked)
            {
                if (_moveDirReader.MoveDir != Vector2.zero)
                {
                    if (MovementsData.StartAccelerationDuration > 0)
                        ChangeState(StateMachine.StateAccelerate);
                    else
                        ChangeState(StateMachine.StateWalk);
                }
            }

            //If there is MoveDir
            //Change to StateAccelerate if MovementsData.StartAccelerationDuration > 0
            //Change to StateWalk otherwise
        }
    }
}