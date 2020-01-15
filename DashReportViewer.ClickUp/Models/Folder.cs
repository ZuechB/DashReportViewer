namespace DashReportViewer.ClickUp.Models
{
    public class Folders
    {
        public Folders_Folder[] folders { get; set; }
    }

    public class Folders_Folder
    {
        public string id { get; set; }
        public string name { get; set; }
        public int orderindex { get; set; }
        public string override_statuses { get; set; }
        public bool hidden { get; set; }
        public string task_count { get; set; }
        public bool archived { get; set; }
        public Folders_List[] lists { get; set; }
        public Folders_Status2[] statuses { get; set; }
    }

    public class Folders_List
    {
        public string id { get; set; }
        public string name { get; set; }
        public int orderindex { get; set; }
        public object content { get; set; }
        public Folders_Status status { get; set; }
        public object priority { get; set; }
        public object assignee { get; set; }
        public string task_count { get; set; }
        public object due_date { get; set; }
        public object start_date { get; set; }
        public bool archived { get; set; }
        public string override_statuses { get; set; }
        public Folders_Status1[] statuses { get; set; }
    }

    public class Folders_Status
    {
        public string status { get; set; }
        public string color { get; set; }
        public bool hide_label { get; set; }
    }

    public class Folders_Status1
    {
        public string status { get; set; }
        public int orderindex { get; set; }
        public string color { get; set; }
        public string type { get; set; }
    }

    public class Folders_Status2
    {
        public string status { get; set; }
        public string type { get; set; }
        public int orderindex { get; set; }
        public string color { get; set; }
    }
}