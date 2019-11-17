using System;

namespace DashReportViewer.Models.Reporting
{
    public class Report
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Type ReportType { get; set; }
        public bool IsFavorite { get; set; }
    }
}
