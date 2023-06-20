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


                
                var wideTableData = new List<WideTable>();
                wideTableData.Add(new WideTable()
                {
                    Test1 = "test asdsadsdsaddasdasdadasds",
                    Test10 = "test asdsadsadasasdasd",
                    Test11 = "test",
                    Test12 = "test",
                    Test13 = "test asdasdasdasdasdasd",
                    Test14 = "test",
                    Test15 = "test",
                    Test16 = "test",
                    Test17 = "test",
                    Test18 = "test",
                    Test2 = "test",
                    Test3 = "test",
                    Test4 = "test adsadasddsdasd",
                    Test5 = "test",
                    Test6 = "testasdasdasdasdsddasdasd",
                    Test7 = "test asdasdasdasd",
                    Test8 = "test asdsadsdasdasd",
                    Test9 = "testasdasdasdasdasd",
                });

                widgets.Add(new Widget("Users")
                {
                    Content = new TableContent()
                    {
                        Content = wideTableData

                    },
                    Column = 12
                });


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

    public class WideTable
    {
        public string Test1 { get; set; }
        public string Test2 { get; set; }
        public string Test3 { get; set; }
        public string Test4 { get; set; }
        public string Test5 { get; set; }
        public string Test6 { get; set; }
        public string Test7 { get; set; }
        public string Test8 { get; set; }
        public string Test9 { get; set; }
        public string Test10 { get; set; }
        public string Test11 { get; set; }
        public string Test12 { get; set; }
        public string Test13 { get; set; }

        public string Test14 { get; set; }
        public string Test15 { get; set; }

        public string Test16 { get; set; }
        public string Test17 { get; set; }
        public string Test18 { get; set; }
    }
}