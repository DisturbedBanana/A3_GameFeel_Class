using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateAccelerate : AKenneyState
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414

        private float _timer = 0f;

        #pragma warning restore 0414
        #endregion

        IMove2DDirReader _moveDirReader = null;
        IMove2DSpeedWriter _speedWriter = null;
        IMove2DLockedReader _lockedReader = null;   
        IMove2DOrientWriter _orientWriter = null;
        IMove2DOrientReader _orientReader = null;
        IMove2DSpeedMaxReader _speedMaxReader = null;

        protected override void OnStateInit()
        {
            //Find Movable Interfaces inside StateMachine
            //You will need to :
            // - Check if movements are locked
            // - Read MoveDir
            // - Read Move SpeedMax
            // - Write Move Orient
            // - Write Move Speed

            base.OnStateInit();
            _moveDirReader = StateMachine.GetComponent<IMove2DDirReader>();
            _speedWriter = StateMachine.GetComponent<IMove2DSpeedWriter>();
            _lockedReader = StateMachine.GetComponent<IMove2DLockedReader>();
            _orientWriter = StateMachine.GetComponent<IMove2DOrientWriter>();
            _orientReader = StateMachine.GetComponent<IMove2DOrientReader>();
            _speedMaxReader = StateMachine.GetComponent<IMove2DSpeedMaxReader>();

            if (!_lockedReader.AreMovementsLocked)
            {
                _orientWriter.OrientDir = _moveDirReader.MoveDir;
                _speedWriter.MoveSpeed = _speedMaxReader.MoveSpeedMax;
            }
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            //Force OrientDir to MoveDir
            //Calculate _timer according to MoveSpeed / MoveSpeedMax / MovementsData.StartAccelerationDuration
            _orientWriter.OrientDir = _moveDirReader.MoveDir;
            _timer = _speedWriter.MoveSpeed / _speedMaxReader.MoveSpeedMax * MovementsData.StartAccelerationDuration;
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
            if (_moveDirReader == null)
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
            //If MovementsData.TurnBackDecelerationDuration > 0 => Go to StateTurnBackDecelerate
            //Else If MovementsData.TurnBackAccelerationDuration > 0 => Go to StateTurnBackAccelerate

            if (Vector2.Angle(_moveDirReader.MoveDir, _orientReader.OrientDir) > MovementsData.TurnBackAngleThreshold)
            {
                   if (MovementsData.TurnBackDecelerationDuration > 0)
                {
                    ChangeState(StateMachine.StateTurnBackDecelerate);
                }
                else if (MovementsData.TurnBackAccelerationDuration > 0)
                {
                    ChangeState(StateMachine.StateTurnBackAccelerate);
                }
            }


                //Increment _timer with deltaTime
                _timer += Time.deltaTime;
            
            //If _timer > MovementsData.StartAccelerationDuration
                //Go to StateWalk (acceleration is finished)
            if (_timer > MovementsData.StartAccelerationDuration)
            {
                ChangeState(StateMachine.StateWalk);
            }

            //Calculate percent using timer and MovementsData.StartAccelerationDuration
            //Calculate MoveSpeed according to percent and MoveSpeedMax
            //Force OrientDir to MoveDir
            float percent = _timer / MovementsData.StartAccelerationDuration;
            //_speedWriter.MoveSpeed = percent * _speedMaxReader.MoveSpeedMax;
            _orientWriter.OrientDir = _moveDirReader.MoveDir;
        }
    }
}