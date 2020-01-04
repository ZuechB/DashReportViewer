using System.Collections.Generic;

namespace DashReportViewer.Shared.ReportContent
{
    public class AreaChartContent : BaseReportContent
    {
        public List<AreaChartDataPoint> dataPoints { get; set; }
        public List<string> XAxis { get; set; }
    }

    public class AreaChartDataPoint
    {
        public string Label { get; set; }
        public List<int> Data { get; set; }
    }
}