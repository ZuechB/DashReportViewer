using System.Threading.Tasks;
using DashReportViewer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DashReportViewer.Controllers
{
    public class HomeController : Controller
    {
        readonly SignInManager<ApplicationUser> _signInManager;
        public HomeController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }
    }
}