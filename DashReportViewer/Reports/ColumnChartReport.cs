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
    [ReportName("Column Chart", "35E385C0-BC0E-4A60-AAA0-6A42406CA2E9", Description = "This is a test", Folder = "test")]
    public class ColumnChartReport : ReportEntity, IReport
    {
        public ColumnChartReport(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) { }

        protected override async Task<IEnumerable<object>> Main()
        {
            return await Task.Run(() =>
            {
                var widgets = new List<Widget>();


                widgets.Add(new Widget("Column Chart Report")
                {
                    Content = new ColumnChartContent()
                    {

                    },
                    Column = 12
                });


                return widgets;
            });
        }
    }
}