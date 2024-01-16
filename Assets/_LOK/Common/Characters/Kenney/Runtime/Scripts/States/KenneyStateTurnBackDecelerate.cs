using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateTurnBackDecelerate : AKenneyState
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414

        private float _timer = 0f;

#pragma warning restore 0414
        #endregion

        IMove2DLockedReader _lockedReader = null;
        IMove2DDirReader _dirReader = null;
        IMove2DSpeedMaxReader _speedMaxReader = null;
        IMove2DOrientReader _orientReader = null;
        IMove2DSpeedWriter _speedWriter = null;
        IMove2DTurnBackWriter _turnBackWriter = null;

        protected override void OnStateInit()
        {
            //Find Movable Interfaces inside StateMachine
            //You will need to :
            // - Check if movements are locked
            // - Read Move Dir
            // - Read Move SpeedMax
            // - Read Move Orient
            // - Write Move Speed
            // - Write Move IsTurningBack

            _lockedReader = StateMachine.GetComponent<IMove2DLockedReader>();
            _dirReader = StateMachine.GetComponent<IMove2DDirReader>();
            _speedMaxReader = StateMachine.GetComponent<IMove2DSpeedMaxReader>();
            _orientReader = StateMachine.GetComponent<IMove2DOrientReader>();
            _speedWriter = StateMachine.GetComponent<IMove2DSpeedWriter>();
            _turnBackWriter = StateMachine.GetComponent<IMove2DTurnBackWriter>();

            if (!_lockedReader.AreMovementsLocked)
            {
                _turnBackWriter.IsTurningBack = true;
                _speedWriter.MoveSpeed = _speedMaxReader.MoveSpeedMax;
            }
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            //Set IsTurningBack to true
            _turnBackWriter.IsTurningBack = true;
            
            //Calculate _timer according to MoveSpeed / MoveSpeedMax / MovementsData.TurnBackDecelerationDuration
            _timer = _speedWriter.MoveSpeed / _speedMaxReader.MoveSpeedMax * MovementsData.TurnBackDecelerationDuration;
        }

        protected override void OnStateExit(AKenneyState nextState)
        {
            //Set IsTurningBack to false
            _turnBackWriter.IsTurningBack = false;
        }
        
        protected override void OnStateUpdate()
        {
            //Go to State Idle if Movements are locked
            if (_lockedReader.AreMovementsLocked)
            {
                ChangeState(StateMachine.StateIdle);
            }

            //If there is MoveDir and the angle between MoveDir and OrientDir > MovementsData.TurnBackAngleThreshold, Go to StateAccelerate
            if (_dirReader.MoveDir != Vector2.zero && Vector2.Angle(_dirReader.MoveDir, _orientReader.OrientDir) > MovementsData.TurnBackAngleThreshold)
            {
                ChangeState(StateMachine.StateTurnBackAccelerate);
            }

            _timer += Time.deltaTime;


            if (_timer > MovementsData.TurnBackDecelerationDuration)
            {
                if (_dirReader.MoveDir != Vector2.zero)
                {
                    ChangeState(StateMachine.StateTurnBackAccelerate);
                }
                else
                {
                    ChangeState(StateMachine.StateIdle);
                }
            }

            float percent = _timer / MovementsData.TurnBackDecelerationDuration;
            _speedWriter.MoveSpeed = Mathf.Lerp(_speedMaxReader.MoveSpeedMax, 0f, percent);
        }
    }
}