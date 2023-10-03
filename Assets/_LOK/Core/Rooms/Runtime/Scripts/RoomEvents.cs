using System;

namespace LOK.Core.Room
{
    public static class RoomEvents
    {
        public static Action<Room, RoomSide> OnRoomExitTrigger { get; set; }
        public static Action<Room> OnRoomStart { get; set; }
        public static Action<Room, RoomEnterPoint> OnRoomEnter { get; set; }
        public static Action<Room> OnRoomExit { get; set; }
        public static Action<Room> OnRoomEnd { get; set; }
        public static Action<Room, string, string> OnMapExit { get; set; }
    }
}