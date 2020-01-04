using System.Collections.Generic;

namespace DashReportViewer.Shared.ReportContent
{
    public class AreaChartContent : BaseReportContent
    {
        public IEnumerable<string> Labels { get; set; }
        public List<AreaChartDataPoint> dataPoints { get; set; }
    }

    public class AreaChartDataPoint
    {
        public string XAxis { get; set; }
        public List<int> Data { get; set; }
    }

    public class BaseReportContent
    {

    }
}