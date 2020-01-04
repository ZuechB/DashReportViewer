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

                widgets.Add(GetUsers(firstName));
                widgets.Add(GetUsers(null));

                return widgets;
            });
        }

        private Widget GetUsers(string firstName)
        {
            var labels = new List<string>();
            labels.Add("Year");
            labels.Add("Sales");
            labels.Add("Expenses");
            labels.Add("test");

            var dataPoints = new List<AreaChartDataPoint>();
            dataPoints.Add(new AreaChartDataPoint()
            {
                XAxis = "2013",
                Data = new List<int>() { 1000, 400, 400 }
            });

            dataPoints.Add(new AreaChartDataPoint()
            {
                XAxis = "2014",
                Data = new List<int>() { 1170, 460, 400 }
            });

            dataPoints.Add(new AreaChartDataPoint()
            {
                XAxis = "2015",
                Data = new List<int>() { 1000, 400, 400 }
            });

            dataPoints.Add(new AreaChartDataPoint()
            {
                XAxis = "2016",
                Data = new List<int>() { 1170, 460, 400 }
            });



            return new Widget("Users") { 
                Content = new AreaChartContent()
                {
                    Labels = labels,
                    dataPoints = dataPoints

                }, Column = 6 
            };
        }
    }
}
