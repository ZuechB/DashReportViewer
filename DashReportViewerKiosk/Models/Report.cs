namespace DashReportViewerKiosk.Models
{
    public class ReportsResponse
    {
        public Report[] Reports { get; set; }
    }

    public class Report
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}