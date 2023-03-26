using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Goal.Seedwork.Infra.Crosscutting.Extensions
{
    public static partial class StringExtensions
    {
#if NET7_0_OR_GREATER
        [GeneratedRegex("[^0-9a-zA-Z]+")]
        private static partial Regex AlphaNumericRegex();
#endif

        public static string PadLeft(this string text, int totalWidth, string paddingString)
        {
            var padding = new StringBuilder();

            for (int i = 0; i < totalWidth; i++)
            {
                padding.Append(paddingString);
            }

            padding.Append(text);

            return padding.ToString();
        }

        public static string PadRight(this string text, int totalWidth, string paddingString)
        {
            var padding = new StringBuilder();

            padding.Append(text);

            for (int i = 0; i < totalWidth; i++)
            {
                padding.Append(paddingString);
            }

            return padding.ToString();
        }

        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            string normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

#if NET7_0_OR_GREATER
            return AlphaNumericRegex().Replace(str, "");
#else
            return Regex.Replace(str, "[^0-9a-zA-Z]+", "");
#endif
        }

        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);
    }
}
