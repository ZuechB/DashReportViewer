using System.Collections.Generic;

namespace DashReportViewer.Shared.ReportContent
{
    public class PieChartContent : BaseReportContent
    {
        public string Title { get; set; }
        public List<PieChartDataPoint> DataPoints { get; set; }
    }

    public class PieChartDataPoint
    {
        public string Name { get; set; }
        public int Number { get; set; }
    }
}