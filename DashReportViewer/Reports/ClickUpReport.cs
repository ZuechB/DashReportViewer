using DashReportViewer.ClickUp;
using DashReportViewer.Models;
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
    [ReportName("Click Up Sample", "724DB366-5A96-43F5-A2FC-20DC108597E8", Description = "Shows how to connect to third party services that are connected to DashReportViewer")]
    [
        ReportParams("Date", ReportInputType.DateRange, OrderId = 1),
        ReportParams("First Name", ReportInputType.TextBox, OrderId = 2)
    ]
    public class ClickUpReport : ReportEntity, IReport
    {
        readonly IClickUpService clickUpService;
        public ClickUpReport(Dictionary<string, object> parameterValues, IReportService reportService) : base(parameterValues, reportService) 
        {
            clickUpService = GetService<IClickUpService>();
        }

        protected override async Task<IEnumerable<object>> Main()
        {
            var firstName = GetParameterValue<string>("FirstName");
            var date = GetParameterValue<DateRange>("Date");
            
            var widgets = new List<Widget>();
            widgets = await GetAllSprints(widgets);
            return widgets;
        }

        private async Task<List<Widget>> GetAllSprints(List<Widget> widgets)
        {
            var lists = await clickUpService.GetLists("2339092", "sprint"); // will get all the lists with the name sprint in it like for example: sprint 1, sprint 2, etc...
            foreach (var item in lists)
            {
                var tasks = await clickUpService.GetTasks(item.Key, true);


                var sprintTasks = new List<TasksPerSprint>();

                var tasksByUser = tasks.Where(t => t.status.type == "closed").GroupBy(t => t.assignees.FirstOrDefault().username);
                foreach (var userTask in tasksByUser)
                {
                    var sprintTask = new TasksPerSprint();
                    sprintTask.Username = userTask.Key;
                    sprintTask.Count = userTask.Count();

                    foreach (var task in userTask)
                    {
                        var scrumPoint = task.custom_fields.Where(c => c.name == "Scrum Points").FirstOrDefault();
                        var option = scrumPoint.type_config.options.Where(t => t.orderindex == scrumPoint.value).FirstOrDefault();
                        
                        sprintTask.SprintPoints += Convert.ToInt32(option.name);
                    }

                    sprintTasks.Add(sprintTask);
                }

                sprintTasks.Add(new Models.TasksPerSprint()
                {
                    Username = "Total",
                    Count = sprintTasks.Sum(t => t.Count),
                    SprintPoints = sprintTasks.Sum(t => t.SprintPoints)
                });

                widgets.Add(new Widget(item.Value)
                {
                    Content = new TableContent()
                    {
                        Content = sprintTasks
                    },
                    Column = 6
                });
            }

            return widgets;
        }
    }
}