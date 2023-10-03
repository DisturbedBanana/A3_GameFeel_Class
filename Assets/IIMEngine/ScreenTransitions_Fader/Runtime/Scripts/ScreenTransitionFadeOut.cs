using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace IIMEngine.ScreenTransitions.Fader
{
    public class ScreenTransitionFadeOut : ScreenTransition
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [SerializeField] private Image _imageRenderer;
        [SerializeField] private float _fadeOutDuration = 1f;
        [SerializeField] private AnimationCurve _fadeOutCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        #pragma warning restore 0414
        #endregion
        
        protected override IEnumerator PlayLoop()
        {
            //TODO: Implements FadeOut
            //Get Current Alpha from ImageRenderer.
            //Lerp ImageRenderer Current Alpha to 0 using _fadeOutDuration & _fadeOutCurve.
            //Don't forget => you will need a loop (for, while). You are inside a Coroutine.
            yield break;
        }
    }
}