using DashReportViewer.Shared.Attributes;

namespace DashReportViewer.Models
{
    public class TasksPerSprint
    {
        [ColumnName("Name")]
        public string Username { get; set; }
        [ColumnName("Sprint Points")]
        public int SprintPoints { get; set; }
        [ColumnName("Total")]
        public int Count { get; set; }
    }
}