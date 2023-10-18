using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateWalk : AKenneyState
    {
        IMove2DSpeedWriter _speedWriter = null;
        IMove2DLockedReader _lockedReader = null;
        IMove2DDirReader _moveDirReader = null;
        IMove2DOrientWriter _orientWriter = null;
        IMove2DSpeedMaxReader _speedMaxReader = null;
        protected override void OnStateInit()
        {
            //Find Movable Interfaces inside StateMachine
            //You will need to :
            // - Check if movements are locked
            // - Read Move Dir
            // - Read Move SpeedMax
            // - Write Move Orient
            // - Write Move Speed
            _speedWriter = StateMachine.GetComponent<IMove2DSpeedWriter>();
            _lockedReader = StateMachine.GetComponent<IMove2DLockedReader>();
            _moveDirReader = StateMachine.GetComponent<IMove2DDirReader>();
            _orientWriter = StateMachine.GetComponent<IMove2DOrientWriter>();
            _speedMaxReader = StateMachine.GetComponent<IMove2DSpeedMaxReader>();

            if (!_lockedReader.AreMovementsLocked)
            {
                _orientWriter.OrientDir = _moveDirReader.MoveDir;
                _speedWriter.MoveSpeed = _speedMaxReader.MoveSpeedMax;
            }
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            //Force MoveSpeed to MoveSpeedMax
            //Force OrientDir to MoveDir
            _speedWriter.MoveSpeed = _speedMaxReader.MoveSpeedMax;
            _orientWriter.OrientDir = _moveDirReader.MoveDir;
        }

        protected override void OnStateUpdate()
        {
            //Go to State Idle if Movements are locked
            if (_lockedReader.AreMovementsLocked)
            {
                ChangeState(StateMachine.StateIdle);
            }

            //If there is no MoveDir
                //Go to StateDecelerate if MovementsData.StopDecelerationDuration > 0
                //Go to StateIdle otherwise
            if (_moveDirReader.MoveDir == Vector2.zero)
            {
                if (MovementsData.StopDecelerationDuration > 0)
                {
                    ChangeState(StateMachine.StateDecelerate);
                }
                else
                {
                    ChangeState(StateMachine.StateIdle);
                }
            }

            //If MovementsData.TurnBackDecelerationDuration > 0 and the angle between MoveDir and OrientDir > MovementsData.TurnBackAngleThreshold
            //Go to StateTurnBackDecelerate
            if (MovementsData.TurnBackDecelerationDuration > 0 && Vector2.Angle(_moveDirReader.MoveDir, _orientWriter.OrientDir) > MovementsData.TurnBackAngleThreshold)
            {
                ChangeState(StateMachine.StateTurnBackDecelerate);
            }

            //Force OrientDir to MoveDir

            _orientWriter.OrientDir = _moveDirReader.MoveDir;
        }
    }
}