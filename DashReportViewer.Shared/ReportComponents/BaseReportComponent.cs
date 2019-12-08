using DashReportViewer.Shared.Models.Widgets;
using System.Collections.Generic;

namespace DashReportViewer.Shared.ReportComponents
{
    public class BaseReportComponent
    {
        public string Name { get; set; }
        public int RowNum { get; set; }
        public int ColumnNum { get; set; }

        public BaseReportComponent(Widget widget)
        {
            this.Name = widget.Name;
            this.RowNum = widget.Row;
            this.ColumnNum = widget.Column;
            this.RawData = widget.Content;
        }

        public IEnumerable<object> RawData { get; protected set; }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}