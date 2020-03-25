using Apolloio;
using DashReportViewer.Shared.Attributes;
using DashReportViewer.Shared.Models.Reporting;
using DashReportViewer.Shared.Models.Widgets;
using DashReportViewer.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashReportViewer.Shared.ReportContent;

namespace DashReportViewer.Reports
{
    [ReportName("Apollo.IO Report", "FA22CB24-5CF5-4BF2-863C-D39869519087")]
    public class ApolloIOReport : ReportEntity, IReport
    {
        readonly IApolloService apolloService;
        public ApolloIOReport(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService)
        {
            apolloService = GetService<IApolloService>();
        }

        protected override async Task<IEnumerable<object>> Main()
        {
            var widgets = new List<Widget>();


            var authResponse = await apolloService.Authenticate();


            widgets.Add(new Widget()
            {
                Content = new TextContent()
                {
                    Text = authResponse.is_logged_in,
                    FontSize = "30px",
                    HorizontalAlign = TextHorizontalAlign.Center,
                    VerticalAlign = TextVerticalAlign.Middle,
                    WidgetHeight = "200px"
                },
                Column = 3
            });

            return widgets;
        }
    }
}
