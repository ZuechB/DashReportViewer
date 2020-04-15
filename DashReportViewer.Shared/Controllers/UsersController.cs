using DashReportViewer.Shared.Models;
using DashReportViewer.Shared.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashReportViewer.Shared.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        readonly DashReportAppSettings appSettings;

        public UsersController(IOptions<DashReportAppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        public IActionResult Index()
        {
            return View(new UsersModel()
            {
                SideBarBackgroundColor = appSettings.SideBarColor
            });
        }
    }
}