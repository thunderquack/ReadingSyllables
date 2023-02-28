namespace ReadingSyllables.SyllablesGenerator
{
    internal static class RepeatingRule
    {
        internal static TimeSpan GetTimeSpanOfRepeat(int repeatingNumber)
        {
            switch (repeatingNumber)
            {
                case 0:
                    return TimeSpan.Zero;
                case 1:
                    return TimeSpan.FromSeconds(5);
                case 2:
                    return TimeSpan.FromSeconds(25);
                case 3:
                    return TimeSpan.FromMinutes(2);
                case 4:
                    return TimeSpan.FromMinutes(10);
                case 5:
                    return TimeSpan.FromHours(1);
                case 6:
                    return TimeSpan.FromHours(5);
                case 7:
                    return TimeSpan.FromDays(1);
                case 8:
                    return TimeSpan.FromDays(5);
                case 9:
                    return TimeSpan.FromDays(25);
                case 10:
                    return TimeSpan.FromDays(120);
                case 11:
                default:
                    return TimeSpan.FromDays(2 * 365);
            }
        }
        internal static DateTime GetNextRepeat(int repeatingNumber)
        {
            return DateTime.UtcNow + GetTimeSpanOfRepeat(repeatingNumber);
        }
    }
}
