using DashReportViewer.Attributes;
using DashReportViewer.Models.Reporting;
using DashReportViewer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashReportViewer.Reports
{
    [ReportName("Daily Sales", "E7135E62-0DCA-4133-8273-5606182F9EDD", typeof(DailyActiveUsers))]
    [ReportParams("Start Date", ReportInputType.DateTime, OrderId = 1, DefaultValue = "day:0_starttime"),
     ReportParams("End Date", ReportInputType.DateTime, OrderId = 2, DefaultValue = "day:0_endtime"),
     //ReportParams("Select Location", ReportInputType.CustomOption, OrderId = 3, DefaultValue = (int)ReportParams.ListOptions.ALL),
     //ReportParams("Select Status", ReportInputType.CustomOption, OrderId = 4, DefaultValue = (int)ReportParams.ListOptions.ALL)
        ]
    public class DailyActiveUsers : ReportEntity, IReport
    {
        public DailyActiveUsers(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) { }

        protected override async Task<IEnumerable<object>> Main()
        {
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
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
