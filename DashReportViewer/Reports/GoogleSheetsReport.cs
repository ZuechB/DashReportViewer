using DashReportViewer.GoogleSheets;
using DashReportViewer.Models;
using DashReportViewer.Shared.Attributes;
using DashReportViewer.Shared.Models.Reporting;
using DashReportViewer.Shared.Models.Widgets;
using DashReportViewer.Shared.ReportContent;
using DashReportViewer.Shared.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashReportViewer.Reports
{
    [ReportName("Google Sheets Report", "DEE1CADD-91A7-4F18-9913-98BC62680953", Description = "This is how you connect to google sheets")]
    public class GoogleSheetsReport : ReportEntity, IReport
    {
        readonly IGSheetsService gAService;
        public GoogleSheetsReport(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService)
        {
            gAService = GetService<IGSheetsService>();
        }

        protected override async Task<IEnumerable<object>> Main()
        {
            var widgets = new List<Widget>();

            // fill in the following from google console
            var auth = new
            {
                type = "",
                project_id = "",
                private_key_id = "",
                private_key = "",
                client_email = "",
                client_id = "",
                auth_uri = "https://accounts.google.com/o/oauth2/auth",
                token_uri = "https://oauth2.googleapis.com/token",
                auth_provider_x509_cert_url = "https://www.googleapis.com/oauth2/v1/certs",
                client_x509_cert_url = ""
            };


            var response  = await gAService.ReadSheet<GSheet>(auth, "DashReport",
                "1OQMUWSyo7zifekmv-sW3gZOSubkNZtXLqFIpP6agMto",
                "A1:E");

            widgets.Add(new Widget("My Sheet")
            {
                Content = new TableContent()
                {
                    Content = response
                },
                Column = 12
            });

            return widgets;
        }
    }

    public class GSheet
    {
        public string Brand { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
    }
}
