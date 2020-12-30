using DashReportViewer.Shared.Attributes;
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
    [ReportName("Gauge Chart", "63733EA8-A62F-4EBC-9205-000CC6E1964E", Description = "This is a test", Folder = "test")]
    public class GaugeChartReport : ReportEntity, IReport
    {
        public GaugeChartReport(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) { }

        protected override async Task<IEnumerable<object>> Main()
        {
            return await Task.Run(() =>
            {
                var widgets = new List<Widget>();


                var dataPoints = new List<GaugeDataPoint>();

                dataPoints.Add(new GaugeDataPoint()
                {
                    Label = "Memory",
                    value = 85
                });

                dataPoints.Add(new GaugeDataPoint()
                {
                    Label = "CPU",
                    value = 95
                });



                widgets.Add(new Widget("Sample Gauge Widget")
                {
                    Content = new GaugeChartContent()
                    {
                        DataPoints = dataPoints,
                    },
                    Column = 12
                });


                return widgets;

            });
        }
    }
}
