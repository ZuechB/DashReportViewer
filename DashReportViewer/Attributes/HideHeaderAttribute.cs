using System;

namespace DashReportViewer.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HideHeaderAttribute : Attribute
    {
        public HideHeaderAttribute() { }
    }
}
