using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashReportViewer.Shared.Models.Reporting
{
    public interface IReport
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
        IList<ReportParams> Parameters { get; }
        bool HideHeader { get; }
        IList<ReportType> ViewOptions { get; }
        IEnumerable<object> RawData { get; }
        //List<string> Columns { get; }
        //List<List<object>> Data { get; }

        //string Filename { get; set; }
        //Func<object, List<(string filename, List<object> data)>> MultipleFileExport { get; set; }
        //(List<string> columns, List<List<object>> data) ProcessData(IEnumerable<object> reportData);

        // row clicked
        void RowClick(string Id);
        void RowClick(int Id);
        void RowClick(Guid Id);
        void RowClick(long Id);

        Task Run();
    }
}
