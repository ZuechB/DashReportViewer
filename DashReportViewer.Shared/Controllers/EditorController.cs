using DashReportViewer.Shared.RealTimeCompiler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashReportViewer.Shared.Controllers
{
    [Authorize]
    public class EditorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Execute(string code)
        {
            Guid id = Guid.Parse("a21fda13-f36f-4af5-ac05-2a3dabae04ef");
            var result = Compile.Execute(id, code);
            return Json(result);
        }
    }
}