using DashReportViewer.GA.Models;
using Google.Apis.AnalyticsReporting.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DashReportViewer.GA
{
    public interface IGAService
    {
        List<DimensionResult> GetDimensionsAndMetrics(string authenticationJson, string viewId, List<Dimension> dimensions, List<Metric> metrics, string startDate, string endDate);
    }

    public class GAService : IGAService
    {
        public List<DimensionResult> GetDimensionsAndMetrics(string authenticationJson, string viewId, List<Dimension> dimensions, List<Metric> metrics, string startDate, string endDate)
        {
            var browserSessions = new List<DimensionResult>();

            var credential = Google.Apis.Auth.OAuth2.GoogleCredential.FromJson(authenticationJson)
                .CreateScoped(new[] { Google.Apis.AnalyticsReporting.v4.AnalyticsReportingService.Scope.AnalyticsReadonly });

            using (var analytics = new Google.Apis.AnalyticsReporting.v4.AnalyticsReportingService(new Google.Apis.Services.BaseClientService.Initializer
            {
                HttpClientInitializer = credential
            }))
            {
                // Create the DateRange object.
                DateRange dateRange = new DateRange() { StartDate = startDate, EndDate = endDate };

                // Create the ReportRequest object.
                // Create the ReportRequest object.
                ReportRequest reportRequest = new ReportRequest
                {
                    ViewId = viewId,
                    DateRanges = new List<DateRange>() { dateRange },
                    Dimensions = dimensions, // new List<Dimension>() { browser, campaign, age },
                    Metrics = metrics, // new List<Metric>() { sessions, pageviews }
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
                    for (int i = 0; i < row.Dimensions.Count(); i++)
                    {
                        browserSessions.Add(new DimensionResult()
                        {
                            Name = row.Dimensions[i],
                            Value = row.Metrics.FirstOrDefault().Values[i]
                        });
                    }
                }
            }

            return browserSessions;
        }

        
    }
}