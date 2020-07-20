namespace DashReportViewer.ClickUp.Models
{
    public class Teams
    {
        public Team[] teams { get; set; }
    }

    public class Team
    {
        public string id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public object avatar { get; set; }
        public Member[] members { get; set; }
    }

    public class Team_Member
    {
        public Team_Member_User user { get; set; }
    }

    public class Team_Member_User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string color { get; set; }
        public string profilePicture { get; set; }
        public string initials { get; set; }
        public int role { get; set; }
    }
}