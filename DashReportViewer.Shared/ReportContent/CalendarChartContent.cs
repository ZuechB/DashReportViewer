using System;
using System.Collections.Generic;

namespace DashReportViewer.Shared.ReportContent
{
    public class CalendarChartContent : BaseReportContent
    {
        public string Title { get; set; }
        public List<CalendarDataPoint> DataPoints { get; set; }
    }

    public class CalendarDataPoint
    {
        public DateTime Date { get; set; }
        public int Size { get; set; } 
    }
}
