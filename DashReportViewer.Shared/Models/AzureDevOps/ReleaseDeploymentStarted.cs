using System;

namespace DashReportViewer.AzureDevOps.Models
{
    public class ReleaseDeploymentStarted
    {
        public string subscriptionId { get; set; }
        public int notificationId { get; set; }
        public string id { get; set; }
        public string eventType { get; set; }
        public string publisherId { get; set; }
        public ReleaseDeploymentStarted_Message message { get; set; }
        public ReleaseDeploymentStarted_Detailedmessage detailedMessage { get; set; }
        public ReleaseDeploymentStarted_Resource resource { get; set; }
        public string resourceVersion { get; set; }
        public ReleaseDeploymentStarted_Resourcecontainers resourceContainers { get; set; }
        public DateTime createdDate { get; set; }
    }

    public class ReleaseDeploymentStarted_Message
    {
        public string text { get; set; }
        public string html { get; set; }
        public string markdown { get; set; }
    }

    public class ReleaseDeploymentStarted_Detailedmessage
    {
        public string text { get; set; }
        public string html { get; set; }
        public string markdown { get; set; }
    }

    public class ReleaseDeploymentStarted_Resource
    {
        public ReleaseDeploymentStarted_Environment environment { get; set; }
        public ReleaseDeploymentStarted_Release1 release { get; set; }
        public ReleaseDeploymentStarted_Project project { get; set; }
        public object stageName { get; set; }
        public int attemptId { get; set; }
        public int id { get; set; }
        public object url { get; set; }
    }

    public class ReleaseDeploymentStarted_Environment
    {
        public int id { get; set; }
        public int releaseId { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public ReleaseDeploymentStarted_Variables variables { get; set; }
        public object[] variableGroups { get; set; }
        public object[] preDeployApprovals { get; set; }
        public object[] postDeployApprovals { get; set; }
        public ReleaseDeploymentStarted_Preapprovalssnapshot preApprovalsSnapshot { get; set; }
        public ReleaseDeploymentStarted_Postapprovalssnapshot postApprovalsSnapshot { get; set; }
        public object[] deploySteps { get; set; }
        public int rank { get; set; }
        public int definitionEnvironmentId { get; set; }
        public int queueId { get; set; }
        public ReleaseDeploymentStarted_Environmentoptions environmentOptions { get; set; }
        public object[] demands { get; set; }
        public object[] conditions { get; set; }
        public DateTime modifiedOn { get; set; }
        public object[] workflowTasks { get; set; }
        public object[] deployPhasesSnapshot { get; set; }
        public ReleaseDeploymentStarted_Owner owner { get; set; }
        public DateTime scheduledDeploymentTime { get; set; }
        public object[] schedules { get; set; }
        public ReleaseDeploymentStarted_Release release { get; set; }
        public ReleaseDeploymentStarted_Predeploymentgatessnapshot preDeploymentGatesSnapshot { get; set; }
        public ReleaseDeploymentStarted_Postdeploymentgatessnapshot postDeploymentGatesSnapshot { get; set; }
    }

    public class ReleaseDeploymentStarted_Variables
    {
    }

    public class ReleaseDeploymentStarted_Preapprovalssnapshot
    {
        public object[] approvals { get; set; }
        public ReleaseDeploymentStarted_Approvaloptions approvalOptions { get; set; }
    }

    public class ReleaseDeploymentStarted_Approvaloptions
    {
        public int requiredApproverCount { get; set; }
        public bool releaseCreatorCanBeApprover { get; set; }
        public bool autoTriggeredAndPreviousEnvironmentApprovedCanBeSkipped { get; set; }
        public bool enforceIdentityRevalidation { get; set; }
        public int timeoutInMinutes { get; set; }
        public string executionOrder { get; set; }
    }

    public class ReleaseDeploymentStarted_Postapprovalssnapshot
    {
        public object[] approvals { get; set; }
    }

    public class ReleaseDeploymentStarted_Environmentoptions
    {
        public string emailNotificationType { get; set; }
        public string emailRecipients { get; set; }
        public bool skipArtifactsDownload { get; set; }
        public int timeoutInMinutes { get; set; }
        public bool enableAccessToken { get; set; }
        public bool publishDeploymentStatus { get; set; }
        public bool badgeEnabled { get; set; }
        public bool autoLinkWorkItems { get; set; }
        public bool pullRequestDeploymentEnabled { get; set; }
    }

    public class ReleaseDeploymentStarted_Owner
    {
        public string displayName { get; set; }
        public string id { get; set; }
    }

    public class ReleaseDeploymentStarted_Release
    {
        public int id { get; set; }
        public string name { get; set; }
        public ReleaseDeploymentStarted__Links _links { get; set; }
    }

    public class ReleaseDeploymentStarted__Links
    {
        public ReleaseDeploymentStarted_Web web { get; set; }
    }

    public class ReleaseDeploymentStarted_Web
    {
        public string href { get; set; }
    }

    public class ReleaseDeploymentStarted_Predeploymentgatessnapshot
    {
        public int id { get; set; }
        public object gatesOptions { get; set; }
        public object[] gates { get; set; }
    }

    public class ReleaseDeploymentStarted_Postdeploymentgatessnapshot
    {
        public int id { get; set; }
        public object gatesOptions { get; set; }
        public object[] gates { get; set; }
    }

    public class ReleaseDeploymentStarted_Release1
    {
        public int id { get; set; }
        public object name { get; set; }
        public string status { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public object modifiedBy { get; set; }
        public object createdBy { get; set; }
        public object[] environments { get; set; }
        public ReleaseDeploymentStarted_Variables1 variables { get; set; }
        public object[] variableGroups { get; set; }
        public object[] artifacts { get; set; }
        public ReleaseDeploymentStarted_Releasedefinition releaseDefinition { get; set; }
        public int releaseDefinitionRevision { get; set; }
        public string reason { get; set; }
        public object releaseNameFormat { get; set; }
        public bool keepForever { get; set; }
        public int definitionSnapshotRevision { get; set; }
        public object logsContainerUrl { get; set; }
        public ReleaseDeploymentStarted__Links2 _links { get; set; }
        public object[] tags { get; set; }
        public object triggeringArtifactAlias { get; set; }
        public object projectReference { get; set; }
    }

    public class ReleaseDeploymentStarted_Variables1
    {
    }

    public class ReleaseDeploymentStarted_Releasedefinition
    {
        public int id { get; set; }
        public string name { get; set; }
        public object projectReference { get; set; }
        public ReleaseDeploymentStarted__Links1 _links { get; set; }
    }

    public class ReleaseDeploymentStarted__Links1
    {
    }

    public class ReleaseDeploymentStarted__Links2
    {
    }

    public class ReleaseDeploymentStarted_Project
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ReleaseDeploymentStarted_Resourcecontainers
    {
        public ReleaseDeploymentStarted_Collection collection { get; set; }
        public ReleaseDeploymentStarted_Account account { get; set; }
        public ReleaseDeploymentStarted_Project1 project { get; set; }
    }

    public class ReleaseDeploymentStarted_Collection
    {
        public string id { get; set; }
    }

    public class ReleaseDeploymentStarted_Account
    {
        public string id { get; set; }
    }

    public class ReleaseDeploymentStarted_Project1
    {
        public string id { get; set; }
    }
}