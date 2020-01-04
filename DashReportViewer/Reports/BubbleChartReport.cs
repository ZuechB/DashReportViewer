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
    [ReportName("Bubble Chart (Under Development)", "086A8665-C70D-4495-8469-4D462A505AC5", Description = "This is a test")]
    [ReportParams("Date", ReportInputType.DateRange, OrderId = 1)]
    public class BubbleChartReport : ReportEntity, IReport
    {
        public BubbleChartReport(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) { }

        protected override async Task<IEnumerable<object>> Main()
        {
            var date = GetParameterValue<DateRange>("Date");

            return await Task.Run(() =>
            {
                var widgets = new List<Widget>();



                widgets.Add(new Widget("Sample Widget")
                {
                    Content = new BubbleChartContent()
                    {
                        HorizontalText = "Horizontal Here",
                        VerticalText = "Vertical Here",
                        dataPoints = null,
                        //XAxis = new List<string>() { "Year", "2013", "2014", "2015", "2016" }
                    },
                    Column = 12
                });





                return widgets;

            });
        }
    }
}
