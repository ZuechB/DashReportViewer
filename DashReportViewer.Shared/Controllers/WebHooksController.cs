using System;
using System.Linq;
using System.Threading.Tasks;
using DashReportViewer.AzureDevOps.Models;
using DashReportViewer.Shared.Models.AzureDevOps;
using DashReportViewer.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DashReportViewer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class WebHooksController : ControllerBase
    {
        readonly IAzureDevOpsService azureDevOpsService;
        public WebHooksController(IAzureDevOpsService azureDevOpsService)
        {
            this.azureDevOpsService = azureDevOpsService;
        }

        [HttpPost("{actionType}")]
        public async Task<IActionResult> AzureDevOps(AzureDevOpsActionType actionType, [FromBody]ReleaseDeployment payload)
        {
            await azureDevOpsService.Release(payload);
            return Ok();
        }
    }
}