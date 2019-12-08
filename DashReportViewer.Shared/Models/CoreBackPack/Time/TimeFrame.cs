using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashReportViewer.Models.CoreBackPack.Time
{
    public class TimeFrame
    {
        public static DateTime StartOfDay(DateTime theDate)
        {
            return theDate.Date;
        }

        public static DateTime EndOfDay(DateTime theDate)
        {
            return theDate.Date.AddDays(1).AddTicks(-1);
        }
        //public static DateTime StartOfThisMonth(string timeZone)
        //{
        //    var now = SystemTime.Now.ToLocalTime(timeZone);
        //    var startOfMonth = new DateTime(now.Year, now.Month, 1);
        //    return startOfMonth.AddTicks(-1);
        //}

        //public static DateTime EndOfThisMonth(string timeZone)
        //{
        //    DateTime today = SystemTime.Now.ToLocalTime(timeZone).Date;
        //    DateTime endOfMonth = new DateTime(today.Year,
        //                                       today.Month,
        //                                       DateTime.DaysInMonth(today.Year,
        //                                                            today.Month));

        //    return endOfMonth;
        //}
    }
}
