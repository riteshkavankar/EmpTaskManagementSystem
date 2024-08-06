namespace EmployeeTaskManagementSystem.Helper
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dateTime, DayOfWeek firstDayOfWeek)
        {
            var diff = dateTime.DayOfWeek - firstDayOfWeek;
            if (diff < 0) diff += 7;
            return dateTime.AddDays(-diff).Date;
        }

        public static DateTime EndOfWeek(this DateTime dateTime, DayOfWeek lastDayOfWeek)
        {
            var diff = lastDayOfWeek - dateTime.DayOfWeek;
            if (diff < 0) diff += 7; 
            return dateTime.AddDays(diff).Date; 
        }
    }

}
