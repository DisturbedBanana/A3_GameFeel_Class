using LOK.Core.Room;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace LOK.Common.RoomUtils
{
    public class RoomValidateFeedbacks : MonoBehaviour, IRoomValidateHandler
    {
        [SerializeField] private MMF_Player _validateFeedbacks;

        public void OnRoomValidated(Room room)
        {
            PlayValidateFeedbacks();
        }

        public void PlayValidateFeedbacks()
        {
            _validateFeedbacks.PlayFeedbacks(transform.position);
        }
    }
}