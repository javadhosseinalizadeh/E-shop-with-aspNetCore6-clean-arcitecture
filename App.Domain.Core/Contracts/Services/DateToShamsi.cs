using System.Globalization;


namespace App.Domain.Core.Contracts.Services
{
    public static class ToShamsiDate
    {
        public static string ToShamsi(this DateTimeOffset dateTime)
        {
            var pc = new PersianCalendar();
            var date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            var year = pc.GetYear(date);
            var month = pc.GetMonth(date);
            var day = pc.GetDayOfMonth(date);
            var shamsi = $"{year}/{month}/{day}";
            return shamsi;
        }
        public static DateTime ToShamsi(this DateTime dateTime)
        {
            var x = new PersianCalendar();
            var h = x.ToDateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, 0);
            return h;
        }
    }
}
