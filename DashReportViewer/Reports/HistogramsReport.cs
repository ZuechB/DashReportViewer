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
    [ReportName("Histograms Chart", "A64939B8-4BC6-4787-ADB2-357BF1A59822", Description = "This is a test", Folder = "user report test")]
    [ReportParams("Date", ReportInputType.DateRange, OrderId = 1)]
    public class HistogramsReport : ReportEntity, IReport
    {
        public HistogramsReport(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) { }

        protected override async Task<IEnumerable<object>> Main()
        {
            var date = GetParameterValue<DateRange>("Date");

            return await Task.Run(() =>
            {
                var widgets = new List<Widget>();


                var dataPoints = new List<HistogramsDataPoint>();


                dataPoints.Add(new HistogramsDataPoint("Acrocanthosaurus", 12.2m));
                dataPoints.Add(new HistogramsDataPoint("Albertosaurus", 9.1m));
                dataPoints.Add(new HistogramsDataPoint("Allosaurus", 12.2m));
                dataPoints.Add(new HistogramsDataPoint("Apatosaurus", 22.9m));
                dataPoints.Add(new HistogramsDataPoint("Archaeopteryx", 0.9m));
                dataPoints.Add(new HistogramsDataPoint("Argentinosaurus", 36.6m));


                widgets.Add(new Widget("Sample Widget")
                {
                    Content = new HistogramsContent()
                    {
                        NameText = "Dinosaur",
                        NumberText = "Length",
                        DataPoints = dataPoints,
                        Title = "This is about the widget",
                    },
                    Column = 6
                });


                widgets.Add(new Widget("Sample Widget")
                {
                    Content = new HistogramsContent()
                    {
                        NameText = "Dinosaur",
                        NumberText = "Length",
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
