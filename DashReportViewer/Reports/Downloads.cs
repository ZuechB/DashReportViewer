using DashReportViewer.Shared.Attributes;
using DashReportViewer.Shared.Models;
using DashReportViewer.Shared.Models.Reporting;
using DashReportViewer.Shared.Models.Widgets;
using DashReportViewer.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashReportViewer.Reports
{
    [ReportName("Downloads", "E79AA1F9-76B3-4902-891E-10104F6BD54B", Description = "This is a test")]
    [
    ReportParams("Date", ReportInputType.DateRange, OrderId = 1),
    ReportParams("First Name", ReportInputType.TextBox, OrderId = 2)
]
    public class Downloads : ReportEntity, IReport
    {
        public Downloads(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) { }

        protected override async Task<IEnumerable<object>> Main()
        {
            var parm = parameters;

            var firstName = GetParameterValue<string>("FirstName");
            var date = GetParameterValue<DateRange>("Date");


            return await Task.Run(() =>
            {
                var widgets = new List<Widget>();

                widgets.Add(GetUsers(firstName));
                widgets.Add(GetUsers(null));

                return widgets;
            });
        }

        private Widget GetUsers(string firstName)
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

            if (!String.IsNullOrWhiteSpace(firstName))
            {
                users = users.Where(u => u.FirstName.ToLower().Contains(firstName.ToLower())).ToList();
            }

            return new Widget("Users", WidgetType.AreaChart) { Content = users, Column = 6 };
        }
    }
}
