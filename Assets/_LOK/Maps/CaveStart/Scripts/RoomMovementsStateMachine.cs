using LOK.Common.Characters.Kenney;
using LOK.Core.Room;
using UnityEngine;

namespace LOK.CaveStart
{
    public class RoomMovementsStateMachine : MonoBehaviour, IRoomInitHandler, IRoomEnableHandler
    {
        [Header("Kenney Movements")]
        [SerializeField] private KenneyMovementsData _movementsData;
        
        private Room _room;

        public void OnRoomInit(Room room)
        {
            _room = room;
        }

        public void OnRoomEnable(Room room)
        {
            _movementsData.SpeedMode = KenneySpeedMode.DynamicSpeed;
        }
    }
}