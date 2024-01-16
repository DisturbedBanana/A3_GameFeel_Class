using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateDecelerate : AKenneyState
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414

        private float _timer = 0f;

        #pragma warning restore 0414
        #endregion
        IMove2DDirReader _moveDirReader = null;
        IMove2DSpeedWriter _speedWriter = null;
        IMove2DLockedReader _lockedReader = null;
        IMove2DOrientReader _orientReader = null;
        IMove2DSpeedMaxReader _speedMaxReader = null;

        protected override void OnStateInit()
        {
            //Find Movable Interfaces inside StateMachine
            //You will need to :
            // - Check if movements are locked
            // - Read Move Dir
            // - Read Move SpeedMax
            // - Read Move Orient
            // - Write Move Speed

            base.OnStateInit();
            _moveDirReader = StateMachine.GetComponent<IMove2DDirReader>();
            _speedWriter = StateMachine.GetComponent<IMove2DSpeedWriter>();
            _lockedReader = StateMachine.GetComponent<IMove2DLockedReader>();
            _orientReader = StateMachine.GetComponent<IMove2DOrientReader>();
            _speedMaxReader = StateMachine.GetComponent<IMove2DSpeedMaxReader>();

            if (!_lockedReader.AreMovementsLocked)
            {
                _speedWriter.MoveSpeed = _speedMaxReader.MoveSpeedMax;
            }
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            //Calculate _timer according to MoveSpeed / MoveSpeedMax / MovementsData.StopDecelerationDuration
            _timer = _speedWriter.MoveSpeed / _speedMaxReader.MoveSpeedMax * MovementsData.StopDecelerationDuration;
        }

        protected override void OnStateUpdate()
        {
            //Go to State Idle if Movements are locked
            if (_lockedReader.AreMovementsLocked)
            {
                ChangeState(StateMachine.StateIdle);
            }

            //If there is MoveDir
            //If the angle between MoveDir and OrientDir > MovementsData.TurnBackAngleThreshold
            //If MovementsData.TurnBackDecelerationDuration > 0 => Go to StateTurnBackDecelerate
            //Else If MovementsData.TurnBackAccelerationDuration > 0 => Go to StateTurnBackAccelerate
            //Else Go to StateAccelerate if MovementsData.StartAccelerationDuration > 0f
            //Else Go to StateWalk
            if (_moveDirReader.MoveDir != Vector2.zero)
            {
                if (Vector2.Angle(_moveDirReader.MoveDir, _orientReader.OrientDir) > MovementsData.TurnBackAngleThreshold)
                {
                    if (MovementsData.TurnBackDecelerationDuration > 0)
                    {
                        ChangeState(StateMachine.StateTurnBackDecelerate);
                    }
                    else if(MovementsData.TurnBackAccelerationDuration > 0)
                    {
                        ChangeState(StateMachine.StateTurnBackDecelerate);
                    }
                    else
                    {
                        if (MovementsData.StartAccelerationDuration > 0f)
                        {
                            ChangeState(StateMachine.StateAccelerate);
                        }
                        else
                        {
                            ChangeState(StateMachine.StateWalk);
                        }
                    }
                }
            }

            //Increment _timer with deltaTime
            _timer += Time.deltaTime;
            
            //If _timer > MovementsData.StopDecelerationDuration
                //Go to StateIdle (deceleration is finished)
            if (_timer > MovementsData.StopDecelerationDuration)
            {
                _speedWriter.MoveSpeed = 0;
                ChangeState(StateMachine.StateIdle);
            }

            //Calculate percent using timer and MovementsData.StopDecelerationDuration
            //Calculate MoveSpeed according to percent and MoveSpeedMax
            float percent = _timer / MovementsData.StopDecelerationDuration;
            //_speedWriter.MoveSpeed = percent * _speedMaxReader.MoveSpeedMax;


        }
    }
}