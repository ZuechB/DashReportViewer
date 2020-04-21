using DashReportViewer.Shared.Models;
using DashReportViewer.Shared.Models.User;
using DashReportViewer.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DashReportViewer.Shared.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        readonly DashReportAppSettings appSettings;
        readonly IUserService userService;

        public UsersController(IOptions<DashReportAppSettings> appSettings, IUserService userService)
        {
            this.appSettings = appSettings.Value;
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await userService.GetUsers();
            return View(new UsersModel()
            {
                SideBarBackgroundColor = appSettings.SideBarColor,
                ApplicationUsers = users
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(long id, string firstName, string lastName, string email, int role)
        {
            await userService.UpdateUser(id, firstName, lastName, email, role);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GetUser(long userId)
        {
            var user = await userService.GetUser(userId);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(NewUser user)
        {
            var userResult = await userService.CreateUser(user);
            return Ok(userResult);
        }
    }
}