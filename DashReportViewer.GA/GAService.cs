using DashReportViewer.GA.Models;
using Google.Apis.AnalyticsReporting.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DashReportViewer.GA
{
    public interface IGAService
    {
        List<BrowserSession> GetDeviceSessions(string authenticationJson, string viewId, string startDate, string endDate);
        List<BrowserSession> GetPageViews(string authenticationJson, string viewId, string startDate, string endDate);
    }

    public class GAService : IGAService
    {
        public List<BrowserSession> GetDimensionsAndMetrics(string authenticationJson, string viewId, string startDate, string endDate)
        {
            var browserSessions = new List<BrowserSession>();

            var credential = Google.Apis.Auth.OAuth2.GoogleCredential.FromJson(authenticationJson)
                .CreateScoped(new[] { Google.Apis.AnalyticsReporting.v4.AnalyticsReportingService.Scope.AnalyticsReadonly });

            using (var analytics = new Google.Apis.AnalyticsReporting.v4.AnalyticsReportingService(new Google.Apis.Services.BaseClientService.Initializer
            {
                HttpClientInitializer = credential
            }))
            {
                // Create the DateRange object.
                DateRange dateRange = new DateRange() { StartDate = startDate, EndDate = endDate };

                // Create the Metrics object.
                Metric sessions = new Metric() { Expression = "ga:sessions", Alias = "sessions" };
                Metric pageviews = new Metric() { Expression = "ga:pageviews", Alias = "pageviews" };
                Metric newusers = new Metric() { Expression = "ga:newUsers", Alias = "newusers" };

                //Create the Dimensions object.
                Dimension browser = new Dimension() { Name = "ga:browser" };
                Dimension campaign = new Dimension() { Name = "ga:campaign" };
                Dimension age = new Dimension() { Name = "ga:userAgeBracket" };

                // Create the ReportRequest object.
                // Create the ReportRequest object.
                ReportRequest reportRequest = new ReportRequest
                {
                    ViewId = viewId,
                    DateRanges = new List<DateRange>() { dateRange },
                    Dimensions = new List<Dimension>() { browser, campaign, age },
                    Metrics = new List<Metric>() { sessions, pageviews }
                };

                List<ReportRequest> requests = new List<ReportRequest>();
                requests.Add(reportRequest);

                // Create the GetReportsRequest object.
                GetReportsRequest getReport = new GetReportsRequest() { ReportRequests = requests };

                // Call the batchGet method.
                GetReportsResponse response = analytics.Reports.BatchGet(getReport).Execute();

                var rows = response.Reports.FirstOrDefault().Data.Rows;
                foreach (var row in rows)
                {



                    //var device = row.Dimensions.FirstOrDefault();
                    //var count = Convert.ToInt32(row.Metrics.FirstOrDefault().Values.FirstOrDefault());

                    //browserSessions.Add(new BrowserSession()
                    //{
                    //    Device = device,
                    //    Count = count
                    //});
                }
            }

            return browserSessions;
        }


        //public List<BrowserSession> GetDeviceSessions(string authenticationJson, string viewId, string startDate, string endDate)
        //{
        //    var browserSessions = new List<BrowserSession>();

        //    var credential = Google.Apis.Auth.OAuth2.GoogleCredential.FromJson(authenticationJson)
        //        .CreateScoped(new[] { Google.Apis.AnalyticsReporting.v4.AnalyticsReportingService.Scope.AnalyticsReadonly });

        //    using (var analytics = new Google.Apis.AnalyticsReporting.v4.AnalyticsReportingService(new Google.Apis.Services.BaseClientService.Initializer
        //    {
        //        HttpClientInitializer = credential
        //    }))
        //    {
        //        // Create the DateRange object.
        //        DateRange dateRange = new DateRange() { StartDate = startDate, EndDate = endDate };

        //        // Create the Metrics object.
        //        Metric sessions = new Metric { Expression = "ga:sessions", Alias = "Sessions" };

        //        //Create the Dimensions object.
        //        Dimension browser = new Dimension { Name = "ga:browser" };

        //        // Create the ReportRequest object.
        //        // Create the ReportRequest object.
        //        ReportRequest reportRequest = new ReportRequest
        //        {
        //            ViewId = viewId,
        //            DateRanges = new List<DateRange>() { dateRange },
        //            Dimensions = new List<Dimension>() { browser },
        //            Metrics = new List<Metric>() { sessions }
        //        };

        //        List<ReportRequest> requests = new List<ReportRequest>();
        //        requests.Add(reportRequest);

        //        // Create the GetReportsRequest object.
        //        GetReportsRequest getReport = new GetReportsRequest() { ReportRequests = requests };

        //        // Call the batchGet method.
        //        GetReportsResponse response = analytics.Reports.BatchGet(getReport).Execute();

        //        var rows = response.Reports.FirstOrDefault().Data.Rows;
        //        foreach (var row in rows)
        //        {
        //            var device = row.Dimensions.FirstOrDefault();
        //            var count = Convert.ToInt32(row.Metrics.FirstOrDefault().Values.FirstOrDefault());

        //            browserSessions.Add(new BrowserSession()
        //            {
        //                Device = device,
        //                Count = count
        //            });
        //        }
        //    }

        //    return browserSessions;
        //}
    }
}
