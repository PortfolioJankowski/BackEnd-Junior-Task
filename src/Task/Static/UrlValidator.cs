using System.Text.RegularExpressions;

namespace Task.Services.Static
{
    public static class UrlValidator
    {
        public static bool isValidUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;

            var pattern = RegexPatternProvider.GetUrlPattern();
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(url);
        }
    }
}
