
namespace DashReportViewer.Shared.Models
{
    public class AppSettings
    {
        public string CompanyName { get; set; }
        public SendGrid SendGrid { get; set; }
        public ClickUp ClickUp { get; set; }
    }


    public class SendGrid
    {
        public string APIKey { get; set; }
    }

    public class ClickUp
    {
        public string APIKey { get; set; }
    }
}