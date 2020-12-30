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
    [ReportName("Pie Chart", "6A69C2CA-ED05-4B28-9962-AF02C8716D67", Description = "This is a test", Icon = "fa-briefcase", Folder = "test2")]
    public class PieChartReport : ReportEntity, IReport
    {
        public PieChartReport(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) { }

        protected override async Task<IEnumerable<object>> Main()
        {
            var date = GetParameterValue<DateRange>("Date");

            return await Task.Run(() =>
            {
                var widgets = new List<Widget>();


                var dataPoints = new List<PieChartDataPoint>();

                dataPoints.Add(new PieChartDataPoint()
                {
                    Name = "Code for Work",
                    Number = 100
                });

                dataPoints.Add(new PieChartDataPoint()
                {
                    Name = "Sleep",
                    Number = 20
                });

                dataPoints.Add(new PieChartDataPoint()
                {
                    Name = "Eat",
                    Number = 50
                });

                dataPoints.Add(new PieChartDataPoint()
                {
                    Name = "Code for fun",
                    Number = 80
                });


                widgets.Add(new Widget("Sample Widget")
                {
                    Content = new PieChartContent()
                    {
                        DataPoints = dataPoints,
                        Title = "This is about the widget",
                    },
                    Column = 6
                });


                widgets.Add(new Widget("Sample Widget")
                {
                    Content = new PieChartContent()
                    {
                        DataPoints = dataPoints,
                        Title = "This is about the widget",
                    },
                    Column = 6
                });





                return widgets;

            });
        }
    }
}
