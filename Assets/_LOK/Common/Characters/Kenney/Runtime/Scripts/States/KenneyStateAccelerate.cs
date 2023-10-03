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

        protected override void OnStateInit()
        {
            //Find Movable Interfaces inside StateMachine
            //You will need to :
            // - Check if movements are locked
            // - Read MoveDir
            // - Read Move SpeedMax
            // - Write Move Orient
            // - Write Move Speed
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            //Force OrientDir to MoveDir
            //Calculate _timer according to MoveSpeed / MoveSpeedMax / MovementsData.StartAccelerationDuration
        }

        protected override void OnStateUpdate()
        {
            //Go to State Idle if Movements are locked

            //If there is no MoveDir
                //Go to StateDecelerate if MovementsData.StopDecelerationDuration > 0
                //Go to StateIdle otherwise

            //If the angle between MoveDir and OrientDir > MovementsData.TurnBackAngleThreshold
                //If MovementsData.TurnBackAccelerationDuration > 0 => Go to StateTurnBackDecelerate
                //Else If MovementsData.TurnBackAccelerationDuration > 0 => Go to StateTurnBackAccelerate

            //Increment _timer with deltaTime
            
            //If _timer > MovementsData.StartAccelerationDuration
                //Go to StateWalk (acceleration is finished)

            //Calculate percent using timer and MovementsData.StartAccelerationDuration
            //Calculate MoveSpeed according to percent and MoveSpeedMax
            //Force OrientDir to MoveDir
        }
    }
}