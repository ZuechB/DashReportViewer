using DashReportViewer.Shared.Attributes;

namespace DashReportViewer.Models
{
    public class TasksPerSprint
    {
        [ColumnName("Name")]
        public string Username { get; set; }
        [ColumnName("Tickets")]
        public int Tickets { get; set; }
        [ColumnName("Sprint Points")]
        public int SprintPoints { get; set; }
        [ColumnName("Bugs")]
        public int Bugs { get; set; }
        [ColumnName("Bug Points")]
        public int BugPoints { get; set; }
        [ColumnName("Total")]
        public int Count { get; set; }
    }
}