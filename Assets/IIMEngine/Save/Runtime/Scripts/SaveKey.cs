namespace IIMEngine.Save
{
    public class SaveKey
    {
        public SaveKeyType KeyType { get; private set; }
        public string KeyName { get; private set; }

        public SaveKey(SaveKeyType keyType, string keyName)
        {
            KeyType = keyType;
            KeyName = keyName;
        }
    }
}