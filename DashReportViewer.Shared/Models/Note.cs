using System;

namespace DashReportViewer.Shared.Models
{
    public class Note
    {
        public Guid ReportId { get; set; }
        public int CardId { get; set; }
        public string Message { get; set; }
    }
}
