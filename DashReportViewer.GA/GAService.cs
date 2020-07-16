using DashReportViewer.GA.Models;
using Google.Apis.AnalyticsReporting.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DashReportViewer.GA
{
    public interface IGAService
    {
        List<DimensionResult> GetDimensionsAndMetrics(string authenticationJson, string viewId, List<Dimension> dimensions, List<Metric> metrics, string startDate, string endDate, string filter = "", OrderBy orderBy = null);

        List<DimensionResultFlexible> GetDimensionsAndMetricsFlexible(string authenticationJson, string viewId, List<Dimension> dimensions, List<Metric> metrics, string startDate, string endDate, string filter = "", OrderBy orderBy = null);
        // If dimension length != metric length:
        List<DimensionResult4Columns> GetDimensionsAndMetricsUneven(string authenticationJson, string viewId, List<Dimension> dimensions, List<Metric> metrics, string startDate, string endDate, string filter = "", OrderBy orderBy = null);
        List<DimensionResult5Columns> GetDimensionsAndMetrics5Columns(string authenticationJson, string viewId, List<Dimension> dimensions, List<Metric> metrics, string startDate, string endDate, string filter = "", OrderBy orderBy = null);
    }



    public class GAService : IGAService
    {
        public List<DimensionResult> GetDimensionsAndMetrics(string authenticationJson, string viewId, List<Dimension> dimensions, List<Metric> metrics, string startDate, string endDate, string filter = "", OrderBy orderBy = null)
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
                    FiltersExpression = filter,
                    PageSize = 10000,
                    OrderBys = new List<OrderBy>() { orderBy },
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

        public List<DimensionResult4Columns> GetDimensionsAndMetricsUneven(string authenticationJson, string viewId, List<Dimension> dimensions, List<Metric> metrics, string startDate, string endDate, string filter = "", OrderBy orderBy = null)
        {
            var browserSessions = new List<DimensionResult4Columns>();

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
                    FiltersExpression = filter,
                    PageSize = 10000,
                    OrderBys = new List<OrderBy>() { orderBy },
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
                    if ((row.Dimensions.Count() == 3 || row.Dimensions.Count() == 4) && row.Metrics.Count() == 1)
                    {
                        browserSessions.Add(new DimensionResult4Columns()
                        {
                            FirstColumn = row.Dimensions[0],
                            SecondColumn = row.Dimensions[1],
                            ThirdColumn = row.Dimensions[2],
                            Value = row.Metrics.FirstOrDefault().Values[0]
                        });
                    }
                }
            }

            return browserSessions;
        }

        public List<DimensionResultFlexible> GetDimensionsAndMetricsFlexible(string authenticationJson, string viewId, List<Dimension> dimensions, List<Metric> metrics, string startDate, string endDate, string filter = "", OrderBy orderBy = null)
        {
            var browserSessions = new List<DimensionResultFlexible>();

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
                ReportRequest reportRequest = new ReportRequest
                {
                    ViewId = viewId,
                    DateRanges = new List<DateRange>() { dateRange },
                    Dimensions = dimensions, // new List<Dimension>() { browser, campaign, age },
                    FiltersExpression = filter,
                    PageSize = 10000,
                    OrderBys = new List<OrderBy>() { orderBy },
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
                    browserSessions.Add(new DimensionResultFlexible()
                    {
                        FirstColumn = row.Dimensions[0],
                        SecondColumn = row.Dimensions.Count() > 1 ? row.Dimensions[1] : null,
                        ThirdColumn = row.Dimensions.Count() > 2 ? row.Dimensions[2] : null,
                        FourthColumn = row.Dimensions.Count() > 3 ? row.Dimensions[3] : null,
                        FifthColumn = row.Dimensions.Count() > 4 ? row.Dimensions[4] : null,
                        ValueOne = row.Metrics.FirstOrDefault().Values[0],
                        ValueTwo = row.Metrics.Count() > 1 ? row.Metrics.FirstOrDefault().Values[1] : null,
                        ValueThree = row.Metrics.Count() > 2 ? row.Metrics.FirstOrDefault().Values[2] : null,
                        ValueFour = row.Metrics.Count() > 3 ? row.Metrics.FirstOrDefault().Values[3] : null,
                        ValueFive = row.Metrics.Count() > 3 ? row.Metrics.FirstOrDefault().Values[4] : null
                    });
          
                }
            }

            return browserSessions;
        }

    public List<DimensionResult5Columns> GetDimensionsAndMetrics5Columns(string authenticationJson, string viewId, List<Dimension> dimensions, List<Metric> metrics, string startDate, string endDate, string filter = "", OrderBy orderBy = null)
        {
            var browserSessions = new List<DimensionResult5Columns>();

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
                ReportRequest reportRequest = new ReportRequest
                {
                    ViewId = viewId,
                    DateRanges = new List<DateRange>() { dateRange },
                    Dimensions = dimensions, // new List<Dimension>() { browser, campaign, age },
                    FiltersExpression = filter,
                    PageSize = 10000,
                    OrderBys = new List<OrderBy>() { orderBy },
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
                    if ((row.Dimensions.Count() == 4) && row.Metrics.Count() == 1)
                    {
                        browserSessions.Add(new DimensionResult5Columns()
                        {
                            FirstColumn = row.Dimensions[0],
                            SecondColumn = row.Dimensions[1],
                            ThirdColumn = row.Dimensions[2],
                            FourthColumn = row.Dimensions[3],
                            Value = row.Metrics.FirstOrDefault().Values[0]
                        });
                    }
                }
            }

            return browserSessions;
        }

    }
}