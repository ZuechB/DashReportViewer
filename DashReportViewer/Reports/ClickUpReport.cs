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
    [ReportName("Click Up Sample", "724DB366-5A96-43F5-A2FC-20DC108597E8", Description = "Shows how to connect to third party service that is connected to DashReportViewer")]
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
            //var firstName = GetParameterValue<string>("FirstName");
            //var date = GetParameterValue<DateRange>("Date");
            
            var widgets = new List<Widget>();
            widgets = await GetAllSprints(widgets);

            widgets.Insert(0, TrendsGraph(widgets));

            return widgets;
        }

        private Widget TrendsGraph(List<Widget> widgets)
        {
            var dataPoints = new List<AreaChartDataPoint>();

            var sprintPoints = new List<int>();
            var bugPoints = new List<int>();
            var sprintNames = new List<string>();
            sprintNames.Add("Sprints");

            foreach (var widget in widgets)
            {
                sprintNames.Add(widget.Name);

                var table = (TableContent)widget.Content;
                var tasks = (List<TasksPerSprint>)table.Content;

                sprintPoints.Add(tasks.Where(t => t.Username != "Total").Sum(t => t.SprintPoints));
                bugPoints.Add(tasks.Where(t => t.Username != "Total").Sum(t => t.BugPoints));
            }

            dataPoints.Add(new AreaChartDataPoint()
            {
                Label = "Sprint Points",
                Data = sprintPoints
            });

            dataPoints.Add(new AreaChartDataPoint()
            {
                Label = "Bug Points",
                Data = bugPoints
            });

            return new Widget("Tends over time")
            {
                Content = new AreaChartContent()
                {
                    dataPoints = dataPoints,
                    XAxis = sprintNames
                },
                Column = 12
            };
        }

        private async Task<List<Widget>> GetAllSprints(List<Widget> widgets)
        {
            var lists = await clickUpService.GetLists("2339092", "sprint"); // will get all the lists with the name sprint in it like for example: sprint 1, sprint 2, etc...
            foreach (var item in lists)
            {
                var tasks = await clickUpService.GetTasks(item.Key, true);

                var sprintTasks = new List<TasksPerSprint>();

                var closedTickets = tasks.Where(t => t.status.type == "closed");
                if (closedTickets != null)
                {
                    var tasksByUser = closedTickets.GroupBy(t => t.assignees.FirstOrDefault() != null ? t.assignees.FirstOrDefault().username : null);
                    foreach (var userTask in tasksByUser)
                    {
                        var sprintTask = new TasksPerSprint();
                        sprintTask.Username = userTask.Key;
                        sprintTask.Count = userTask.Count();

                        int totalTickets = 0;
                        int totalBugs = 0;
                        foreach (var task in userTask)
                        {
                            bool isBug = false;
                            if (task.tags != null && task.tags.Any())
                            {
                                var bug = task.tags.Where(t => t.name.ToLower() == "bug").FirstOrDefault();
                                if (bug != null)
                                {
                                    isBug = true;
                                }
                            }

                            var scrumPoint = task.custom_fields.Where(c => c.name == "Scrum Points").FirstOrDefault();
                            if (scrumPoint != null)
                            {
                                var option = scrumPoint.type_config.options.Where(t => t.orderindex == scrumPoint.value).FirstOrDefault();
                                if (option != null)
                                {
                                    if (!String.IsNullOrWhiteSpace(option.name))
                                    {
                                        if (!isBug)
                                        {
                                            sprintTask.SprintPoints += Convert.ToInt32(option.name);
                                            totalTickets++;
                                        }
                                        else
                                        {
                                            sprintTask.BugPoints += Convert.ToInt32(option.name);
                                            totalBugs++;
                                        }
                                    }
                                }
                            }
                        }

                        sprintTask.Tickets = totalTickets;
                        sprintTask.Bugs = totalBugs;

                        sprintTasks.Add(sprintTask);
                    }

                    if (sprintTasks != null && !String.IsNullOrWhiteSpace(item.Value))
                    {
                        var count = sprintTasks.Sum(t => t.Count);
                        var sprintPoints = sprintTasks.Sum(t => t.SprintPoints);

                        if (count == 0 && sprintPoints == 0)
                        {
                            continue;
                        }

                        sprintTasks.Add(new Models.TasksPerSprint()
                        {
                            Username = "Total",
                            Count = count,
                            SprintPoints = sprintPoints
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
                }
            }

            return widgets;
        }
    }
}