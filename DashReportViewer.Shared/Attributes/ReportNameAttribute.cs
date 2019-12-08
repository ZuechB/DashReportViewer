using System;

namespace DashReportViewer.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ReportNameAttribute : System.Attribute
    {
        public string ReportName { get; set; }
        public string Description { get; set; }
        public string Folder { get; set; }
        public string ReportId { get; set; }

        public ReportNameAttribute(string reportName, string reportId, string description = "", string folder = "")
        {
            ReportName = reportName;
            ReportId = reportId;
            Description = description;
            Folder = folder;
        }
    }
}