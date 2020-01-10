using Google.Apis.AnalyticsReporting.v4.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashReportViewer.GA.Models
{
    public class GASession
    {
        public static Metric Session = new Metric() { Expression = "ga:sessions", Alias = "sessions" };
    }
}
