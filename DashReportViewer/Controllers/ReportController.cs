using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DashReportViewer.Models.Reporting;
using DashReportViewer.Models.Widgets;
using DashReportViewer.ReportComponents;
using DashReportViewer.Services;
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

            var report = await reportService.RunReport(reportType, paramsList);


            var components = new List<BaseReportComponent>();
            foreach (Widget widget in report.RawData)
            {
                switch(widget.WidgetType)
                {
                    case WidgetType.Table:
                        components.Add(new TableComponent(widget));
                        break;
                    case WidgetType.BarGraph:
                        components.Add(new AreaChartComponent(widget));
                        break;
                }
            }

            if (report != null)
            {
                var viewModel = new ReportViewModel()
                {
                    ReportName = report.Name,
                    ReportDescription = report.Description,
                    UniqueID = report.Id,
                    Components = components
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