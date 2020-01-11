using DashReportViewer.Shared.Attributes;
using DashReportViewer.Shared.Models;
using DashReportViewer.Shared.Models.Reporting;
using DashReportViewer.Shared.Models.Widgets;
using DashReportViewer.Shared.ReportContent;
using DashReportViewer.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashReportViewer.Reports
{
    [ReportName("Text Report", "1104DA04-938C-4A41-9B27-ED49C614DB97", Description = "This is a test")]
    public class TextReport : ReportEntity, IReport
    {
        public TextReport(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) { }

        protected override async Task<IEnumerable<object>> Main()
        {
            return await Task.Run(() =>
            {
                var widgets = new List<Widget>();


                widgets.Add(new Widget()
                {
                    Content = new TextContent()
                    {
                        Text = "Hello World!",
                        FontSize = "30px",
                        HorizontalAlign = TextHorizontalAlign.Center,
                        VerticalAlign = TextVerticalAlign.Middle,
                        WidgetHeight = "200px"
                    },
                    Column = 3
                });


                return widgets;
            });
        }

    }
}