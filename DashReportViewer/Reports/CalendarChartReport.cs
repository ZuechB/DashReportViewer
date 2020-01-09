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
    [ReportName("Calendar Chart", "E4D6ACBC-09F6-4B98-9322-A5F9057CFE20", Description = "This is a test", Folder = "test")]
    [ReportParams("Date", ReportInputType.DateRange, OrderId = 1)]
    public class CalendarChartReport : ReportEntity, IReport
    {
        public CalendarChartReport(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) { }

        protected override async Task<IEnumerable<object>> Main()
        {
            var date = GetParameterValue<DateRange>("Date");

            return await Task.Run(() =>
            {
                var widgets = new List<Widget>();


                var dataPoints = new List<CalendarDataPoint>();

                dataPoints.Add(new CalendarDataPoint()
                {
                    Date = new DateTime(2019, 1, 24),
                    Size = 1500
                });

                dataPoints.Add(new CalendarDataPoint()
                {
                    Date = new DateTime(2019, 8, 8),
                    Size = 2400
                });

                dataPoints.Add(new CalendarDataPoint()
                {
                    Date = new DateTime(2019, 7, 12),
                    Size = 3500
                });

                dataPoints.Add(new CalendarDataPoint()
                {
                    Date = new DateTime(2019, 7, 10),
                    Size = 20
                });



                widgets.Add(new Widget("Sample Widget")
                {
                    Content = new CalendarChartContent()
                    {
                        DataPoints = dataPoints,
                        Title = "This is about the widget",
                    },
                    Column = 12
                });





                return widgets;

            });
        }
    }
}
