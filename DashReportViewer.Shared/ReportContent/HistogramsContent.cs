using System.Collections.Generic;

namespace DashReportViewer.Shared.ReportContent
{
    public class HistogramsContent : BaseReportContent
    {
        public string Title { get; set; }
        public string NameText { get; set; }
        public string NumberText { get; set; }

        public List<HistogramsDataPoint> DataPoints { get; set; }
    }

    public class HistogramsDataPoint
    {
        public HistogramsDataPoint(string name, decimal number)
        {
            Name = name;
            Number = number;
        }

        public string Name { get; set; }
        public decimal Number { get; set; }
    }
}