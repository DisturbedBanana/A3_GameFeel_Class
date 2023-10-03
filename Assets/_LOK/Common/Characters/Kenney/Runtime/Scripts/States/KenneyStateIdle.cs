using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateIdle : AKenneyState
    {
        protected override void OnStateInit()
        {
            //Find Movable Interfaces inside StateMachine
            //You will need to write Speed and check if movements are locked and read MoveDir
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            //Force MoveSpeed to 0
        }

        protected override void OnStateUpdate()
        {
            //Do nothing if movements are locked

            //If there is MoveDir
                //Change to StateAccelerate if MovementsData.StartAccelerationDuration > 0
                //Change to StateWalk otherwise
        }
    }
}