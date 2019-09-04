using System;

namespace ServerChatOmnicasa.Entities
{
    public class ConvertDateTime
    {
        #region Fields

        /// <summary>
        /// Format Date
        /// </summary>
        private const string FormatDate = "yyyy-MM-ddThh:mm:ss";

        #endregion

        #region Methods

        public static string GetDateTimeNowUtc()
        {
            return DateTime.UtcNow.ToString(FormatDate);
        }

        #endregion
    }
}
