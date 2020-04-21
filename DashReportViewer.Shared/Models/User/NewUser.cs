using System.Collections.Generic;

namespace DashReportViewer.Shared.Models.User
{
    public class NewUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public string Timezone { get; set; }
    }

    public enum Role
    {
        Admin = 1
    }

    public class NewUserResult
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}