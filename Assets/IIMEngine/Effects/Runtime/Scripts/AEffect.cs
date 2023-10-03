using System;
using System.Collections;
using UnityEngine;

namespace IIMEngine.Effects
{
    public abstract class AEffect : MonoBehaviour
    {
        [Header("Effect ID")]
        [SerializeField] private string _effectID = "";

        [Header("Delays")]
        [SerializeField] private float _delayBefore = 0f;

        public string EffectID => _effectID;

        public bool IsRunning { get; private set; } = false;

        private Coroutine _effectCoroutine = null;

        private void Awake()
        {
            EffectInit();
        }

        private void OnEnable()
        {
            ResetEffect();
        }

        private void OnDisable()
        {
            StopImmediate();
        }

        public void EffectInit()
        {
            OnEffectInit();
        }

        public void ResetEffect()
        {
            if (_effectCoroutine != null) {
                StopCoroutine(_effectCoroutine);
            }

            OnEffectReset();
            IsRunning = false;
        }

        public void Play()
        {
            if (IsRunning) return;
            ResetEffect();
            IsRunning = true;
            _effectCoroutine = StartCoroutine(_EffectCoroutine());
        }

        public void Stop()
        {
            if (!IsRunning) return;
            IsRunning = false;
        }

        public void StopImmediate()
        {
            if (!IsRunning) return;
            if (_effectCoroutine != null) {
                StopCoroutine(_effectCoroutine);
            }

            OnEffectEnd();
            IsRunning = false;
        }

        private IEnumerator _EffectCoroutine()
        {
            yield return new WaitForSeconds(_delayBefore);
            OnEffectStart();
            yield return OnEffectStartCoroutine();
            while (IsRunning) {
                yield return null;
                OnEffectUpdate();
            }

            yield return OnEffectEndCoroutine();
            OnEffectEnd();
            _effectCoroutine = null;
        }

        protected virtual void OnEffectInit() { }
        protected virtual void OnEffectStart() { }
        protected virtual void OnEffectReset() { }
        protected virtual IEnumerator OnEffectStartCoroutine() { yield break; }
        protected virtual void OnEffectUpdate() { }
        protected virtual void OnEffectEnd() { }
        protected virtual IEnumerator OnEffectEndCoroutine() { yield break; }
    }
}