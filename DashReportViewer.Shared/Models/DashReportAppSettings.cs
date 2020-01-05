
namespace DashReportViewer.Shared.Models
{
    public class DashReportAppSettings
    {
        public string CompanyName { get; set; }
        public DashReportAppSettings_SendGrid SendGrid { get; set; }
        public DashReportAppSettings_ClickUp ClickUp { get; set; }
    }

    public class DashReportAppSettings_SendGrid
    {
        public string APIKey { get; set; }
    }

    public class DashReportAppSettings_ClickUp
    {
        public string APIKey { get; set; }
    }
}