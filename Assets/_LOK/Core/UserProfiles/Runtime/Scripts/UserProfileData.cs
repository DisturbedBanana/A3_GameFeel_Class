using System.Collections.Generic;

namespace LOK.Core.UserProfiles
{
    public class UserProfileData
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string UserName { get; set; } = "";

        private Dictionary<string, string> _passwordsDict = new Dictionary<string, string>();

        public void AddPassword(string validatorID, string password)
        {
            _passwordsDict[validatorID] = password;
        }

        public bool TryGetPassword(string validatorID, out string password)
        {
            return _passwordsDict.TryGetValue(validatorID, out password);
        }

        public string GetPassword(string validatorID)
        {
            return _passwordsDict[validatorID];
        }
    }
}