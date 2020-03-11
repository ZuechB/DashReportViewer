using System;

namespace DashReportViewer.AzureDevOps.Models
{
    public class AzureDevOp
    {
        public long Id { get; set; }
        public long ReleaseId { get; set; }
        public long EnvironmentId { get; set; }
        public string Status { get; set; }
        public string Owner { get; set; }
        public string ReleaseName { get; set; }
        public string DeploymentText { get; set; }
        public string ProjectName { get; set; }
        public string EnvironmentName { get; set; }
        public DateTime Created { get; set; }
        //public AzureDevOpsActionType actionType { get; set; }
    }
}