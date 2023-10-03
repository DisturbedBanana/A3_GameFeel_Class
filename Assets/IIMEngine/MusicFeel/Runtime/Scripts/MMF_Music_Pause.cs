using MoreMountains.Feedbacks;
using UnityEngine;

namespace IIMEngine.Music.Feel
{
    [AddComponentMenu("")]
    [FeedbackPath("Music/Music Pause")]
    public class MMF_Music_Pause : MMF_Feedback
    {
        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            //Call PauseMusic from MusicsPlaylistManager
        }
    }
}