using DashReportViewer.Attributes;
using DashReportViewer.Models;
using DashReportViewer.Models.Reporting;
using DashReportViewer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashReportViewer.Reports
{
    [ReportName("Daily Active Users", "E7135E62-0DCA-4133-8273-5606182F9EDD", typeof(DailyActiveUsers), Description = "This is a test")]
    [
        ReportParams("Date", ReportInputType.DateRange, OrderId = 1),
        ReportParams("First Name", ReportInputType.TextBox, OrderId = 3, DefaultValue = "Hello world"),
    ]
    public class DailyActiveUsers : ReportEntity, IReport
    {
        public DailyActiveUsers(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) { }

        protected override async Task<IEnumerable<object>> Main()
        {
            var parm = parameters;
            var firstName = GetParameterValue<string>("FirstName");
            var date = GetParameterValue<DateRange>("Date");
            

            return await Task.Run(() =>
            {
                var users = new List<User>();

                users.Add(new User()
                {
                    FirstName = "Brandon",
                    LastName = "Zuech"
                });
                users.Add(new User()
                {
                    FirstName = "Mallory",
                    LastName = "Zuech"
                });
                users.Add(new User()
                {
                    FirstName = "Cameron",
                    LastName = "Zuech"
                });
                users.Add(new User()
                {
                    FirstName = "Carter",
                    LastName = "Zuech"
                });

                return users;
            });
        }
    }

    public class User
    {
        [ColumnName("First Name")]
        public string FirstName { get; set; }
        [ColumnName("Last Name")]
        public string LastName { get; set; }
    }
}