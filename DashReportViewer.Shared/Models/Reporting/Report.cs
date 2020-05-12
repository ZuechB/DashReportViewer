using System;

namespace DashReportViewer.Shared.Models.Reporting
{
    public class Report
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Type ReportType { get; set; }
        public bool IsFavorite { get; set; }
        public string Folder { get; set; }
        public string Icon { get; set; }
    }
}
