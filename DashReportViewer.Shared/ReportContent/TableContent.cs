using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashReportViewer.Shared.ReportContent
{
    public class TableContent : BaseReportContent
    {
        public IEnumerable<object> Content { get; set; }
    }
}
