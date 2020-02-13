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
    [ReportName("Annotation Chart (In Development)", "927D99A4-71AD-4BEF-A12C-BFE8428D0ACB", Description = "This is a test", Folder = "test")]
    public class AnnotationChartReport : ReportEntity, IReport
    {
        public AnnotationChartReport(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) { }

        protected override async Task<IEnumerable<object>> Main()
        {
            return await Task.Run(() =>
            {
                var widgets = new List<Widget>();


                widgets.Add(new Widget("Annotation Chart")
                {
                    Content = new AnnotationChartContent()
                    {

                    },
                    Column = 6
                });


                return widgets;
            });
        }
    }
}