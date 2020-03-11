using System;
using System.Linq;
using DashReportViewer.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DashReportViewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ReportAPIController : ControllerBase
    {
        readonly IReportService reportService;
        public ReportAPIController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var reports = reportService.GetReports(AppDomain.CurrentDomain);
            return Ok(reports.Select(s => new
            {
                s.Id,
                s.Name,
                s.Description
            }));
        }
    }
}