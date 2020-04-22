using DashReportViewer.Shared.Attributes;
using DashReportViewer.Shared.Models;
using DashReportViewer.Shared.Models.Reporting;
using DashReportViewer.Shared.Models.Widgets;
using DashReportViewer.Shared.ReportContent;
using DashReportViewer.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashReportViewer.Reports
{
    [ReportName("Table Report", "E7135E62-0DCA-4133-8273-5606182F9EDD", Description = "This is a test")]
    public class TableReport : ReportEntity, IReport
    {
        public TableReport(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) { }

        protected override async Task<IEnumerable<object>> Main()
        {
            var parm = parameters;

            return await Task.Run(() =>
            {
            var widgets = new List<Widget>();

            var users = new List<User>();

            users.Add(new User()
            {
                FirstName = "FirstName1",
                LastName = "FirstName1"
            });
            users.Add(new User()
            {
                FirstName = "FirstName2",
                LastName = "FirstName2"
            });
            users.Add(new User()
            {
                FirstName = "FirstName3",
                LastName = "FirstName3"
            });
            users.Add(new User()
            {
                FirstName = "FirstName4",
                LastName = "FirstName4"
            });

            widgets.Add(new Widget("Users") {
                Content = new TableContent()
                {
                    Content = users

                }, Column = 6 });


            return widgets;
            });
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