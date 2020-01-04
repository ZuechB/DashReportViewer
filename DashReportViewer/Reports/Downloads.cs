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
    [ReportName("Downloads", "E79AA1F9-76B3-4902-891E-10104F6BD54B", Description = "This is a test")]
    [
        ReportParams("Date", ReportInputType.DateRange, OrderId = 1),
        ReportParams("First Name", ReportInputType.TextBox, OrderId = 2)
    ]
    public class Downloads : ReportEntity, IReport
    {
        public Downloads(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) { }

        protected override async Task<IEnumerable<object>> Main()
        {
            var parm = parameters;

            var firstName = GetParameterValue<string>("FirstName");
            var date = GetParameterValue<DateRange>("Date");

            return await Task.Run(() =>
            {
                var widgets = new List<Widget>();

                widgets.Add(GetDownloads("My local downloads"));
                widgets.Add(GetDownloads("My global downloads"));

                return widgets;
            });
        }

        private Widget GetDownloads(string widgetName)
        {
            var dataPoints = new List<AreaChartDataPoint>();

            dataPoints.Add(new AreaChartDataPoint()
            {
                Label = "Sales",
                Data = new List<int>() { 1000, 400, 400, 200 }
            });

            dataPoints.Add(new AreaChartDataPoint()
            {
                Label = "Expenses",
                Data = new List<int>() { 1170, 460, 1000, 3000 }
            });


            //dataPoints.Add(new AreaChartDataPoint()
            //{
            //    Label = "test",
            //    Data = new List<int>() { 1000, 460, 400, 80 }
            //});

            //dataPoints.Add(new AreaChartDataPoint()
            //{
            //    Label = "test2",
            //    Data = new List<int>() { 1000, 460, 400, 80 }
            //});



            return new Widget(widgetName)
            {
                Content = new AreaChartContent()
                {
                    dataPoints = dataPoints,
                    XAxis = new List<string>() { "Year", "2013", "2014", "2015", "2016"}
                },
                Column = 6
            };
        }
    }
}
