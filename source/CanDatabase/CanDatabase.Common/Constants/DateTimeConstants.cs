using System;

namespace CanDatabase.Common.Constants
{
    public static class DateTimeConstants
    {
        #region Points in Time
        public static DateTime UnixEpochStartTime => new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        #endregion

        #region Timespans
        public const int OneYearInSeconds = 60 * 60 * 24 * 365;
        #endregion
    }
}
