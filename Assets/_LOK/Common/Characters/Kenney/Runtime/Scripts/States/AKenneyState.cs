using IIMEngine.Movements2D;
using LOK.Core.Room;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public abstract class AKenneyState
    {
        public KenneyStateMachine StateMachine { get; private set; }

        public KenneyMovementsData MovementsData => StateMachine.MovementsData;
        
        public void ChangeState(AKenneyState state) => StateMachine.ChangeState(state);
        
        private IMove2DDirReader _moveDirReader;

        public void StateInit(KenneyStateMachine stateMachine)
        {
            StateMachine = stateMachine;
            OnStateInit();
            _moveDirReader = StateMachine.GetComponent<IMove2DDirReader>();
        }
        
        public void StateEnter(AKenneyState previousState)
        {
            OnStateEnter(previousState);
            RoomEvents.OnRoomEnter += OnRoomEnter;
        }

        public void StateExit(AKenneyState nextState)
        {
            OnStateExit(nextState);
            RoomEvents.OnRoomEnter -= OnRoomEnter;
        }

        public void StateUpdate()
        {
            OnStateUpdate();
        }
        
        private void OnRoomEnter(Room room, RoomEnterPoint roomEnterPoint)
        {
            if (_moveDirReader.MoveDir == Vector2.zero) {
                ChangeState(StateMachine.StateIdle);
            }
        }

        protected virtual void OnStateInit() { }
        protected virtual void OnStateEnter(AKenneyState previousState) { }
        protected virtual void OnStateExit(AKenneyState nextState) { }
        protected virtual void OnStateUpdate() { }
    }
}