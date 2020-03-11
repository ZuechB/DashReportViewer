using DashReportViewer.AzureDevOps.Models;
using DashReportViewer.Context;
using DashReportViewer.Shared.Models.AzureDevOps;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DashReportViewer.Shared.Services
{
    public interface IAzureDevOpsService
    {
        Task Release(ReleaseDeployment releaseDeployment);
    }

    public class AzureDevOpsService : IAzureDevOpsService
    {
        readonly DashReportViewerContext context;
        public AzureDevOpsService(DashReportViewerContext context)
        {
            this.context = context;
        }

        public async Task Release(ReleaseDeployment releaseDeployment)
        {
            if (releaseDeployment.resource.release != null)
            {
                var ado = new AzureDevOp();
                if (releaseDeployment.resource.environment != null)
                {
                    ado.EnvironmentId = releaseDeployment.resource.environment.id;
                    ado.Owner = releaseDeployment.resource.environment.owner.displayName;
                    ado.EnvironmentName = releaseDeployment.resource.environment.name;
                }

                ado.Status = releaseDeployment.resource.release.status;
                ado.Created = releaseDeployment.createdDate;
                ado.DeploymentText = releaseDeployment.message.text;
                ado.ProjectName = releaseDeployment.resource.project.name;
                ado.ReleaseName = releaseDeployment.resource.release.name;
                ado.ReleaseId = releaseDeployment.resource.release.id;

                context.AzureDevOps.Add(ado);
            }
            else
            {
                context.AzureDevOps.Add(new AzureDevOp()
                {
                    EnvironmentId = releaseDeployment.resource.environment.id,
                    Status = releaseDeployment.resource.environment.status,
                    Owner = releaseDeployment.resource.environment.owner.displayName,
                    Created = releaseDeployment.createdDate,
                    DeploymentText = releaseDeployment.message.text,
                    EnvironmentName = releaseDeployment.resource.environment.name,
                    ProjectName = releaseDeployment.resource.project.name,
                    ReleaseId = releaseDeployment.resource.environment.release.id,
                    ReleaseName = releaseDeployment.resource.environment.release.name,
                });
            }

            await context.SaveChangesAsync();
        }
    }
}