using System;
using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace LOK.Common.Chest
{
    public class ChestEntity : MonoBehaviour
    {
        public Action<ChestEntity> OnOpenStart { get; set; } = null;
        public Action<ChestEntity> OnOpenEnd { get; set; } = null;
        public Action<ChestEntity> OnClosed { get; set; } = null;

        public enum State
        {
            Opened,
            Closed,
        }

        private State _currentState = State.Closed;

        public bool IsOpened => _currentState == State.Opened;

        public bool IsClosed => _currentState == State.Closed;

        [Header("Open")]
        [SerializeField] private MMF_Player _openFeedbacks;

        public void Open(bool withFeedbacks = true)
        {
            if (IsOpened) return;
            _currentState = State.Opened;
            StartCoroutine(_CoroutineOpen(withFeedbacks));
        }

        IEnumerator _CoroutineOpen(bool withFeedbacks)
        {
            OnOpenStart?.Invoke(this);
            if (withFeedbacks && _openFeedbacks != null) {
                yield return _openFeedbacks.PlayFeedbacksCoroutine(transform.position);
            }
            OnOpenEnd?.Invoke(this);
        }

        public void Close()
        {
            if (IsClosed) return;
            _currentState = State.Closed;
            OnClosed?.Invoke(this);
        }
    }
}