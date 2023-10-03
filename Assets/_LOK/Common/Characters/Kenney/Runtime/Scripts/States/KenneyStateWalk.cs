using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateWalk : AKenneyState
    {
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
            //Force MoveSpeed to MoveSpeedMax
            //Force OrientDir to MoveDir
        }

        protected override void OnStateUpdate()
        {
            //Go to State Idle if Movements are locked

            //If there is no MoveDir
                //Go to StateDecelerate if MovementsData.StopDecelerationDuration > 0
                //Go to StateIdle otherwise

            //If MovementsData.TurnBackDecelerationDuration > 0 and the angle between MoveDir and OrientDir > MovementsData.TurnBackAngleThreshold
            //Go to StateTurnBackDecelerate

            //Force OrientDir to MoveDir
        }
    }
}