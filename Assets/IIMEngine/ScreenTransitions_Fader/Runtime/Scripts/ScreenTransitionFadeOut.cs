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
            var currentAlpha = _imageRenderer.color.a;
            var t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / _fadeOutDuration;
                var a = Mathf.Lerp(currentAlpha, 0f, _fadeOutCurve.Evaluate(t));
                _imageRenderer.color = new Color(0f, 0f, 0f, a);
                yield return null;
            }
            yield break;
        }
    }
}