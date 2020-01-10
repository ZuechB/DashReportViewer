using Google.Apis.AnalyticsReporting.v4.Data;

namespace DashReportViewer.GA.Models
{
    public class GAUser
    {
        public static Metric PageViews = new Metric() { Expression = "ga:pageviews", Alias = "pageviews" };

        /// <summary>
        /// The total number of users for the requested time period.
        /// </summary>
        public static Metric Users = new Metric() { Expression = "ga:users", Alias = "users" };

        /// <summary>
        /// The number of sessions marked as a user's first sessions.
        /// </summary>
        public static Metric NewUsers = new Metric() { Expression = "ga:newUsers", Alias = "newusers" };

        /// <summary>
        /// The percentage of sessions by users who had never visited the property before.
        /// </summary>
        public static Metric PercentNewSessions = new Metric() { Expression = "ga:percentNewSessions", Alias = "percentNewSessions" };
    }

    public class UserDimension
    {
        public static Dimension browser = new Dimension() { Name = "ga:browser" };
        public static Dimension campaign = new Dimension() { Name = "ga:campaign" };
        public static Dimension age = new Dimension() { Name = "ga:userAgeBracket" };
    }
}