using System;
using Microsoft.Build.Framework;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace CustomBuildTask
{
    [TaskName("CreateService")]
    public class MyTask:Task,ITask
    {

        public IBuildEngine BuildEngine { get; set; }
        public ITaskHost HostObject { get; set; }

        #region Properties

        /// <summary>
        /// The name of the machine
        /// </summary>
        [TaskAttribute("machineName", Required = false)]
        [StringValidator(AllowEmpty = false)]
        public string MachineName { get; set; }

        /// <summary>
        /// The name of the service
        /// </summary>
        [Required]
        [TaskAttribute("name", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string ServiceName { get; set; }

        /// <summary>
        /// The display name of the service - what is seen in service management
        /// </summary>
        [Required]
        [TaskAttribute("displayName", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string ServiceDisplayName { get; set; }

        ///<summary>
        /// The location of the service
        ///</summary>
        [Required]
        [TaskAttribute("physicalLocation", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string PhysicalLocation { get; set; }

        ///<summary>
        /// The start mode of the service - Automatic, Manual, Boot, System, or Disabled
        ///</summary>
        /// <remarks>If something is incorrect, Automatic is chosen</remarks>
        [Required]
        [TaskAttribute("startMode", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string StartMode { get; set; }

        /// <summary>
        /// The user name for the service to run under - if using LocalSystem or NT AUTHORITY\NetworkService, leave password blank
        /// </summary>
        [Required]
        [TaskAttribute("userName", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string UserName { get; set; }

        /// <summary>
        /// Password for the account to run under
        /// </summary>
        [TaskAttribute("password", Required = false)]
        [StringValidator(AllowEmpty = true)]
        public string Password { get; set; }

        /// <summary>
        /// A comma separated string of services that this service will depend on. Service names. Not the service display names.
        /// </summary>
        [TaskAttribute("dependencies", Required = false)]
        [StringValidator(AllowEmpty = true)]
        public string Dependencies { get; set; }

        #endregion

        
        bool ITask.Execute()
        {
            bool returnCode = CreateTheService();
            MessageImportance importance = returnCode ? MessageImportance.Normal : MessageImportance.High;
            string message = "CreateService returned:  " + returnCode.ToString();
            BuildEngine.LogMessageEvent(new BuildMessageEventArgs(message, String.Empty, "CreateServiceTask", importance));

            return returnCode;

        }

        protected override void ExecuteTask()
        {
            Project.Log(Level.Info, string.Format("Calling Create Service to install {0} with physical location {1} on {2} with user {3} and startup mode {4}.", ServiceName, PhysicalLocation, MachineName ?? Environment.MachineName, UserName, StartMode));
            bool returnCode = CreateTheService();
            if (!returnCode)
            {
                Project.Log(Level.Error, string.Format("Create Service was unsuccessful! It returned code {0}.", 0));
            }
            else
            {
                Project.Log(Level.Info, string.Format("Service named {0} was installed successfully on {1}.", ServiceDisplayName, MachineName ?? Environment.MachineName));
            }

        }

        /// <summary>
        /// Creates a windows service through WMI
        /// </summary>
        /// <returns>true if the task is successful</returns>
        public bool CreateTheService()
        {
            return true;
        }


    }
}
