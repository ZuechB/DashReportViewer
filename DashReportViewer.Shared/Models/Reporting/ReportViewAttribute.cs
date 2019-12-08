using System;

namespace DashReportViewer.Shared.Models.Reporting
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ReportViewAttribute : Attribute
    {
        public ReportType Option { get; set; }

        public ReportViewAttribute(ReportType option)
        {
            Option = option;
        }
    }
}