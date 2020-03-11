using DashReportViewer.AzureDevOps.Models;
using DashReportViewer.AzureDevOps.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DashReportViewer.AzureDevOps.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class AzureDevOpsController : ControllerBase
    {
        readonly IAzureDevOpsService azureDevOpsService;
        public AzureDevOpsController(IAzureDevOpsService azureDevOpsService)
        {
            this.azureDevOpsService = azureDevOpsService;
        }
        
        [HttpPost]
        public async Task<IActionResult> ReleaseDeploymentStarted(ReleaseDeploymentStarted releaseDeployment)
        {
            await azureDevOpsService.Release(releaseDeployment);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ReleaseDeploymentCompleted(ReleaseDeploymentStarted releaseDeployment)
        {
            await azureDevOpsService.Release(releaseDeployment);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ReleaseCreated(ReleaseDeploymentStarted releaseDeployment)
        {
            await azureDevOpsService.Release(releaseDeployment);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ReleaseAbandoned(ReleaseDeploymentStarted releaseDeployment)
        {
            await azureDevOpsService.Release(releaseDeployment);
            return Ok();
        }


    }
}



// http://9d723c77.ngrok.io/api/AzureDevOps/ReleaseDeploymentStarted