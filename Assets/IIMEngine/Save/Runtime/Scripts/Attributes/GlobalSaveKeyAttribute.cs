using System;

namespace IIMEngine.Save
{
    public class GlobalSaveKeyAttribute : Attribute
    {
        public SaveKeyType KeyType { get; private set; }

        public GlobalSaveKeyAttribute(SaveKeyType keyType)
        {
            KeyType = keyType;
        }
    }
}