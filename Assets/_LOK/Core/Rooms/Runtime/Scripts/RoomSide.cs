namespace LOK.Core.Room
{
    public enum RoomSide
    {
        Undefined = -1,
        Up = 0,
        Down,
        Left,
        Right
    }

    public static class RoomSideUtils
    {
        public static RoomSide GetOppositeSide(this RoomSide exitSide)
        {
            switch (exitSide) {
                case RoomSide.Down: return RoomSide.Up;
                case RoomSide.Up: return RoomSide.Down;
                case RoomSide.Left: return RoomSide.Right;
                case RoomSide.Right: return RoomSide.Left;
            }

            return RoomSide.Undefined;
        }
    }
}