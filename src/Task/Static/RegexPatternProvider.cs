namespace Task.Services.Static
{
    public static class RegexPatternProvider
    {
        public static string GetUrlPattern()
        {
            return @"^(https?|ftp)://[^\s/$.?#].[^\s]*$";
        }
    }
}
