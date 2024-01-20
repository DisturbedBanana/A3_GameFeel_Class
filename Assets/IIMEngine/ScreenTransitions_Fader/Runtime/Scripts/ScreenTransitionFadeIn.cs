using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace IIMEngine.ScreenTransitions.Fader
{
    public class ScreenTransitionFadeIn : ScreenTransition
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [SerializeField] private Image _imageRenderer;
        [SerializeField] private float _fadeInDuration = 1f;
        [SerializeField] private AnimationCurve _fadeInCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        #pragma warning restore 0414
        #endregion
        
        protected override IEnumerator PlayLoop()
        {
            //TODO: Implements FadeIn
            //Get Current Alpha from ImageRenderer.
            //Lerp ImageRenderer Current Alpha to 1 using _fadeInDuration & _fadeInCurve.
            //Don't forget => you will need a loop (for, while). You are inside a Coroutine.
            var currentAlpha = _imageRenderer.color.a;
            var t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / _fadeInDuration;
                var a = Mathf.Lerp(currentAlpha, 1f, _fadeInCurve.Evaluate(t));
                _imageRenderer.color = new Color(0f, 0f, 0f, a);
                yield return null;
            }
            yield break;
        }
    }
}