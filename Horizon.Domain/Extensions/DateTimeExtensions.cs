namespace Horizon.Domain.Extensions
{
    public static class DateTimeNullable
    {
        public static string? ToFormatedString(this DateTime? dateTime, string format = "yyyy-MM-dd hh:mm:ss")
        {
            return dateTime == null ? null : ((DateTime)dateTime).ToString(format);
        }
    }
}
