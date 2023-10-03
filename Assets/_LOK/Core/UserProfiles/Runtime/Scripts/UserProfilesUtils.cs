using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace LOK.Core.UserProfiles
{
    public static class UserProfilesUtils
    {
        public static string SimplifyUserName(string userName)
        {
            userName = userName.ToLower();
            userName = _RemoveSpaces(userName);
            userName = _RemoveSpecialCharacters(userName);
            userName = _RemoveDiacritics(userName);
            return userName;
        }

        private static string _RemoveSpaces(string userName)
        {
            return userName.Replace(" ", "").Trim();
        }
        
        private static string _RemoveSpecialCharacters(string userName)
        {
            return userName.Replace("-", "");
        }

        private static string _RemoveDiacritics(string userName)
        {
            var normalizedString = userName.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++) {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark) {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }
    }
}