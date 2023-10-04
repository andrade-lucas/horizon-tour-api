namespace Horizon.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static string? ToFormatedString(this DateTime? dateTime, string format = "yyyy-MM-dd hh:mm:ss")
        {
            return dateTime != null 
                ? ((DateTime)dateTime).ToUniversalTime().ToString(format)
                : null;
        }

        public static string ToFormatedString(this DateTime dateTime, string format = "yyyy-MM-dd hh:mm:ss")
        {
            return dateTime.ToUniversalTime().ToString(format);
        }
    }
}
