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
            var result = Compile.Execute(code);
            return Json(result);
        }
    }
}