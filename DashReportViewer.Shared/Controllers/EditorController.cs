using DashReportViewer.Shared.Models;
using DashReportViewer.Shared.RealTimeCompiler;
using DashReportViewer.Shared.Services;
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
    public class EditorController : Controller
    {
        readonly IReportService reportService;
        readonly DashReportAppSettings appSettings;
        public EditorController(IReportService reportService, IOptions<DashReportAppSettings> appSettings)
        {
            this.reportService = reportService;
            this.appSettings = appSettings.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Execute(string code)
        {
            Guid id = Guid.Parse("a21fda13-f36f-4af5-ac05-2a3dabae04ef");
            await reportService.UpdateCode(id, code);
            var result = await Compile.Execute(id, code, reportService, appSettings);
            return Json(result);
        }
    }
}