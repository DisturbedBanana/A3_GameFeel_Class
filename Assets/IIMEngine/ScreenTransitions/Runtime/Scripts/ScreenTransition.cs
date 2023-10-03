using System.Collections;
using UnityEngine;

namespace IIMEngine.ScreenTransitions
{
    public abstract class ScreenTransition : MonoBehaviour
    {
        [SerializeField] private string _transitionID = "";

        public string TransitionID => _transitionID;

        public bool IsPlaying { get; private set; }

        public void Init()
        {
            OnInit();
        }

        public void Play()
        {
            StartCoroutine(_CoroutinePlay());
        }

        private IEnumerator _CoroutinePlay()
        {
            IsPlaying = true;
            yield return PlayLoop();
            IsPlaying = false;
        }

        protected abstract IEnumerator PlayLoop();

        protected virtual void OnInit() { }
    }
}