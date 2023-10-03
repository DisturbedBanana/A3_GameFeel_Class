using System;
using UnityEngine;

namespace IIMEngine.Camera
{
    [Serializable]
    public class CameraProfileTransition
    {
        [SerializeField] private float _duration = 1f;
        [SerializeField] private AnimationCurve _curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        public float Duration => _duration;

        public AnimationCurve Curve => _curve;
    }
}