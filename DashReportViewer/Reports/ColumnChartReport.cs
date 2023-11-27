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
    [ReportName("Column Chart", "7DDBD662-C10D-46BD-A370-387BAB168819", Description = "Simple Column Chart", Folder = "test")]
    public class ColumnChartReport : ReportEntity, IReport
    {
        public ColumnChartReport(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) { }

        protected override async Task<IEnumerable<object>> Main()
        {
            return await Task.Run(() =>
            {
                var widgets = new List<Widget>();



                // header



                var dataPoints = new List<ColumnChartDataHeader>();
                
                
                
                dataPoints.Add(new ColumnChartDataHeader() {
                    Name = "food",
                    ColumnChartDataPoints = new ColumnChartDataPoints()
                    {
                        Label = "2010",
                        Data = new List<double>() { 10, 24 }
                    } 
                });

                dataPoints.Add(new ColumnChartDataHeader()
                {
                    Name = "drink",
                    ColumnChartDataPoints = new ColumnChartDataPoints()
                    {
                        Label = "2012",
                        Data = new List<double>() { 10, 24 }
                    }
                });





                widgets.Add(new Widget("Column Chart Report")
                {
                    Content = new ColumnChartContent()
                    {
                        DataPoints = dataPoints,
                        IsStacked = true,
                        //Title = "Hello Chart",
                        WidgetHeight = "200px"
                    },
                    Column = 12
                });


                return widgets;
            });
        }
    }
}