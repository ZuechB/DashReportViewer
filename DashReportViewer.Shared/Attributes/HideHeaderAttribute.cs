using System;

namespace DashReportViewer.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HideHeaderAttribute : Attribute
    {
        public HideHeaderAttribute() { }
    }
}
