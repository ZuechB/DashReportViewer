using System.Collections.Generic;

namespace DashReportViewer.Shared.ReportContent
{
    public class GaugeChartContent : BaseReportContent
    {
        public List<GaugeDataPoint> DataPoints { get; set; }
    }

    public class GaugeDataPoint
    {
        public string Label { get; set; }
        public int value { get; set; }
    }
}
