using System.Collections.Generic;

namespace DashReportViewer.Shared.ReportContent
{
    public class BubbleChartContent : BaseReportContent
    {
        public List<BubbleDataPoint> dataPoints { get; set; }
        public string HorizontalText { get; set; }
        public string VerticalText { get; set; }
    }

    public class BubbleDataPoint
    {
        public string Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; } 
        public int Size { get; set; }
    }
}
