using UnityEngine.Localization;

namespace Game
{
    public static class LocalizationExtensions
    {
        public static string[] GetStrings(this LocalizedString[] localizedStrings)
        {
            var text = new string[localizedStrings.Length];

            for (int i = 0; i < localizedStrings.Length; i++) 
                text[i] = localizedStrings[i].GetLocalizedString();
            
            return text;
        }
    }
}