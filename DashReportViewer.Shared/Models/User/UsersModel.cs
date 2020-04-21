using DashReportViewer.Models;
using System.Collections.Generic;

namespace DashReportViewer.Shared.Models.User
{
    public class UsersModel
    {
        public string SideBarBackgroundColor { get; set; }
        public List<ApplicationUser> ApplicationUsers { get; set; }
    }
}