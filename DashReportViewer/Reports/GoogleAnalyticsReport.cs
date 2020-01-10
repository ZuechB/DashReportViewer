using DashReportViewer.GA;
using DashReportViewer.GA.Models;
using DashReportViewer.Models;
using DashReportViewer.Shared.Attributes;
using DashReportViewer.Shared.Models;
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
    [ReportName("Google Analytics Report", "25BCFF06-4233-4BA5-9586-914FF4B0F960", Description = "This is a test")]
    [
        ReportParams("Date", ReportInputType.DateRange, OrderId = 1)
    ]
    public class GoogleAnalyticsReport : ReportEntity, IReport
    {
        readonly IGAService gAService;
        public GoogleAnalyticsReport(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) 
        {
            gAService = GetService<IGAService>();
        }

        protected override async Task<IEnumerable<object>> Main()
        {
            var widgets = new List<Widget>();
            var sessions = new List<BrowserSession>();
            var date = GetParameterValue<DateRange>("Date");
            if (date != null)
            {
                var startDate = date.Start.ToString("yyyy-MM-dd");
                var endDate = date.End.ToString("yyyy-MM-dd");

                var json = GetJson();
                sessions = gAService.GetDeviceSessions(json, "198345607", startDate, endDate);
                gAService.GetPageViews(json, "198345607", startDate, endDate);
            }

            widgets.Add(new Widget("Devices")
            {
                Content = new TableContent()
                {
                    Content = sessions
                },
                Column = 12
            });

            return widgets;
        }

        private string GetJson()
        {
            var googleJson = new GoogleJson();
            return JsonConvert.SerializeObject(googleJson);
        }
    }

    //public class User
    //{
    //    [ColumnName("First Name")]
    //    public string FirstName { get; set; }
    //    [ColumnName("Last Name")]
    //    public string LastName { get; set; }
    //    [ColumnName("Joined")]
    //    public DateTimeOffset Created { get; set; }
    //}
}