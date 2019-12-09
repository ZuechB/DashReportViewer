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
    [ReportName("Daily Active Users", "E7135E62-0DCA-4133-8273-5606182F9EDD", Description = "This is a test")]
    [
        ReportParams("Date", ReportInputType.DateRange, OrderId = 1),
        ReportParams("First Name", ReportInputType.TextBox, OrderId = 2),
        ReportParams("MultipleOptions", ReportInputType.CustomOption, OrderId = 3),
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
                //return GetUsersList();

                var widgets = new List<Widget>();

                widgets.Add(GetUsers(firstName));
                widgets.Add(GetUsers(null));

                return widgets;
            });
        }

        private List<User> GetUsersList()
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
        }

        [ReportParams("Multiple Options")]
        public List<KeyValuePair<string, string>> MultipleOptions()
        {
            var options = new List<KeyValuePair<string, string>>();

            options.Add(new KeyValuePair<string, string>("1", "This is a test"));
            options.Add(new KeyValuePair<string, string>("2", "This is another test"));

            return options;
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

            return new Widget("Users", WidgetType.Table) { Content = users, Column = 6 };
        }
    }

    public class User
    {
        [ColumnName("First Name")]
        public string FirstName { get; set; }
        [ColumnName("Last Name")]
        public string LastName { get; set; }
        [ColumnName("Joined")]
        public DateTimeOffset Created { get; set; }
    }
}