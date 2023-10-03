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
        
        protected override void OnStateInit()
        {
            //Find Movable Interfaces inside StateMachine
            //You will need to :
            // - Check if movements are locked
            // - Read Move Dir
            // - Read Move SpeedMax
            // - Write Move Orient
            // - Write Move Speed
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            //Calculate _timer according to MoveSpeed / MoveSpeedMax / MovementsData.TurnBackAccelerationDuration
        }

        protected override void OnStateUpdate()
        {
            //Go to State Idle if Movements are locked

            //If there is no MoveDir
                //Go to StateDecelerate if MovementsData.StopDecelerationDuration > 0
                //Go to StateIdle otherwise

            //If the angle between MoveDir and OrientDir > MovementsData.TurnBackAngleThreshold
                //Go to StateTurnBackDecelerate

            //Increment _timer with deltaTime
            
            //If _timer > MovementsData.TurnBackAccelerationDuration
                //Go to StateWalk (acceleration is finished)

            //Calculate percent using timer and MovementsData.TurnBackAccelerationDuration
            //Calculate MoveSpeed according to percent and MoveSpeedMax
            //Force OrientDir to MoveDir
        }
    }
}