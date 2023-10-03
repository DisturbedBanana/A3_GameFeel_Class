using UnityEngine;

namespace IIMEngine.Animations
{
    public class Animatable : MonoBehaviour, IAnimatable
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _defaultAnimState = "";

        public void PlayAnimation(string animStateName)
        {
            _animator.Play(animStateName);
        }

        public void ResetToDefault()
        {
            _animator.Play(_defaultAnimState);
        }
    }
}


