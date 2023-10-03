using UnityEngine;

namespace IIMEngine.Save
{
    public static class SaveSystem
    {
        #region Functions Global Keys

        public static bool ReadGlobalBool(string key, bool defaultValue = false)
        {
            int intDefaultValue = defaultValue ? 1 : 0;
            return PlayerPrefs.GetInt(key, intDefaultValue) == 1;
        }

        public static void WriteGlobalBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
            SaveEvents.OnKeyChanged?.Invoke(key);
        }

        public static int ReadGlobalInt(string id, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(id, defaultValue);
        }

        public static void WriteGlobalInt(string id, int value)
        {
            PlayerPrefs.SetInt(id, value);
            SaveEvents.OnKeyChanged?.Invoke(id);
        }

        public static float ReadGlobalFloat(string id, float defaultValue = 0f)
        {
            return PlayerPrefs.GetFloat(id, defaultValue);
        }

        public static void WriteGlobalFloat(string id, float value)
        {
            PlayerPrefs.SetFloat(id, value);
            SaveEvents.OnKeyChanged?.Invoke(id);
        }

        public static string ReadGlobalString(string id, string defaultValue = "")
        {
            return PlayerPrefs.GetString(id, defaultValue);
        }

        public static void WriteGlobalString(string id, string value)
        {
            PlayerPrefs.SetString(id, value);
            SaveEvents.OnKeyChanged?.Invoke(id);
        }

        public static void DeleteGlobalKey(string id)
        {
            PlayerPrefs.DeleteKey(id);
            SaveEvents.OnKeyChanged?.Invoke(id);
        }

        #endregion

        public static void ForceSave()
        {
            PlayerPrefs.Save();
        }

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}