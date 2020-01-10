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
    [ReportName("Bubble Chart", "086A8665-C70D-4495-8469-4D462A505AC5", Description = "This is a test", Folder = "test")]
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


                var dataPoints = new List<BubbleDataPoint>();

                dataPoints.Add(new BubbleDataPoint()
                {
                    Id = "USA",
                    Name = "North America",
                    X = 80.66m,
                    Y = 1.67m,
                    Size = 307007000.255m
                });

                dataPoints.Add(new BubbleDataPoint()
                {
                    Id = "Canada",
                    Name = "North America",
                    X = 60.66m,
                    Y = 2.67m,
                    Size = 307007000.255m
                });

                dataPoints.Add(new BubbleDataPoint()
                {
                    Id = "Italy",
                    Name = "Europe",
                    X = 20.66m,
                    Y = 5.67m,
                    Size = 307007200.255m
                });





                widgets.Add(new Widget("Sample Widget")
                {
                    Content = new BubbleChartContent()
                    {
                        Header = new BubbleHeader()
                        {
                            X = "Life Expectancy",
                            Y = "Fertility Rate",
                            Name = "Region",
                            Size = "Population"
                        },
                        Title = "This is about the widget",
                        HorizontalText = "Horizontal Here",
                        VerticalText = "Vertical Here",
                        dataPoints = dataPoints,
                    },
                    Column = 6
                });


                widgets.Add(new Widget("Sample Widget")
                {
                    Content = new BubbleChartContent()
                    {
                        Header = new BubbleHeader()
                        {
                            X = "Life Expectancy",
                            Y = "Fertility Rate",
                            Name = "Region",
                            Size = "Population"
                        },
                        Title = "This is about the widget",
                        HorizontalText = "Horizontal Here",
                        VerticalText = "Vertical Here",
                        dataPoints = dataPoints,
                    },
                    Column = 6
                });





                return widgets;

            });
        }
    }
}
