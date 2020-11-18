namespace DashReportViewer.ClickUp.Models
{
    public class Tasks
    {
        public Tasks_Task[] tasks { get; set; }
    }

    public class Tasks_Task
    {
        public string id { get; set; }
        public string name { get; set; }
        public string text_content { get; set; }
        public Tasks_Status status { get; set; }
        public string orderindex { get; set; }
        public string date_created { get; set; }
        public string date_updated { get; set; }
        public object date_closed { get; set; }
        public bool archived { get; set; }
        public Tasks_Creator creator { get; set; }
        public Tasks_Assignee[] assignees { get; set; }
        public object[] checklists { get; set; }
        public Tag[] tags { get; set; }
        public object parent { get; set; }
        public object priority { get; set; }
        public object due_date { get; set; }
        public object start_date { get; set; }
        public object points { get; set; }
        public object time_estimate { get; set; }
        public Tasks_List list { get; set; }
        public Tasks_Project project { get; set; }
        public Tasks_Folder folder { get; set; }
        public Tasks_Space space { get; set; }
        public Tasks_Custom_Fields[] custom_fields { get; set; }
        public object[] dependencies { get; set; }
        public string team_id { get; set; }
        public string url { get; set; }
    }

    public class Tasks_Status
    {
        public string status { get; set; }
        public string color { get; set; }
        public string type { get; set; }
        public int orderindex { get; set; }
    }

    public class Tasks_Creator
    {
        public int id { get; set; }
        public string username { get; set; }
        public string color { get; set; }
        public object profilePicture { get; set; }
    }

    public class Tasks_List
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool access { get; set; }
    }

    public class Tasks_Project
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool hidden { get; set; }
        public bool access { get; set; }
    }

    public class Tasks_Folder
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool hidden { get; set; }
        public bool access { get; set; }
    }

    public class Tasks_Space
    {
        public string id { get; set; }
    }

    public class Tasks_Assignee
    {
        public int id { get; set; }
        public string username { get; set; }
        public string color { get; set; }
        public string initials { get; set; }
        public object profilePicture { get; set; }
    }

    public class Tasks_Custom_Fields
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public Tasks_Type_Config type_config { get; set; }
        public string date_created { get; set; }
        public bool hide_from_guests { get; set; }
        public string value { get; set; }
    }

    public class Tasks_Type_Config
    {
        public int _default { get; set; }
        public object placeholder { get; set; }
        public bool new_drop_down { get; set; }
        public Tasks_Option[] options { get; set; }
    }

    public class Tasks_Option
    {
        public string id { get; set; }
        public string name { get; set; }
        public object value { get; set; }
        public string type { get; set; }
        public string color { get; set; }
        public int orderindex { get; set; }
    }

}
