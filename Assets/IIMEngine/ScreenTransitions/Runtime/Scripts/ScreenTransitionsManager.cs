using System;
using System.Collections;
using UnityEngine;

namespace IIMEngine.ScreenTransitions
{
    public class ScreenTransitionsManager : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        public static ScreenTransitionsManager Instance { get; private set; }
        
        [SerializeField] private bool _autoInit = true;
        
        private ScreenTransition[] _allTransitions;
        
        #pragma warning restore 0414
        #endregion

        private void Awake()
        {
            Instance = this;
            
            if (_autoInit) {
                Init();
            }
        }

        public void Init()
        {
            _allTransitions = _FindAllTransitions();
            _InitAllTransitions();
        }

        public IEnumerator PlayAndWaitTransition(string transitionID)
        {
            //Call PlayTransition and wait until transition is finished
            yield return PlayTransition(transitionID);
        }

        public ScreenTransition PlayTransition(string transitionID)
        {
            //Find Transition using transitionID and return it
            //Play Transition if transition found
            //Return Transition
            foreach (var transition in _allTransitions)
            {
                if (transition.TransitionID == transitionID)
                {
                    transition.Play();
                    return transition;
                }
            }
            return null;
        }

        private ScreenTransition[] _FindAllTransitions()
        {
            //Find All ScreenTransitions
            ScreenTransition[] transitions = GetComponentsInChildren<ScreenTransition>();
            return transitions;
        }

        private void _InitAllTransitions()
        {
            //Call ScreenTransition.Init() method for each found transitions inside _allTransitions
            foreach (var transition in _allTransitions)
            {
                transition.Init();
            }
        }
    }
}