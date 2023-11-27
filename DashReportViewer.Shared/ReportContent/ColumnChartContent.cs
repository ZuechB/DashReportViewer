using System.Collections.Generic;

namespace DashReportViewer.Shared.ReportContent
{
    public class ColumnChartContent : BaseReportContent
    {
        public ColumnChartContent()
        {
            legend = new ColumnChartLegend();
        }


        public string Title { get; set; }
        public List<ColumnChartDataHeader> DataPoints { get; set; }
        public ColumnChartLegend legend { get; set; }
        public bool IsStacked { get; set; }
    }

    public class ColumnChartLegend
    {
        public string position { get; set; } = Position.Top.ToString();
        public int maxLines { get; set; } = 3;
    }

    public class ColumnChartDataHeader
    {
        public string Name { get; set; }

        public ColumnChartDataPoints ColumnChartDataPoints { get; set; }
    }

    public class ColumnChartDataPoints
    {
        public string Label { get; set; }
        public List<double> Data { get; set; }
    }

    public enum Position
    {
        Top = 1,
        Left = 2,
        Bottom = 3,
        Right = 4
    }
}