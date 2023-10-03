using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace IIMEngine.Save
{
    public static class SaveKeyUtils
    {
        public static SaveKey[] GetGlobalSaveKeys()
        {
            List<SaveKey> resultList = new List<SaveKey>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                foreach (Type type in assembly.GetTypes()) {
                    foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)) {
                        GlobalSaveKeyAttribute attribute = fieldInfo.GetCustomAttribute<GlobalSaveKeyAttribute>(true);
                        if (attribute != null) {
                            SaveKey saveKey = new SaveKey(attribute.KeyType, fieldInfo.GetValue(null) as string);
                            resultList.Add(saveKey);
                        }
                    }
                }
            }

            return resultList.ToArray();
        }
    }
}