namespace LOK.Core.Room
{
    public interface IRoomEnterHandler
    {
        void OnRoomEnter(Room room, RoomEnterPoint enterPoint = null);
    }
}