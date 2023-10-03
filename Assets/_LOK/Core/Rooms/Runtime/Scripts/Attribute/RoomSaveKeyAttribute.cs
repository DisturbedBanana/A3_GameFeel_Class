using System;
using IIMEngine.Save;

namespace LOK.Core.Room
{
    public class RoomSaveKeyAttribute : Attribute
    {
        public SaveKeyType KeyType { get; private set; }
        
        public RoomSaveKeyAttribute(SaveKeyType keyType)
        {
            KeyType = keyType;
        }
    }
}