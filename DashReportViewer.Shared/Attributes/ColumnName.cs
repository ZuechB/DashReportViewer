using System;

namespace DashReportViewer.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute : Attribute
    {
        public string Name { get; set; }

        public ColumnNameAttribute(string Name)
        {
            this.Name = Name;
        }
    }
}