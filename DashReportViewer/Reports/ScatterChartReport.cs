using DashReportViewer.Shared.Attributes;
using DashReportViewer.Shared.Models;
using DashReportViewer.Shared.Models.Reporting;
using DashReportViewer.Shared.Models.Widgets;
using DashReportViewer.Shared.ReportContent;
using DashReportViewer.Shared.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashReportViewer.Reports
{
    [ReportName("Scatter Chart", "5F3AFC72-1180-49D3-8426-A5F50A793031", Description = "This is a test")]
    [
        ReportParams("Date", ReportInputType.DateRange, OrderId = 1),
        ReportParams("First Name", ReportInputType.TextBox, OrderId = 2)
    ]
    public class ScatterChartReport : ReportEntity, IReport
    {
        public ScatterChartReport(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) { }

        protected override async Task<IEnumerable<object>> Main()
        {
            var parm = parameters;

            var firstName = GetParameterValue<string>("FirstName");
            var date = GetParameterValue<DateRange>("Date");

            return await Task.Run(() =>
            {
                var widgets = new List<Widget>();

                widgets.Add(GetDownloads("Widget 1"));
                widgets.Add(GetDownloads("Widget 2"));

                return widgets;
            });
        }

        private Widget GetDownloads(string widgetName)
        {
            var dataPoints = new List<ScatterChartDataPoint>();

            dataPoints.Add(new ScatterChartDataPoint(8m, 12m));
            dataPoints.Add(new ScatterChartDataPoint(4m, 5.5m));
            dataPoints.Add(new ScatterChartDataPoint(11m, 14m));
            dataPoints.Add(new ScatterChartDataPoint(4m, 5m));
            dataPoints.Add(new ScatterChartDataPoint(3m, 3.5m));
            dataPoints.Add(new ScatterChartDataPoint(6.5m, 7m));

            return new Widget(widgetName)
            {
                Content = new ScatterChartContent()
                {
                    YName = "Weight",
                    XName = "Age",
                    DataPoints = dataPoints,
                    Title = "This is a sample title"
                },
                Column = 6
            };
        }
    }
}
