namespace DashReportViewer.ClickUp.Models
{
    public class Members
    {
        public Member[] members { get; set; }
    }

    public class Member
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
