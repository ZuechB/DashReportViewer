using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DashReportViewer.Models.Reporting;
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


            if (report != null)
            {
                var viewModel = new ReportViewModel()
                {
                    ReportName = report.Name,
                    ReportDescription = report.Description,
                    UniqueID = report.Id,
                };

                if (report.Data != null)
                {
                    if (ContentType == ReportType.View)
                    {
                        if (report.ViewOptions.Count() > 0 && !report.ViewOptions.Contains(ReportType.View))
                        {

                            viewModel.Parameters = report.Parameters;
                            viewModel.Columns = new List<string>() { "Message" };
                            viewModel.Data = new List<List<object>>() { new List<object>() { "Please download the report to get results." } };
                            viewModel.ViewTypes = report.ViewOptions;

                        }
                        else if (report.Data.Count == 0)
                        {
                            viewModel.Parameters = report.Parameters;
                            viewModel.Columns = new List<string>() { "Message" };
                            viewModel.Data = new List<List<object>>() { new List<object>() { "No data is available. Please expand parameters or contact support." } };
                            viewModel.ViewTypes = report.ViewOptions;
                        }
                        else if (report.Data.Count > 1000)
                        {
                            viewModel.Parameters = report.Parameters;
                            viewModel.Columns = new List<string>() { "Message" };
                            viewModel.Data = new List<List<object>>() { new List<object>() { "Too many rows to display in browser - please narrow parameters or download the report to get results." } };
                            viewModel.ViewTypes = report.ViewOptions;
                        }
                        else
                        {
                            viewModel.Parameters = report.Parameters;
                            viewModel.Columns = report.Columns;
                            viewModel.Data = CellsToString(report.Data);
                            viewModel.ViewTypes = report.ViewOptions;
                        }

                        return View(viewModel);
                    }
                    //else
                    //{
                    //    var (extension, mimeType) = GetConstantsForFileTypes(ContentType);

                    //    //Handle turning one report into multiple files
                    //    if (report.MultipleFileExport != null)
                    //    {
                    //        var toProcess = report.MultipleFileExport(report.RawData);
                    //        List<(byte[] filedata, string filename)> files = new List<(byte[] filedata, string filename)>();
                    //        foreach (var item in toProcess)
                    //        {
                    //            (var columns, var data) = report.ProcessData(item.data);
                    //            var filename = item.filename;
                    //            if (string.IsNullOrEmpty(Path.GetExtension(filename)))
                    //            {
                    //                filename += extension;
                    //            }
                    //            files.Add((CreateFile(ContentType, columns, data, item.filename, report.HideHeader), filename));
                    //        }
                    //        if (files.Count == 1)
                    //        {
                    //            var (filedata, filename) = files.First();
                    //            return File(filedata, mimeType, filename);
                    //        }
                    //        else
                    //        {
                    //            using (var outputStream = new MemoryStream())
                    //            {
                    //                using (var archive = new ZipArchive(outputStream, ZipArchiveMode.Create, true))
                    //                {
                    //                    foreach (var file in files)
                    //                    {
                    //                        var filename = file.filename;
                    //                        if (string.IsNullOrEmpty(Path.GetExtension(filename)))
                    //                        {
                    //                            filename += extension;
                    //                        }
                    //                        var entry = archive.CreateEntry(filename);
                    //                        using (var zipStream = entry.Open())
                    //                        {
                    //                            zipStream.Write(file.filedata, 0, file.filedata.Length);
                    //                        }
                    //                    }
                    //                }
                    //                return File(outputStream.ToArray(), "application/zip", "ReportFiles.zip");
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        var filename = report.Filename ?? report.Name + " - " + DateTime.Now;
                    //        var file = CreateFile(ContentType, report.Columns, report.Data, report.Name, report.HideHeader);
                    //        return File(file, mimeType, filename + extension);
                    //    }
                    //}
                }
                //else if (report.Widgets == null || report.Widgets.Count == 0)
                //{
                //    viewModel.Parameters = report.Parameters;
                //    viewModel.Columns = new List<string>() { "Message" };
                //    viewModel.Data = new List<List<string>>() { new List<string>() { "No data is available. Please expand parameters or contact support." } };
                //    viewModel.ViewTypes = report.ViewOptions;

                //    return View(viewModel);
                //}
                else
                {
                    viewModel.Parameters = report.Parameters;
                    viewModel.Columns = new List<string>();
                    viewModel.Data = new List<List<object>>();
                    viewModel.ViewTypes = report.ViewOptions;

                    return View(viewModel);
                }
            }
            throw new Exception("Report is null");
        }

        //private byte[] CreateFile(ReportType contentType, List<string> columns, List<List<object>> data, string reportName = null, bool hideHeader = false)
        //{
        //    switch (contentType)
        //    {
        //        case ReportType.Word:
        //            var dataTable = ConvertToDataTable(columns, data);

        //            using (var memoStream = new MemoryStream())
        //            {
        //                using (StreamWriter writer = new StreamWriter(memoStream))
        //                {
        //                    HtmlTextWriter hw = new HtmlTextWriter(writer);

        //                    System.Web.UI.WebControls.GridView GridView1 = new System.Web.UI.WebControls.GridView()
        //                    {
        //                        AllowPaging = false,
        //                        DataSource = dataTable
        //                    };
        //                    GridView1.DataBind();
        //                    GridView1.RenderControl(hw);
        //                }
        //                return memoStream.ToArray();
        //            }

        //        case ReportType.Excel:
        //            dataTable = ConvertToDataTable(columns, data);

        //            var culture = CultureInfo.CurrentCulture;
        //            using (ExcelPackage pck = new ExcelPackage())
        //            {
        //                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(reportName);

        //                for (int i = 0; i < columns.Count; i++)
        //                {
        //                    ws.Cells[1, i + 1].Value = columns[i];
        //                }
        //                for (int i = 0; i < data.Count; i++)
        //                {
        //                    for (int j = 0; j < data.First().Count; j++)
        //                    {
        //                        var cell = ws.Cells[i + 2, j + 1];
        //                        var cellData = data[i][j];

        //                        if (cellData.GetType() == typeof(decimal))
        //                        {
        //                            cell.Style.Numberformat.Format = "$###,###,##0.00";
        //                        }
        //                        else if (cellData.GetType() == typeof(DateTime))
        //                        {
        //                            cell.Style.Numberformat.Format = "mm/dd/yy h:mm";
        //                        }

        //                        cell.Value = cellData;
        //                    }
        //                }
        //                return pck.GetAsByteArray();
        //            }

        //        case ReportType.PDF:
        //            using (MemoryStream stream = new MemoryStream())
        //            {
        //                var document = new MigraDoc.DocumentObjectModel.Document();
        //                document.Info.Title = reportName;
        //                document.Info.Author = "Enigma Systems, LLC";

        //                MigraDoc.DocumentObjectModel.Section section = document.AddSection();

        //                var DocData = section.AddTextFrame();
        //                DocData.Top = "1.0cm";
        //                DocData.Width = "12cm";
        //                DocData.AddParagraph(reportName);
        //                DocData.AddParagraph("Report generated on: " + DateTime.Now.ToString());

        //                var table = section.AddTable();
        //                table.Style = "Table";
        //                table.Borders.Width = 0.25;
        //                table.Borders.Left.Width = 0.5;
        //                table.Borders.Right.Width = 0.5;
        //                table.Rows.LeftIndent = 0;

        //                if (!hideHeader)
        //                {
        //                    for (int i = 0; i < columns.Count(); i++)
        //                    {
        //                        var column = table.AddColumn();
        //                        column.Format.Alignment = MigraDoc.DocumentObjectModel.ParagraphAlignment.Left;
        //                    }
        //                }

        //                var row = table.AddRow();
        //                for (int i = 0; i < columns.Count(); i++)
        //                {
        //                    row.Cells[i].AddParagraph(columns[i]);
        //                }

        //                foreach (var dataRow in data)
        //                {
        //                    var index = 0;
        //                    var row2 = table.AddRow();
        //                    foreach (var dataColumn in dataRow)
        //                    {
        //                        row2.Cells[index].AddParagraph(CellToString(dataColumn));
        //                        index++;
        //                    }
        //                }

        //                MigraDoc.Rendering.PdfDocumentRenderer renderer = new MigraDoc.Rendering.PdfDocumentRenderer(true)
        //                {
        //                    Document = document
        //                };
        //                renderer.RenderDocument();

        //                renderer.Save(stream, true);

        //                return stream.ToArray();
        //            }
        //        case ReportType.CSV:
        //            var fileData = FileExtensions.CreateCsvFile(data.Select(r => r.Select(c => c.ToString())), hideHeader ? null : columns);

        //            return fileData;

        //        default:
        //            return null;
        //    }

        //}

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