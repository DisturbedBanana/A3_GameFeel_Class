using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateTurnBackAccelerate : AKenneyState
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414

        private float _timer = 0f;

#pragma warning restore 0414
        #endregion


        IMove2DDirReader _moveDirReader = null;
        IMove2DSpeedMaxReader _speedMaxReader = null;
        IMove2DOrientWriter _orientWriter = null;
        IMove2DSpeedWriter _speedWriter = null;
        IMove2DLockedReader _lockedReader = null;

        protected override void OnStateInit()
        {
            //Find Movable Interfaces inside StateMachine
            //You will need to :
            // - Check if movements are locked
            // - Read Move Dir
            // - Read Move SpeedMax
            // - Write Move Orient
            // - Write Move Speed
            _moveDirReader = StateMachine.GetComponent<IMove2DDirReader>();
            _speedMaxReader = StateMachine.GetComponent<IMove2DSpeedMaxReader>();
            _orientWriter = StateMachine.GetComponent<IMove2DOrientWriter>();
            _speedWriter = StateMachine.GetComponent<IMove2DSpeedWriter>();
            _lockedReader = StateMachine.GetComponent<IMove2DLockedReader>();

            if (!_lockedReader.AreMovementsLocked)
            {
                _orientWriter.OrientDir = _moveDirReader.MoveDir;
                _speedWriter.MoveSpeed = _speedMaxReader.MoveSpeedMax;
            }
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            //Calculate _timer according to MoveSpeed / MoveSpeedMax / MovementsData.TurnBackAccelerationDuration
            _timer = _speedWriter.MoveSpeed / _speedMaxReader.MoveSpeedMax * MovementsData.TurnBackAccelerationDuration;
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

            //If the angle between MoveDir and OrientDir > MovementsData.TurnBackAngleThreshold
            //Go to StateTurnBackDecelerate
            if (Vector2.Angle(_moveDirReader.MoveDir, _orientWriter.OrientDir) > MovementsData.TurnBackAngleThreshold)
            {
                ChangeState(StateMachine.StateTurnBackDecelerate);
            }

            //Increment _timer with deltaTime
            _timer += Time.deltaTime;

            //If _timer > MovementsData.TurnBackAccelerationDuration
            //Go to StateWalk (acceleration is finished)
            if (_timer > MovementsData.TurnBackAccelerationDuration)
            {
                ChangeState(StateMachine.StateWalk);
            }

            //Calculate percent using timer and MovementsData.TurnBackAccelerationDuration
            //Calculate MoveSpeed according to percent and MoveSpeedMax
            //Force OrientDir to MoveDir
            float percent = _timer / MovementsData.TurnBackAccelerationDuration;
            _speedWriter.MoveSpeed = percent * _speedMaxReader.MoveSpeedMax;
            _orientWriter.OrientDir = _moveDirReader.MoveDir;
        }
    }
}