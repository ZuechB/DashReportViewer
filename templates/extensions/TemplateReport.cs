using DashReportViewer.Shared.Attributes;
using DashReportViewer.Shared.Models;
using DashReportViewer.Shared.Models.Reporting;
using DashReportViewer.Shared.Models.Widgets;
using DashReportViewer.Shared.ReportContent;
using DashReportViewer.Shared.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashReportViewer.Reports
{
    /// <summary>
    /// For more about the widget options: https://github.com/ZuechB/DashReportViewer/wiki/Widgets
    /// </summary>
    [ReportName("Name of your report", "$guid1$", Description = "What is this report about?")]
    //[
        //ReportParams("Date", ReportInputType.DateRange, OrderId = 1),
        //ReportParams("First Name", ReportInputType.TextBox, OrderId = 2)
    //]
    public class TemplateReport : ReportEntity, IReport
    {
        public TemplateReport(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) { }

        protected override async Task<IEnumerable<object>> Main()
        {
            //var firstName = GetParameterValue<string>("FirstName");
            //var date = GetParameterValue<DateRange>("Date");

            return await Task.Run(() =>
            {
                var widgets = new List<Widget>();




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


                widgets.Add(new Widget("Widget Name")
                {
                    Content = new AreaChartContent()
                    {
                        dataPoints = dataPoints,
                        XAxis = new List<string>() { "Year", "2013", "2014", "2015", "2016" }
                    },
                    Column = 12
                });



                return widgets;
            });
        }
    }
}