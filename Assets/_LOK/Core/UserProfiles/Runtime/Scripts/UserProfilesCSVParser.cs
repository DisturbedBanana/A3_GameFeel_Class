using System.Collections.Generic;
using UnityEngine;

namespace LOK.Core.UserProfiles
{
    public static class UserProfilesCSVParser
    {
        const string LINE_SEPARATOR1 = "\r\n";
        const string LINE_SEPARATOR2 = "\n";
        const char COLUMN_SEPARATOR = ',';
        const int LINE_INDEX_START = 1;

        public static UserProfileData[] Parse(TextAsset textAsset)
        {
            List<UserProfileData> passwordDatas = new List<UserProfileData>();
            string[] lines = textAsset.text.Split(LINE_SEPARATOR1);
            if (lines.Length == 1) {
                //Bad separator, use the other
                lines = textAsset.text.Split(LINE_SEPARATOR2);
            }

            string[] validatorIDs = lines[0].Split(COLUMN_SEPARATOR);

            for (int i = LINE_INDEX_START; i < lines.Length; ++i) {
                string line = lines[i];
                string[] columns = line.Split(COLUMN_SEPARATOR);
                UserProfileData userProfileData = new UserProfileData();
                string lastName = columns[0];
                string firstName = columns[1];
                string username = $"{firstName}{lastName}";
                userProfileData.FirstName = firstName;
                userProfileData.LastName = lastName;
                userProfileData.UserName = UserProfilesUtils.SimplifyUserName(username);
                for (int j = 2; j < columns.Length; ++j) {
                    userProfileData.AddPassword(validatorIDs[j], columns[j]);
                }

                passwordDatas.Add(userProfileData);
            }

            return passwordDatas.ToArray();
        }
    }
}