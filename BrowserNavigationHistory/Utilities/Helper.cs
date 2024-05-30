using System;
namespace BrowserNavigationHistory.Utilities
{
    public static class Helper
    {
        public static int GenerateRandomId()
        {
            Random random = new Random();
            return random.Next(0, int.MaxValue);
        }

        public static DateTime UnixTimeStampToDateTime( double unixTimeStamp )
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds( unixTimeStamp ).ToLocalTime();
            return dateTime;
        }
    }
}

