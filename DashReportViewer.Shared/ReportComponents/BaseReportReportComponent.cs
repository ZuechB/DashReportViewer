using DashReportViewer.Shared.Models.Widgets;
using DashReportViewer.Shared.ReportContent;
using System.Collections.Generic;

namespace DashReportViewer.Shared.ReportComponents
{
    public class BaseReportReportComponent
    {
        public string Name { get; set; }
        public int RowNum { get; set; }
        public int ColumnNum { get; set; }
        public int Index { get; set; }

        public BaseReportReportComponent(Widget widget)
        {
            this.Name = widget.Name;
            this.RowNum = widget.Row;
            this.ColumnNum = widget.Column;
            this.RawData = widget.Content;
        }

        public BaseReportContent RawData { get; protected set; }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}