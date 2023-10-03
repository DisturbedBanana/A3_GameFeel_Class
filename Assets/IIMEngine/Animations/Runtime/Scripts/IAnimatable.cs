namespace IIMEngine.Animations
{
    public interface IAnimatable
    {
        void PlayAnimation(string animName);

        void ResetToDefault();
    }
}