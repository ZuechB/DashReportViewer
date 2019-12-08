using System.Collections.Generic;

namespace DashReportViewer.Shared.Models.Widgets
{
    public class Widget
    {
        public Widget(string Name, WidgetType WidgetType)
        {
            this.Name = Name;
            this.WidgetType = WidgetType;
        }

        public WidgetType WidgetType { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public string Name { get; set; }
        public IEnumerable<object> Content { get; set; }
    }

    public enum WidgetType
    {
        Table,
        BarGraph
    }
}