using System;

namespace DashReportViewer.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ReportNameAttribute : System.Attribute
    {
        public string ReportName { get; set; }
        public string Description { get; set; }
        public string ReportId { get; set; }
        public Type ReportType { get; set; }

        public ReportNameAttribute(string reportName, string reportId, Type reportType, string description = "")
        {
            ReportName = reportName;
            ReportId = reportId;
            ReportType = reportType;
            Description = description;
        }
    }
}
