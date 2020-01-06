using System.Collections.Generic;

namespace DashReportViewer.Shared.ReportContent
{
    public class TableContent : BaseReportContent
    {
        public IEnumerable<object> Content { get; set; }
        public List<string> Columns { get; set; }
    }
}