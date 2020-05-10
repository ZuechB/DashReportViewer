using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DashReportViewer.Shared.Models;
using DashReportViewer.Shared.Models.Reporting;
using DashReportViewer.Shared.Models.Widgets;
using DashReportViewer.Shared.ReportComponents;
using DashReportViewer.Shared.ReportContent;
using DashReportViewer.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DashReportViewer.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        readonly IReportService reportService;
        readonly DashReportAppSettings appSettings;

        public ReportController(IReportService reportService, IOptions<DashReportAppSettings> appSettings)
        {
            this.reportService = reportService;
            this.appSettings = appSettings.Value;
        }

        public IActionResult Index()
        {
            var report = reportService.GetReports(AppDomain.CurrentDomain).FirstOrDefault();
            if (report != null)
            {
                return Redirect("~/report/reports?reportType=" + report.Id.ToString());
            }
            return View();
        }

        public IActionResult LogOut()
        {
            return Redirect("~/home/logout");
        }

        public async Task<ActionResult> Reports(Guid reportType, string param = null, ReportType ContentType = ReportType.View)
        {
            var paramsList = new Dictionary<string, object>();

            if (!String.IsNullOrWhiteSpace(param))
            {
                var fields = param.Split(',');
                foreach (var fieldItem in fields)
                {
                    var fielder = fieldItem.Replace("[", "");
                    fielder = fielder.Replace("]", "");
                    var fieldDef = fielder.Split('~');

                    var name = fieldDef[0];
                    var value = fieldDef[1];

                    paramsList.Add(name, value);
                }
            }

            var report = await reportService.RunReport(AppDomain.CurrentDomain, reportType, paramsList);


            var components = new List<BaseReportReportComponent>();
            foreach (Widget widget in report.RawData)
            {
                if (widget.Content.GetType() == typeof(TableContent))
                {
                    components.Add(new TableReportComponent(widget));
                }
                else if (widget.Content.GetType() == typeof(AreaChartContent))
                {
                    components.Add(new AreaChartReportComponent(widget));
                }
                else if (widget.Content.GetType() == typeof(BubbleChartContent))
                {
                    components.Add(new BubbleChartReportComponent(widget));
                }
                else if (widget.Content.GetType() == typeof(CalendarChartContent))
                {
                    components.Add(new CalendarChartReportComponent(widget));
                }
                else if (widget.Content.GetType() == typeof(PieChartContent))
                {
                    components.Add(new PieChartReportComponent(widget));
                }
                else if (widget.Content.GetType() == typeof(HistogramsContent))
                {
                    components.Add(new HistogramsReportComponent(widget));
                }
                else if (widget.Content.GetType() == typeof(ScatterChartContent))
                {
                    components.Add(new ScatterChartReportComponent(widget));
                }
                else if (widget.Content.GetType() == typeof(TextContent))
                {
                    components.Add(new TextReportComponent(widget));
                }
                else if (widget.Content.GetType() == typeof(AnnotationChartContent))
                {
                    components.Add(new AnnotationChartReportComponent(widget));
                }
            }

            if (report != null)
            {
                var viewModel = new ReportViewModel()
                {
                    ReportName = report.Name,
                    ReportDescription = report.Description,
                    UniqueID = report.Id,
                    Components = components,
                    ContentType = ContentType,
                    Parameters = report.Parameters,
                    SideBarBackgroundColor = appSettings.SideBarColor
                };

                return View(viewModel);
            }
            throw new Exception("Report is null");
        }
    }
}