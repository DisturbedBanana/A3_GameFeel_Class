using System;

namespace LOK.Common.Doors
{
    public static class DoorEvents
    {
        public static Action<DoorEntity> OnDoorOpened { get; set; } = null;
        public static Action<DoorEntity> OnDoorClosed { get; set; } = null;

    }
}