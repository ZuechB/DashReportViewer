using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DashReportViewer.Shared.Models.Reporting;
using DashReportViewer.Shared.Models.Widgets;
using DashReportViewer.Shared.ReportComponents;
using DashReportViewer.Shared.ReportContent;
using DashReportViewer.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace DashReportViewer.Controllers
{
    public class ReportController : Controller
    {
        readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        public IActionResult Index()
        {
            return View();
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
            }

            if (report != null)
            {
                var viewModel = new ReportViewModel()
                {
                    ReportName = report.Name,
                    ReportDescription = report.Description,
                    UniqueID = report.Id,
                    Components = components,
                    Parameters = report.Parameters
                };

                return View(viewModel);
            }
            throw new Exception("Report is null");
        }


        private DataTable ConvertToDataTable(List<string> columns, List<List<object>> dataTable)
        {
            DataTable dt = new DataTable();
            foreach (var cItem in columns)
            {
                dt.Columns.Add(cItem);
            }

            foreach (var dItem in dataTable)
            {
                object[] rowObj = new object[(dItem.Count())];
                for (int i = 0; i < (dItem.Count()); i++)
                {
                    rowObj[i] = dItem[i];
                }

                dt.Rows.Add(rowObj);
            }
            return dt;
        }

        private string CellToString(object dataCell)
        {
            if (dataCell.GetType() == typeof(decimal))
            {
                return ((decimal)dataCell).ToString("C");
            }

            return dataCell.ToString();
        }

        private List<List<object>> CellsToString(List<List<object>> data)
        {
            List<List<object>> toReturn = new List<List<object>>();
            foreach (var row in data)
            {
                var stringRow = new List<object>();
                toReturn.Add(stringRow);
                foreach (var cell in row)
                {
                    stringRow.Add(CellToString(cell));
                }
            }

            return toReturn;
        }
    }
}