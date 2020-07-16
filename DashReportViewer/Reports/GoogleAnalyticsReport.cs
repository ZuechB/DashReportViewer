using CoreBackpack.Time;
using DashReportViewer.GA;
using DashReportViewer.GA.Models;
using DashReportViewer.Models;
using DashReportViewer.Shared.Attributes;
using DashReportViewer.Shared.Models;
using DashReportViewer.Shared.Models.Reporting;
using DashReportViewer.Shared.Models.Widgets;
using DashReportViewer.Shared.ReportContent;
using DashReportViewer.Shared.Services;
using Google.Apis.AnalyticsReporting.v4.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashReportViewer.Reports
{
    [ReportName("Google Analytics Report", "25BCFF06-4233-4BA5-9586-914FF4B0F960", Description = "This is a test")]
    [
        ReportParams("Date", ReportInputType.DateRange, OrderId = 1)
    ]
    public class GoogleAnalyticsReport : ReportEntity, IReport
    {
        readonly IGAService gAService;
        public GoogleAnalyticsReport(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) 
        {
            gAService = GetService<IGAService>();
        }

        protected override async Task<IEnumerable<object>> Main()
        {
            var widgets = new List<Widget>();
            var sessions = new List<DimensionResult>();
            var activeUsers = new List<DimensionResult4Columns>();
            var organicSearches = new List<DimensionResult4Columns>();
            var otherTraffic = new List<DimensionResult4Columns>();
            var productPageViews = new List<DimensionResult5Columns>();
            var productDetailPageViews = new List<DimensionResultFlexible>();
            var date = GetParameterValue<Shared.Models.DateRange>("Date");
            if (date != null)
            {
                var startDate = date.Start.ToString("yyyy-MM-dd");
                var endDate = date.End.ToString("yyyy-MM-dd");

                var json = GetJson();

                sessions = gAService.GetDimensionsAndMetrics(
                    json, 
                    "198345607", 
                    new List<Dimension>() { UserDimension.browser, UserDimension.campaign, UserDimension.age }, 
                    new List<Metric>() { GASession.Session, GAUser.PageViews, GAUser.NewUsers },     
                    startDate, endDate);

                //searchMaterials = gAService.GetDimensionsAndMetrics(
                //    json,
                //    "198345607",
                //    new List<Dimension>() { new Dimension { Name = "ga:pagePath" } },
                //    new List<Metric>() { new Metric { Expression = "ga:pageViews" } },
                //    "2019-01-01",
                //    DateTime.Now.ToString("yyyy-MM-dd"),
                //    "ga:pagePath=@Product/",
                //    new OrderBy { FieldName = "ga:pageViews", SortOrder = "DESCENDING" });

                activeUsers = gAService.GetDimensionsAndMetricsUneven(
                    json,
                    "198345607",
                    new List<Dimension>() { new Dimension { Name = "ga:day" }, new Dimension { Name = "ga:month" }, new Dimension { Name = "ga:year" } },
                    new List<Metric>() { new Metric { Expression = "ga:users" } },
                    startDate, endDate);

                organicSearches = gAService.GetDimensionsAndMetricsUneven(
                    json,
                    "198345607",
                    new List<Dimension>() { new Dimension { Name = "ga:day" }, new Dimension { Name = "ga:month" }, new Dimension { Name = "ga:year" } },
                    new List<Metric>() { new Metric { Expression = "ga:organicSearches" } },
                    startDate, endDate,
                    "ga:userType=@New");

                otherTraffic = gAService.GetDimensionsAndMetricsUneven(
                    json,
                    "198345607",
                    new List<Dimension>() { new Dimension { Name = "ga:day" }, new Dimension { Name = "ga:month" }, new Dimension { Name = "ga:year" }, new Dimension { Name = "ga:fullReferrer" } },
                    new List<Metric>() { new Metric { Expression = "ga:newUsers" } },
                    startDate, endDate,
                    "ga:organicSearches==0");

                productPageViews = gAService.GetDimensionsAndMetrics5Columns(
                    json,
                    "198345607",
                    new List<Dimension>() { new Dimension { Name = "ga:day" }, new Dimension { Name = "ga:month" }, new Dimension { Name = "ga:year" }, new Dimension { Name = "ga:pagePath" } },
                    new List<Metric>() { new Metric { Expression = "ga:pageViews" } },
                    startDate, endDate,
                    "ga:pagePath=@Product/",
                    new OrderBy { FieldName = "ga:pageViews", SortOrder = "DESCENDING" });

                productDetailPageViews = gAService.GetDimensionsAndMetricsFlexible(
                    json,
                    "198345607",
                    new List<Dimension>() { new Dimension { Name = "ga:dimension1" }, new Dimension { Name = "ga:dimension2" }, new Dimension { Name = "ga:pagePath" } },
                    new List<Metric>() { new Metric { Expression = "ga:users" } },
                    startDate, endDate,
                    "ga:pagePath=@Product/;ga:dimension2==5");

            }

            widgets.Add(new Widget("Devices")
            {
                Content = new TableContent()
                {
                    Content = sessions
                },
                Column = 12
            });

            var dates = new List<string>();
            var countsActiveUsers = new List<double>();
            var countsOrganicSearches = new List<double>();
            var countsOtherTraffic = new List<double>();
            var countsProductPageViews = new List<double>();

            foreach (var day in activeUsers)
            {
                day.Date = new DateTime(int.Parse(day.ThirdColumn), int.Parse(day.SecondColumn), int.Parse(day.FirstColumn));
            }

            foreach (var day in organicSearches)
            {
                day.Date = new DateTime(int.Parse(day.ThirdColumn), int.Parse(day.SecondColumn), int.Parse(day.FirstColumn));
            }

            foreach (var day in otherTraffic)
            {
                day.Date = new DateTime(int.Parse(day.ThirdColumn), int.Parse(day.SecondColumn), int.Parse(day.FirstColumn));
            }

            foreach (var day in productPageViews)
            {
                day.Date = new DateTime(int.Parse(day.ThirdColumn), int.Parse(day.SecondColumn), int.Parse(day.FirstColumn));
            }

            if (date != null)
            {
                //if weeks
                dates.Add("Weekly");
                var weeks = DateTimeExtensions.GetWeeks(date.Start, date.End);
                foreach (var week in weeks)
                {
                    dates.Add(week.StartDate.ToShortDateString() + " to " + week.EndDate.AddDays(-1).ToShortDateString());

                    var countActiveUsers = 0;
                    var countOrganicSearches = 0;
                    var countOtherTraffic = 0;

                    countActiveUsers += activeUsers.Where(d => d.Date >= week.StartDate && d.Date <= week.EndDate.AddDays(-1).EndOfDay()).Sum(d => int.Parse(d.Value));
                    countOrganicSearches += organicSearches.Where(d => d.Date >= week.StartDate && d.Date <= week.EndDate.AddDays(-1).EndOfDay()).Sum(d => int.Parse(d.Value));
                    countOtherTraffic += otherTraffic.Where(d => d.Date >= week.StartDate && d.Date <= week.EndDate.AddDays(-1).EndOfDay()).Sum(d => int.Parse(d.Value));

                    //counts.Add(count);

                    var weekLength = week.EndDate - week.StartDate;
                    countsActiveUsers.Add((double)((double)countActiveUsers / (double)weekLength.Days)); // if you want to see average
                    countsOrganicSearches.Add((double)((double)countOrganicSearches / (double)weekLength.Days));
                    countsOtherTraffic.Add((double)((double)countOtherTraffic / (double)weekLength.Days));
                }
            }

            var dataPoints = new List<AreaChartDataPoint>();
            dataPoints.Add(new AreaChartDataPoint()
            {
                Label = "Average daily Active Users",
                Data = countsActiveUsers
            });

            widgets.Add(new Widget("Active Users")
            {
                Content = new AreaChartContent()
                {
                    dataPoints = dataPoints,
                    XAxis = dates
                },
                Column = 12
            });

            var dataPointsTraffic = new List<AreaChartDataPoint>();
            dataPointsTraffic.Add(new AreaChartDataPoint()
            {
                Label = "Organic New Users",
                Data = countsOrganicSearches
            });

            dataPointsTraffic.Add(new AreaChartDataPoint()
            {
                Label = "Referal/Direct New Users",
                Data = countsOtherTraffic
            });

            widgets.Add(new Widget("Average Organic vs Referal/Direct Users")
            {
                Content = new AreaChartContent()
                {
                    dataPoints = dataPointsTraffic,
                    XAxis = dates
                },
                Column = 12
            });


            widgets.Add(new Widget("activeUsers")
            {
                Content = new TableContent()
                {
                    Content = activeUsers
                },
                Column = 12
            });



  

            return widgets;
        }

        private string GetJson()
        {
            var googleJson = new GoogleJson();
            return JsonConvert.SerializeObject(googleJson);
        }
    }

    //public class User
    //{
    //    [ColumnName("First Name")]
    //    public string FirstName { get; set; }
    //    [ColumnName("Last Name")]
    //    public string LastName { get; set; }
    //    [ColumnName("Joined")]
    //    public DateTimeOffset Created { get; set; }
    //}
}