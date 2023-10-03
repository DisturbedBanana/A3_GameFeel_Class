using IIMEngine.Save;

namespace LOK.Core.Room
{
    public static class RoomSaveSystem
    {
        #region Functions Room Keys

        public static bool ReadCurrentRoomBool(string keySuffix, bool defaultValue = false)
        {
            return ReadRoomBool(RoomsManager.Instance.CurrentRoom, keySuffix, defaultValue);
        }

        public static bool ReadRoomBool(Room room, string keySuffix, bool defaultValue = false)
        {
            return ReadRoomBool(room.name, keySuffix, defaultValue);
        }

        public static bool ReadRoomBool(string roomName, string keySuffix, bool defaultValue = false)
        {
            string key = GenerateRoomSaveKey(roomName, keySuffix);
            return SaveSystem.ReadGlobalBool(key, defaultValue);
        }

        public static void WriteCurrentRoomBool(string keySuffix, bool defaultValue = false)
        {
            WriteRoomBool(RoomsManager.Instance.CurrentRoom, keySuffix, defaultValue);
        }

        public static void WriteRoomBool(Room room, string keySuffix, bool value)
        {
            WriteRoomBool(room.name, keySuffix, value);
        }

        public static void WriteRoomBool(string roomName, string keySuffix, bool value)
        {
            string key = GenerateRoomSaveKey(roomName, keySuffix);
            SaveSystem.WriteGlobalBool(key, value);
        }

        public static int ReadCurrentRoomInt(string keySuffix, int defaultValue = 0)
        {
            return ReadRoomInt(RoomsManager.Instance.CurrentRoom.name, keySuffix, defaultValue);
        }

        public static int ReadRoomInt(Room room, string keySuffix, int defaultValue = 0)
        {
            return ReadRoomInt(room.name, keySuffix, defaultValue);
        }

        public static int ReadRoomInt(string roomName, string keySuffix, int defaultValue = 0)
        {
            string key = GenerateRoomSaveKey(roomName, keySuffix);
            return SaveSystem.ReadGlobalInt(key, defaultValue);
        }

        public static void WriteCurrentRoomInt(string keySuffix, int defaultValue = 0)
        {
            WriteRoomInt(RoomsManager.Instance.CurrentRoom, keySuffix, defaultValue);
        }

        public static void WriteRoomInt(Room room, string keySuffix, int value)
        {
            WriteRoomInt(room.name, keySuffix, value);
        }

        public static void WriteRoomInt(string roomName, string keySuffix, int value)
        {
            string key = GenerateRoomSaveKey(roomName, keySuffix);
            SaveSystem.WriteGlobalInt(key, value);
        }

        public static float ReadCurrentRoomFloat(string keySuffix, float defaultValue = 0)
        {
            return ReadRoomFloat(RoomsManager.Instance.CurrentRoom.name, keySuffix, defaultValue);
        }

        public static float ReadRoomFloat(Room room, string keySuffix, float defaultValue = 0)
        {
            return ReadRoomFloat(room.name, keySuffix, defaultValue);
        }

        public static float ReadRoomFloat(string roomName, string keySuffix, float defaultValue = 0)
        {
            string key = GenerateRoomSaveKey(roomName, keySuffix);
            return SaveSystem.ReadGlobalFloat(key, defaultValue);
        }

        public static void WriteCurrentRoomFloat(string keySuffix, float defaultValue = 0)
        {
            WriteRoomFloat(RoomsManager.Instance.CurrentRoom, keySuffix, defaultValue);
        }

        public static void WriteRoomFloat(Room room, string keySuffix, float value)
        {
            WriteRoomFloat(room.name, keySuffix, value);
        }

        public static void WriteRoomFloat(string roomName, string keySuffix, float value)
        {
            string key = GenerateRoomSaveKey(roomName, keySuffix);
            SaveSystem.WriteGlobalFloat(key, value);
        }

        public static string ReadCurrentRoomString(string keySuffix, string defaultValue = "")
        {
            return ReadRoomString(RoomsManager.Instance.CurrentRoom.name, keySuffix, defaultValue);
        }

        public static string ReadRoomString(Room room, string keySuffix, string defaultValue = "")
        {
            return ReadRoomString(room.name, keySuffix, defaultValue);
        }

        public static string ReadRoomString(string roomName, string keySuffix, string defaultValue = "")
        {
            string key = GenerateRoomSaveKey(roomName, keySuffix);
            return SaveSystem.ReadGlobalString(key, defaultValue);
        }

        public static void WriteCurrentRoomString(string keySuffix, string defaultValue = "")
        {
            WriteRoomString(RoomsManager.Instance.CurrentRoom, keySuffix, defaultValue);
        }

        public static void WriteRoomString(Room room, string keySuffix, string value)
        {
            WriteRoomString(room.name, keySuffix, value);
        }

        public static void WriteRoomString(string roomName, string keySuffix, string value)
        {
            string key = GenerateRoomSaveKey(roomName, keySuffix);
            SaveSystem.WriteGlobalString(key, value);
        }

        public static string GenerateRoomSaveKey(string roomName, string keySuffix)
        {
            return roomName + "_" + keySuffix;
        }

        public static void DeleteRoomKey(string roomName, string keySuffix)
        {
            string key = GenerateRoomSaveKey(roomName, keySuffix);
            SaveSystem.DeleteGlobalKey(key);
        }
        
        public static void DeleteRoomKey(Room room, string keySuffix)
        {
            DeleteRoomKey(room.name, keySuffix);
        }

        #endregion

        #region Functions Room Completed

        private const string SAVEKEY_SUFFIX_ROOM_COMPLETED = "Completed";

        public static bool IsRoomCompleted(Room room)
        {
            return ReadRoomBool(room, SAVEKEY_SUFFIX_ROOM_COMPLETED);
        }

        public static void MarkRoomAsCompleted(Room room)
        {
            WriteRoomBool(room, SAVEKEY_SUFFIX_ROOM_COMPLETED, true);
        }

        public static void MarkCurrentRoomAsCompleted()
        {
            MarkRoomAsCompleted(RoomsManager.Instance.CurrentRoom);
        }

        public static void DeleteRoomCompleted(Room room)
        {
            DeleteRoomKey(room, SAVEKEY_SUFFIX_ROOM_COMPLETED);
        }

        #endregion
    }
}