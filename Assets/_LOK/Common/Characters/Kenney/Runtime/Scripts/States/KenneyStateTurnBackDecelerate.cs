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

            if (!_lockedReader.AreMovementsLocked)
            {

            }
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            //Set IsTurningBack to true
            
            //Calculate _timer according to MoveSpeed / MoveSpeedMax / MovementsData.TurnBackDecelerationDuration
        }

        protected override void OnStateExit(AKenneyState nextState)
        {
            //Set IsTurningBack to false
        }
        
        protected override void OnStateUpdate()
        {
            //Go to State Idle if Movements are locked

            //If there is MoveDir and the angle between MoveDir and OrientDir > MovementsData.TurnBackAngleThreshold
                //Go to StateAccelerate

            //Increment _timer with deltaTime

            //If _timer > MovementsData.TurnBackDecelerationDuration
                //Go to StateTurnBackAccelerate if there is MoveDir
                //Go to StateIdle otherwise

            //Calculate percent using timer and MovementsData.TurnBackDecelerationDuration
            //Calculate MoveSpeed according to percent and MoveSpeedMax
        }
    }
}