namespace Toybot.HelperClasses
{
    public static class TimespanFormat
    {
        public static string[] AllFormats => new string[]
        {
            @"d\dh\hmm\mss\s", @"hh\hmm\m",
            @"h\hmm\mss\s", @"mm\mss\s",
            @"ss\s", @"mm\m", @"d\d",
            @"h\h", @"d\dh\h"
        };
    }
}