namespace Task.Services.DateConverter
{
    public class ZuluDateConverter
    {
        public static DateTime DateOffsetToDateTime(string dateOffset)
        {
            if (dateOffset.Trim().EndsWith("Z"))
            {
                dateOffset = dateOffset.Trim().Replace("+00Z", "");
            }

            if (DateTimeOffset.TryParse(dateOffset.Trim(), out DateTimeOffset parsedDate))
                return parsedDate.DateTime;

            throw new ArgumentException($"Invalid DateTime value: {dateOffset}");
        }
    }
}
