using System;

namespace DashReportViewer.Shared.Models.AzureDevOps
{
    public class ReleaseDeployment
    {
        public string subscriptionId { get; set; }
        public int notificationId { get; set; }
        public string id { get; set; }
        //public string eventType { get; set; }
        //public string publisherId { get; set; }
        public ReleaseDeployment_Message message { get; set; }
        //public ReleaseDeployment_Detailedmessage detailedMessage { get; set; }
        public ReleaseDeployment_Resource resource { get; set; }
        //public string resourceVersion { get; set; }
        //public ReleaseDeployment_Resourcecontainers resourceContainers { get; set; }
        public DateTime createdDate { get; set; }
    }

    public class ReleaseDeployment_Message
    {
        public string text { get; set; }
        public string html { get; set; }
        public string markdown { get; set; }
    }

    public class ReleaseDeployment_Detailedmessage
    {
        public string text { get; set; }
        public string html { get; set; }
        public string markdown { get; set; }
    }

    public class ReleaseDeployment_Resource
    {
        public ReleaseDeployment_Environment environment { get; set; }
        public ReleaseDeployment_Release2 release { get; set; }
        public ReleaseDeployment_Project project { get; set; }
        public string stageName { get; set; }
        public int attemptId { get; set; }
        public int id { get; set; }
        public string url { get; set; }
    }

    public class ReleaseDeployment_Environment
    {
        public int id { get; set; }
        public int releaseId { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public ReleaseDeployment_Variables variables { get; set; }
        public object[] variableGroups { get; set; }
        public ReleaseDeployment_Predeployapproval[] preDeployApprovals { get; set; }
        public object[] postDeployApprovals { get; set; }
        public ReleaseDeployment_Preapprovalssnapshot preApprovalsSnapshot { get; set; }
        public ReleaseDeployment_Postapprovalssnapshot postApprovalsSnapshot { get; set; }
        public ReleaseDeployment_Deploystep[] deploySteps { get; set; }
        public int rank { get; set; }
        public int definitionEnvironmentId { get; set; }
        public ReleaseDeployment_Environmentoptions environmentOptions { get; set; }
        public object[] demands { get; set; }
        public ReleaseDeployment_Condition[] conditions { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public object[] workflowTasks { get; set; }
        public ReleaseDeployment_Deployphasessnapshot[] deployPhasesSnapshot { get; set; }
        public ReleaseDeployment_Owner owner { get; set; }
        public object[] schedules { get; set; }
        public ReleaseDeployment_Release release { get; set; }
        public ReleaseDeployment_Releasedefinition releaseDefinition { get; set; }
        public ReleaseDeployment_Releasecreatedby releaseCreatedBy { get; set; }
        public string triggerReason { get; set; }
        public ReleaseDeployment_Processparameters processParameters { get; set; }
        public ReleaseDeployment_Predeploymentgatessnapshot preDeploymentGatesSnapshot { get; set; }
        public ReleaseDeployment_Postdeploymentgatessnapshot postDeploymentGatesSnapshot { get; set; }
    }

    public class ReleaseDeployment_Variables
    {
    }

    public class ReleaseDeployment_Preapprovalssnapshot
    {
        public ReleaseDeployment_Approval[] approvals { get; set; }
        public ReleaseDeployment_Approvaloptions approvalOptions { get; set; }
    }

    public class ReleaseDeployment_Approvaloptions
    {
        public object requiredApproverCount { get; set; }
        public bool releaseCreatorCanBeApprover { get; set; }
        public bool autoTriggeredAndPreviousEnvironmentApprovedCanBeSkipped { get; set; }
        public bool enforceIdentityRevalidation { get; set; }
        public int timeoutInMinutes { get; set; }
        public string executionOrder { get; set; }
    }

    public class ReleaseDeployment_Approval
    {
        public int rank { get; set; }
        public bool isAutomated { get; set; }
        public bool isNotificationOn { get; set; }
        public int id { get; set; }
    }

    public class ReleaseDeployment_Postapprovalssnapshot
    {
        public ReleaseDeployment_Approval1[] approvals { get; set; }
        public ReleaseDeployment_Approvaloptions1 approvalOptions { get; set; }
    }

    public class ReleaseDeployment_Approvaloptions1
    {
        public object requiredApproverCount { get; set; }
        public bool releaseCreatorCanBeApprover { get; set; }
        public bool autoTriggeredAndPreviousEnvironmentApprovedCanBeSkipped { get; set; }
        public bool enforceIdentityRevalidation { get; set; }
        public int timeoutInMinutes { get; set; }
        public string executionOrder { get; set; }
    }

    public class ReleaseDeployment_Approval1
    {
        public int rank { get; set; }
        public bool isAutomated { get; set; }
        public bool isNotificationOn { get; set; }
        public int id { get; set; }
    }

    public class ReleaseDeployment_Environmentoptions
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

    public class ReleaseDeployment_Owner
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class ReleaseDeployment__Links
    {
        public ReleaseDeployment_Avatar avatar { get; set; }
    }

    public class ReleaseDeployment_Avatar
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Release
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links1 _links { get; set; }
    }

    public class ReleaseDeployment__Links1
    {
        public ReleaseDeployment_Web web { get; set; }
        public ReleaseDeployment_Self self { get; set; }
    }

    public class ReleaseDeployment_Web
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Self
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Releasedefinition
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public object projectReference { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links2 _links { get; set; }
    }

    public class ReleaseDeployment__Links2
    {
        public ReleaseDeployment_Web1 web { get; set; }
        public ReleaseDeployment_Self1 self { get; set; }
    }

    public class ReleaseDeployment_Web1
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Self1
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Releasecreatedby
    {
        public string displayName { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string descriptor { get; set; }
    }

    public class ReleaseDeployment_Processparameters
    {
        public ReleaseDeployment_Datasourcebinding[] dataSourceBindings { get; set; }
    }

    public class ReleaseDeployment_Datasourcebinding
    {
        public string dataSourceName { get; set; }
        public ReleaseDeployment_Parameters parameters { get; set; }
        public string endpointId { get; set; }
        public string target { get; set; }
    }

    public class ReleaseDeployment_Parameters
    {
        public string WebAppKind { get; set; }
    }

    public class ReleaseDeployment_Predeploymentgatessnapshot
    {
        public int id { get; set; }
        public ReleaseDeployment_Gatesoptions gatesOptions { get; set; }
        public object[] gates { get; set; }
    }

    public class ReleaseDeployment_Gatesoptions
    {
        public bool isEnabled { get; set; }
        public int timeout { get; set; }
        public int samplingInterval { get; set; }
        public int stabilizationTime { get; set; }
        public int minimumSuccessDuration { get; set; }
    }

    public class ReleaseDeployment_Postdeploymentgatessnapshot
    {
        public int id { get; set; }
        public object gatesOptions { get; set; }
        public object[] gates { get; set; }
    }

    public class ReleaseDeployment_Predeployapproval
    {
        public int id { get; set; }
        public int revision { get; set; }
        public string approvalType { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public string status { get; set; }
        public string comments { get; set; }
        public bool isAutomated { get; set; }
        public bool isNotificationOn { get; set; }
        public int trialNumber { get; set; }
        public int attempt { get; set; }
        public int rank { get; set; }
        public ReleaseDeployment_Release1 release { get; set; }
        public ReleaseDeployment_Releasedefinition1 releaseDefinition { get; set; }
        public ReleaseDeployment_Releaseenvironment releaseEnvironment { get; set; }
        public string url { get; set; }
    }

    public class ReleaseDeployment_Release1
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links3 _links { get; set; }
    }

    public class ReleaseDeployment__Links3
    {
    }

    public class ReleaseDeployment_Releasedefinition1
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public object projectReference { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links4 _links { get; set; }
    }

    public class ReleaseDeployment__Links4
    {
    }

    public class ReleaseDeployment_Releaseenvironment
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links5 _links { get; set; }
    }

    public class ReleaseDeployment__Links5
    {
    }

    public class ReleaseDeployment_Deploystep
    {
        public int id { get; set; }
        public int deploymentId { get; set; }
        public int attempt { get; set; }
        public string reason { get; set; }
        public string status { get; set; }
        public string operationStatus { get; set; }
        public ReleaseDeployment_Releasedeployphas[] releaseDeployPhases { get; set; }
        public ReleaseDeployment_Requestedby requestedBy { get; set; }
        public ReleaseDeployment_Requestedfor requestedFor { get; set; }
        public DateTime queuedOn { get; set; }
        public ReleaseDeployment_Lastmodifiedby lastModifiedBy { get; set; }
        public DateTime lastModifiedOn { get; set; }
        public bool hasStarted { get; set; }
        public object[] tasks { get; set; }
        public string runPlanId { get; set; }
        public object[] issues { get; set; }
    }

    public class ReleaseDeployment_Requestedby
    {
        public string displayName { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string descriptor { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links6 _links { get; set; }
        public string imageUrl { get; set; }
    }

    public class ReleaseDeployment__Links6
    {
        public ReleaseDeployment_Avatar1 avatar { get; set; }
    }

    public class ReleaseDeployment_Avatar1
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Requestedfor
    {
        public string displayName { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string descriptor { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links7 _links { get; set; }
        public string imageUrl { get; set; }
    }

    public class ReleaseDeployment__Links7
    {
        public ReleaseDeployment_Avatar2 avatar { get; set; }
    }

    public class ReleaseDeployment_Avatar2
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Lastmodifiedby
    {
        public string displayName { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string descriptor { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links8 _links { get; set; }
        public string imageUrl { get; set; }
    }

    public class ReleaseDeployment__Links8
    {
        public ReleaseDeployment_Avatar3 avatar { get; set; }
    }

    public class ReleaseDeployment_Avatar3
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Releasedeployphas
    {
        public int id { get; set; }
        public string phaseId { get; set; }
        public string name { get; set; }
        public int rank { get; set; }
        public string phaseType { get; set; }
        public string status { get; set; }
        public string runPlanId { get; set; }
        public ReleaseDeployment_Deploymentjob[] deploymentJobs { get; set; }
        public object[] manualInterventions { get; set; }
        public DateTime startedOn { get; set; }
    }

    public class ReleaseDeployment_Deploymentjob
    {
        public ReleaseDeployment_Job job { get; set; }
        public ReleaseDeployment_Task[] tasks { get; set; }
    }

    public class ReleaseDeployment_Job
    {
        public int id { get; set; }
        public string timelineRecordId { get; set; }
        public string name { get; set; }
        public DateTime dateStarted { get; set; }
        public DateTime dateEnded { get; set; }
        public DateTime startTime { get; set; }
        public DateTime finishTime { get; set; }
        public string status { get; set; }
        public int rank { get; set; }
        public object[] issues { get; set; }
        public string agentName { get; set; }
        public string logUrl { get; set; }
    }

    public class ReleaseDeployment_Task
    {
        public int id { get; set; }
        public string timelineRecordId { get; set; }
        public string name { get; set; }
        public DateTime dateStarted { get; set; }
        public DateTime dateEnded { get; set; }
        public DateTime startTime { get; set; }
        public DateTime finishTime { get; set; }
        public string status { get; set; }
        public int rank { get; set; }
        public ReleaseDeployment_Issue[] issues { get; set; }
        public string agentName { get; set; }
        public string logUrl { get; set; }
        public ReleaseDeployment_Task1 task { get; set; }
        public int percentComplete { get; set; }
        public string resultCode { get; set; }
    }

    public class ReleaseDeployment_Task1
    {
        public string id { get; set; }
        public string name { get; set; }
        public string version { get; set; }
    }

    public class ReleaseDeployment_Issue
    {
        public string issueType { get; set; }
        public string message { get; set; }
        public ReleaseDeployment_Data data { get; set; }
    }

    public class ReleaseDeployment_Data
    {
        public string type { get; set; }
        public string logFileLineNumber { get; set; }
        public string AgentVersion { get; set; }
        public string code { get; set; }
        public string TaskId { get; set; }
    }

    public class ReleaseDeployment_Condition
    {
        public bool result { get; set; }
        public string name { get; set; }
        public string conditionType { get; set; }
        public string value { get; set; }
    }

    public class ReleaseDeployment_Deployphasessnapshot
    {
        public ReleaseDeployment_Deploymentinput deploymentInput { get; set; }
        public int rank { get; set; }
        public string phaseType { get; set; }
        public string name { get; set; }
        public object refName { get; set; }
        public ReleaseDeployment_Workflowtask[] workflowTasks { get; set; }
    }

    public class ReleaseDeployment_Deploymentinput
    {
        public ReleaseDeployment_Parallelexecution parallelExecution { get; set; }
        public object agentSpecification { get; set; }
        public bool skipArtifactsDownload { get; set; }
        public ReleaseDeployment_Artifactsdownloadinput artifactsDownloadInput { get; set; }
        public int queueId { get; set; }
        public object[] demands { get; set; }
        public bool enableAccessToken { get; set; }
        public int timeoutInMinutes { get; set; }
        public int jobCancelTimeoutInMinutes { get; set; }
        public string condition { get; set; }
        public ReleaseDeployment_Overrideinputs overrideInputs { get; set; }
    }

    public class ReleaseDeployment_Parallelexecution
    {
        public string parallelExecutionType { get; set; }
    }

    public class ReleaseDeployment_Artifactsdownloadinput
    {
        public object[] downloadInputs { get; set; }
    }

    public class ReleaseDeployment_Overrideinputs
    {
    }

    public class ReleaseDeployment_Workflowtask
    {
        public ReleaseDeployment_Environment1 environment { get; set; }
        public string taskId { get; set; }
        public string version { get; set; }
        public string name { get; set; }
        public string refName { get; set; }
        public bool enabled { get; set; }
        public bool alwaysRun { get; set; }
        public bool continueOnError { get; set; }
        public int timeoutInMinutes { get; set; }
        public string definitionType { get; set; }
        public ReleaseDeployment_Overrideinputs1 overrideInputs { get; set; }
        public string condition { get; set; }
        public ReleaseDeployment_Inputs inputs { get; set; }
    }

    public class ReleaseDeployment_Environment1
    {
    }

    public class ReleaseDeployment_Overrideinputs1
    {
    }

    public class ReleaseDeployment_Inputs
    {
        public string script { get; set; }
        public string workingDirectory { get; set; }
        public string failOnStderr { get; set; }
        public string ConnectionType { get; set; }
        public string ConnectedServiceName { get; set; }
        public string PublishProfilePath { get; set; }
        public string PublishProfilePassword { get; set; }
        public string WebAppKind { get; set; }
        public string WebAppName { get; set; }
        public string DeployToSlotOrASEFlag { get; set; }
        public string ResourceGroupName { get; set; }
        public string SlotName { get; set; }
        public string DockerNamespace { get; set; }
        public string DockerRepository { get; set; }
        public string DockerImageTag { get; set; }
        public string VirtualApplication { get; set; }
        public string Package { get; set; }
        public string RuntimeStack { get; set; }
        public string RuntimeStackFunction { get; set; }
        public string StartupCommand { get; set; }
        public string ScriptType { get; set; }
        public string InlineScript { get; set; }
        public string ScriptPath { get; set; }
        public string WebConfigParameters { get; set; }
        public string AppSettings { get; set; }
        public string ConfigurationSettings { get; set; }
        public string UseWebDeploy { get; set; }
        public string DeploymentType { get; set; }
        public string TakeAppOfflineFlag { get; set; }
        public string SetParametersFile { get; set; }
        public string RemoveAdditionalFilesFlag { get; set; }
        public string ExcludeFilesFromAppDataFlag { get; set; }
        public string AdditionalArguments { get; set; }
        public string RenameFilesFlag { get; set; }
        public string XmlTransformation { get; set; }
        public string XmlVariableSubstitution { get; set; }
        public string JSONFiles { get; set; }
    }

    public class ReleaseDeployment_Release2
    {
        public int id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        //public ReleaseDeployment_Modifiedby modifiedBy { get; set; }
        //public ReleaseDeployment_Createdby createdBy { get; set; }
        //public ReleaseDeployment_Createdfor createdFor { get; set; }
        //public ReleaseDeployment_Environment2[] environments { get; set; }
        //public ReleaseDeployment_Variables1 variables { get; set; }
        //public object[] variableGroups { get; set; }
        //public ReleaseDeployment_Artifact[] artifacts { get; set; }
        //public ReleaseDeployment_Releasedefinition2 releaseDefinition { get; set; }
        //public int releaseDefinitionRevision { get; set; }
        //public string description { get; set; }
        //public string reason { get; set; }
        //public string releaseNameFormat { get; set; }
        //public bool keepForever { get; set; }
        //public int definitionSnapshotRevision { get; set; }
        //public string logsContainerUrl { get; set; }
        //public string url { get; set; }
        //public ReleaseDeployment__Links10 _links { get; set; }
        //public object[] tags { get; set; }
        //public object triggeringArtifactAlias { get; set; }
        //public ReleaseDeployment_Projectreference projectReference { get; set; }
        //public ReleaseDeployment_Properties properties { get; set; }
    }

    public class ReleaseDeployment_Modifiedby
    {
        public string displayName { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string descriptor { get; set; }
    }

    public class ReleaseDeployment_Createdby
    {
        public string displayName { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string descriptor { get; set; }
    }

    public class ReleaseDeployment_Createdfor
    {
        public string displayName { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string descriptor { get; set; }
    }

    public class ReleaseDeployment_Variables1
    {
    }

    public class ReleaseDeployment_Releasedefinition2
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public object projectReference { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links9 _links { get; set; }
    }

    public class ReleaseDeployment__Links9
    {
        public ReleaseDeployment_Self2 self { get; set; }
        public ReleaseDeployment_Web2 web { get; set; }
    }

    public class ReleaseDeployment_Self2
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Web2
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment__Links10
    {
        public ReleaseDeployment_Self3 self { get; set; }
        public ReleaseDeployment_Web3 web { get; set; }
    }

    public class ReleaseDeployment_Self3
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Web3
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Projectreference
    {
        public string id { get; set; }
        public object name { get; set; }
    }

    public class ReleaseDeployment_Properties
    {
    }

    public class ReleaseDeployment_Environment2
    {
        public int id { get; set; }
        public int releaseId { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public ReleaseDeployment_Variables2 variables { get; set; }
        public object[] variableGroups { get; set; }
        public ReleaseDeployment_Predeployapproval1[] preDeployApprovals { get; set; }
        public object[] postDeployApprovals { get; set; }
        public ReleaseDeployment_Preapprovalssnapshot1 preApprovalsSnapshot { get; set; }
        public ReleaseDeployment_Postapprovalssnapshot1 postApprovalsSnapshot { get; set; }
        public ReleaseDeployment_Deploystep1[] deploySteps { get; set; }
        public int rank { get; set; }
        public int definitionEnvironmentId { get; set; }
        public ReleaseDeployment_Environmentoptions1 environmentOptions { get; set; }
        public object[] demands { get; set; }
        public ReleaseDeployment_Condition1[] conditions { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public object[] workflowTasks { get; set; }
        public ReleaseDeployment_Deployphasessnapshot1[] deployPhasesSnapshot { get; set; }
        public ReleaseDeployment_Owner1 owner { get; set; }
        public object[] schedules { get; set; }
        public ReleaseDeployment_Release3 release { get; set; }
        public ReleaseDeployment_Releasedefinition3 releaseDefinition { get; set; }
        public ReleaseDeployment_Releasecreatedby1 releaseCreatedBy { get; set; }
        public string triggerReason { get; set; }
        public ReleaseDeployment_Processparameters1 processParameters { get; set; }
        public ReleaseDeployment_Predeploymentgatessnapshot1 preDeploymentGatesSnapshot { get; set; }
        public ReleaseDeployment_Postdeploymentgatessnapshot1 postDeploymentGatesSnapshot { get; set; }
    }

    public class ReleaseDeployment_Variables2
    {
    }

    public class ReleaseDeployment_Preapprovalssnapshot1
    {
        public object[] approvals { get; set; }
    }

    public class ReleaseDeployment_Postapprovalssnapshot1
    {
        public object[] approvals { get; set; }
    }

    public class ReleaseDeployment_Environmentoptions1
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

    public class ReleaseDeployment_Owner1
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links11 _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class ReleaseDeployment__Links11
    {
        public ReleaseDeployment_Avatar4 avatar { get; set; }
    }

    public class ReleaseDeployment_Avatar4
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Release3
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links12 _links { get; set; }
    }

    public class ReleaseDeployment__Links12
    {
        public ReleaseDeployment_Web4 web { get; set; }
        public ReleaseDeployment_Self4 self { get; set; }
    }

    public class ReleaseDeployment_Web4
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Self4
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Releasedefinition3
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public object projectReference { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links13 _links { get; set; }
    }

    public class ReleaseDeployment__Links13
    {
        public ReleaseDeployment_Web5 web { get; set; }
        public ReleaseDeployment_Self5 self { get; set; }
    }

    public class ReleaseDeployment_Web5
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Self5
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Releasecreatedby1
    {
        public string displayName { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string descriptor { get; set; }
    }

    public class ReleaseDeployment_Processparameters1
    {
        public ReleaseDeployment_Datasourcebinding1[] dataSourceBindings { get; set; }
    }

    public class ReleaseDeployment_Datasourcebinding1
    {
        public string dataSourceName { get; set; }
        public ReleaseDeployment_Parameters1 parameters { get; set; }
        public string endpointId { get; set; }
        public string target { get; set; }
    }

    public class ReleaseDeployment_Parameters1
    {
        public string WebAppKind { get; set; }
    }

    public class ReleaseDeployment_Predeploymentgatessnapshot1
    {
        public int id { get; set; }
        public ReleaseDeployment_Gatesoptions1 gatesOptions { get; set; }
        public object[] gates { get; set; }
    }

    public class ReleaseDeployment_Gatesoptions1
    {
        public bool isEnabled { get; set; }
        public int timeout { get; set; }
        public int samplingInterval { get; set; }
        public int stabilizationTime { get; set; }
        public int minimumSuccessDuration { get; set; }
    }

    public class ReleaseDeployment_Postdeploymentgatessnapshot1
    {
        public int id { get; set; }
        public object gatesOptions { get; set; }
        public object[] gates { get; set; }
    }

    public class ReleaseDeployment_Predeployapproval1
    {
        public int id { get; set; }
        public int revision { get; set; }
        public string approvalType { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public string status { get; set; }
        public string comments { get; set; }
        public bool isAutomated { get; set; }
        public bool isNotificationOn { get; set; }
        public int trialNumber { get; set; }
        public int attempt { get; set; }
        public int rank { get; set; }
        public ReleaseDeployment_Release4 release { get; set; }
        public ReleaseDeployment_Releasedefinition4 releaseDefinition { get; set; }
        public ReleaseDeployment_Releaseenvironment1 releaseEnvironment { get; set; }
        public string url { get; set; }
    }

    public class ReleaseDeployment_Release4
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links14 _links { get; set; }
    }

    public class ReleaseDeployment__Links14
    {
    }

    public class ReleaseDeployment_Releasedefinition4
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public object projectReference { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links15 _links { get; set; }
    }

    public class ReleaseDeployment__Links15
    {
    }

    public class ReleaseDeployment_Releaseenvironment1
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links16 _links { get; set; }
    }

    public class ReleaseDeployment__Links16
    {
    }

    public class ReleaseDeployment_Deploystep1
    {
        public int id { get; set; }
        public int deploymentId { get; set; }
        public int attempt { get; set; }
        public string reason { get; set; }
        public string status { get; set; }
        public string operationStatus { get; set; }
        public ReleaseDeployment_Releasedeployphas1[] releaseDeployPhases { get; set; }
        public ReleaseDeployment_Requestedby1 requestedBy { get; set; }
        public ReleaseDeployment_Requestedfor1 requestedFor { get; set; }
        public DateTime queuedOn { get; set; }
        public ReleaseDeployment_Lastmodifiedby1 lastModifiedBy { get; set; }
        public DateTime lastModifiedOn { get; set; }
        public bool hasStarted { get; set; }
        public object[] tasks { get; set; }
        public string runPlanId { get; set; }
        public object[] issues { get; set; }
    }

    public class ReleaseDeployment_Requestedby1
    {
        public string displayName { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string descriptor { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links17 _links { get; set; }
        public string imageUrl { get; set; }
    }

    public class ReleaseDeployment__Links17
    {
        public ReleaseDeployment_Avatar5 avatar { get; set; }
    }

    public class ReleaseDeployment_Avatar5
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Requestedfor1
    {
        public string displayName { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string descriptor { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links18 _links { get; set; }
        public string imageUrl { get; set; }
    }

    public class ReleaseDeployment__Links18
    {
        public ReleaseDeployment_Avatar6 avatar { get; set; }
    }

    public class ReleaseDeployment_Avatar6
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Lastmodifiedby1
    {
        public string displayName { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string descriptor { get; set; }
        public string url { get; set; }
        public ReleaseDeployment__Links19 _links { get; set; }
        public string imageUrl { get; set; }
    }

    public class ReleaseDeployment__Links19
    {
        public ReleaseDeployment_Avatar7 avatar { get; set; }
    }

    public class ReleaseDeployment_Avatar7
    {
        public string href { get; set; }
    }

    public class ReleaseDeployment_Releasedeployphas1
    {
        public int id { get; set; }
        public string phaseId { get; set; }
        public string name { get; set; }
        public int rank { get; set; }
        public string phaseType { get; set; }
        public string status { get; set; }
        public object runPlanId { get; set; }
        public object[] deploymentJobs { get; set; }
        public object[] manualInterventions { get; set; }
        public DateTime startedOn { get; set; }
    }

    public class ReleaseDeployment_Condition1
    {
        public bool result { get; set; }
        public string name { get; set; }
        public string conditionType { get; set; }
        public string value { get; set; }
    }

    public class ReleaseDeployment_Deployphasessnapshot1
    {
        public ReleaseDeployment_Deploymentinput1 deploymentInput { get; set; }
        public int rank { get; set; }
        public string phaseType { get; set; }
        public string name { get; set; }
        public object refName { get; set; }
        public object[] workflowTasks { get; set; }
    }

    public class ReleaseDeployment_Deploymentinput1
    {
        public ReleaseDeployment_Parallelexecution1 parallelExecution { get; set; }
        public object agentSpecification { get; set; }
        public bool skipArtifactsDownload { get; set; }
        public ReleaseDeployment_Artifactsdownloadinput1 artifactsDownloadInput { get; set; }
        public int queueId { get; set; }
        public object[] demands { get; set; }
        public bool enableAccessToken { get; set; }
        public int timeoutInMinutes { get; set; }
        public int jobCancelTimeoutInMinutes { get; set; }
        public string condition { get; set; }
        public ReleaseDeployment_Overrideinputs2 overrideInputs { get; set; }
    }

    public class ReleaseDeployment_Parallelexecution1
    {
        public string parallelExecutionType { get; set; }
    }

    public class ReleaseDeployment_Artifactsdownloadinput1
    {
        public object[] downloadInputs { get; set; }
    }

    public class ReleaseDeployment_Overrideinputs2
    {
    }

    public class ReleaseDeployment_Artifact
    {
        public string sourceId { get; set; }
        public string type { get; set; }
        public string alias { get; set; }
        public ReleaseDeployment_Definitionreference definitionReference { get; set; }
        public bool isPrimary { get; set; }
        public bool isRetained { get; set; }
    }

    public class ReleaseDeployment_Definitionreference
    {
        public ReleaseDeployment_Artifactsourcedefinitionurl artifactSourceDefinitionUrl { get; set; }
        public ReleaseDeployment_Branch branch { get; set; }
        public ReleaseDeployment_Branches branches { get; set; }
        public ReleaseDeployment_Checkoutnestedsubmodules checkoutNestedSubmodules { get; set; }
        public ReleaseDeployment_Checkoutsubmodules checkoutSubmodules { get; set; }
        public ReleaseDeployment_Connection connection { get; set; }
        public ReleaseDeployment_Definition definition { get; set; }
        public ReleaseDeployment_Fetchdepth fetchDepth { get; set; }
        public ReleaseDeployment_Gitlfssupport gitLfsSupport { get; set; }
        public ReleaseDeployment_Istriggeringartifact IsTriggeringArtifact { get; set; }
        public ReleaseDeployment_Version version { get; set; }
        public ReleaseDeployment_Artifactsourceversionurl artifactSourceVersionUrl { get; set; }
    }

    public class ReleaseDeployment_Artifactsourcedefinitionurl
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ReleaseDeployment_Branch
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ReleaseDeployment_Branches
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ReleaseDeployment_Checkoutnestedsubmodules
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ReleaseDeployment_Checkoutsubmodules
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ReleaseDeployment_Connection
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ReleaseDeployment_Definition
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ReleaseDeployment_Fetchdepth
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ReleaseDeployment_Gitlfssupport
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ReleaseDeployment_Istriggeringartifact
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ReleaseDeployment_Version
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ReleaseDeployment_Artifactsourceversionurl
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ReleaseDeployment_Project
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ReleaseDeployment_Resourcecontainers
    {
        public ReleaseDeployment_Collection collection { get; set; }
        public ReleaseDeployment_Account account { get; set; }
        public ReleaseDeployment_Project1 project { get; set; }
    }

    public class ReleaseDeployment_Collection
    {
        public string id { get; set; }
        public string baseUrl { get; set; }
    }

    public class ReleaseDeployment_Account
    {
        public string id { get; set; }
        public string baseUrl { get; set; }
    }

    public class ReleaseDeployment_Project1
    {
        public string id { get; set; }
        public string baseUrl { get; set; }
    }

}
