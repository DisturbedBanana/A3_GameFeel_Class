using System;
using LOK.Core.Room;
using UnityEngine;
using UnityEngine.Events;

namespace LOK.Core.Switch
{
    public class SwitchEntity : MonoBehaviour, IRoomInitHandler
    {
        public Action<SwitchEntity> OnSwitchOn { get; set; } = null;
        public Action<SwitchEntity> OnSwitchDisable { get; set; } = null;
        public Action<SwitchEntity> OnSwitchOff { get; set; } = null;

        public enum State
        {
            Undefined = -1,
            On = 0,
            Off,
            Disabled,
        }

        [Header("Start State")]
        [SerializeField] private State _startState = State.Off;

        [Header("Events")]
        [SerializeField] private UnityEvent _switchOnEvents;
        [SerializeField] private UnityEvent _switchOffEvents;

        public State CurrentState { get; private set; } = State.Undefined;

        public State StartState {
            get => _startState;
            set => _startState = value;
        }

        public void SwitchDisable()
        {
            if (CurrentState == State.Disabled) return;
            CurrentState = State.Disabled;
            OnSwitchDisable?.Invoke(this);
        }

        public void SwitchOn()
        {
            if (CurrentState == State.On) return;
            CurrentState = State.On;
            OnSwitchOn?.Invoke(this);
            _switchOnEvents.Invoke();
        }

        public void SwitchOff()
        {
            if (CurrentState == State.Off) return;
            CurrentState = State.Off;
            OnSwitchOff?.Invoke(this);
            _switchOffEvents.Invoke();
        }

        public void OnRoomInit(Room.Room room)
        {
            switch (_startState) {
                case State.On:
                    SwitchOn();
                    break;

                case State.Off:
                    SwitchOff();
                    break;

                case State.Disabled:
                    SwitchDisable();
                    break;
            }
        }
    }
}