//using System;
//using System.Collections.Generic;
//using System.Text;
//using NAnt.Core;
//using NAnt.Core.Attributes;
//using Microsoft.TeamFoundation.Client;
//using Microsoft.TeamFoundation.VersionControl.Client;
//using System.Collections;
//using System.Net;

//namespace TFS
//{
//    [TaskName("tfsget")]
//    public class TFSGet : Task
//    {
//        #region Task Attributes

//        private string _server;
//        [TaskAttribute("server", Required = true)]
//        [StringValidator(AllowEmpty = false)]
//        public string Server
//        {
//            get { return _server; }
//            set { _server = value; }
//        }

//        private bool _ssl = false;
//        [TaskAttribute("ssl", Required = false)]
//        [StringValidator(AllowEmpty = false)]
//        public string SSL
//        {
//            get { return _ssl.ToString(); }
//            set { _ssl = value.Equals("true"); }
//        }

//        private int _port;
//        [TaskAttribute("port", Required = true)]
//        [StringValidator(AllowEmpty = false)]
//        public string Port
//        {
//            get { return _port.ToString(); }
//            set { _port = Convert.ToInt32(value); }
//        }

//        private string _projectPath;
//        [TaskAttribute("projectPath", Required = true)]
//        [StringValidator(AllowEmpty = false)]
//        public string ProjectPath
//        {
//            get { return _projectPath; }
//            set { _projectPath = value; }
//        }

//        private string _localPath = ".";
//        [TaskAttribute("localPath", Required = false)]
//        [StringValidator(AllowEmpty = false)]
//        public string LocalPath
//        {
//            get { return _localPath; }
//            set { _localPath = value; }
//        }

//        private string _workspace = string.Empty;
//        [TaskAttribute("workspace", Required = false)]
//        [StringValidator(AllowEmpty = false)]
//        public string WorkspaceName
//        {
//            get 
//            { 
//                if (_workspace.Equals(string.Empty))
//                    return DateTime.Now.Ticks.ToString();

//                return _workspace; 
//            }
//            set { _workspace = value; }
//        }

//        private bool _deleteWorkspace = true;
//        [TaskAttribute("deleteWorkspace", Required = false)]
//        [BooleanValidator()]
//        public bool DeleteWorkspace
//        {
//            get { return _deleteWorkspace; }
//            set { _deleteWorkspace = value; }
//        }

//        private RecursionType _recursion = RecursionType.Full;
//        [TaskAttribute("recursionType", Required = false)]
//        [StringValidator(AllowEmpty = false)]
//        public string Recursion
//        {
//            get
//            {
//                switch (_recursion)
//                {
//                    case RecursionType.None: return "None";
//                    case RecursionType.OneLevel: return "OneLevel";
//                    case RecursionType.Full:
//                    default: return "Full";
//                }
//            }
//            set
//            {
//                switch (value.ToLower())
//                {
//                    case "none": _recursion = RecursionType.None; break;
//                    case "onelevel": _recursion = RecursionType.OneLevel; break;
//                    case "full":
//                    default: _recursion = RecursionType.Full; break;
//                }
//            }
//        }

//        private string _changeSet;
//        [TaskAttribute("changeset", Required = false)]
//        [StringValidator(AllowEmpty = false)]
//        public string changeSet
//        {
//            get { return _changeSet; }
//            set { _changeSet = value; }
//        }

//        private string _username;
//        [TaskAttribute("username", Required = false)]
//        [StringValidator(AllowEmpty = false)]
//        public string Username
//        {
//            get { return _username; }
//            set { _username = value; }
//        }

//        private string _password;
//        [TaskAttribute("password", Required = false)]
//        [StringValidator(AllowEmpty = false)]
//        public string Password
//        {
//            get { return _password; }
//            set { _password = value; }
//        }

//        private NetworkCredential Credentials
//        {
//            get
//            {
//                if (Username != null && Password != null)
//                    return new NetworkCredential(Username, Password);

//                return CredentialCache.DefaultNetworkCredentials;
//            }
//        }

//        private GetOptions _options = GetOptions.GetAll | GetOptions.Overwrite;
//        [TaskAttribute("getOptions", Required = false)]
//        [StringValidator(AllowEmpty = false)]
//        public string Options
//        {
//            get
//            {
//                switch (_options)
//                {
//                    case GetOptions.None: return "None";
//                    case GetOptions.Overwrite: return "Overwrite";
//                    case GetOptions.Preview: return "Preview";
//                    case GetOptions.GetAll: return "GetAll";
//                    case GetOptions.GetAll | GetOptions.Overwrite:
//                    default: return "Force";
//                }
//            }
//            set
//            {
//                switch (value.ToLower())
//                {
//                    case "none": _options = GetOptions.None; break;
//                    case "overwrite": _options = GetOptions.Overwrite; break;
//                    case "preview": _options = GetOptions.Preview; break;
//                    case "getall": _options = GetOptions.GetAll; break;
//                    case "force":
//                    default: _options = GetOptions.GetAll | GetOptions.Overwrite; break;
//                }
//            }
//        }

//        #endregion

//        #region Nested Elements

//        private ArrayList _sources = new ArrayList();
//        [BuildElementArray("source")]
//        public Source[] Sources
//        {
//            set
//            {
//                foreach (Source s in value)
//                {
//                    _sources.Add(s);
//                }
//            }
//        }

//        /// <summary>
//        /// SourceItems will parse the many source attributes and their containing paths and return a single-dimension
//        /// array of all filenames requested
//        /// </summary>
//        public string[] SourceItems
//        {
//            get
//            {
//                List<String> sources = new List<String>();
//                foreach (Source s in _sources)
//                    foreach (string path in s.Paths)
//                        sources.Add(path);
                
//                return sources.ToArray();
//            }
//        }

//        /// <summary>
//        /// The Source element should contain one attribute: path
//        /// Path may be any valid, semicolon-separated source pattern, including wildcards
//        /// </summary>
//        public class Source : Element
//        {
//            private string _path;
//            [TaskAttribute("path", Required = true)]
//            [StringValidator(AllowEmpty = false)]
//            public virtual string Path
//            {
//                set { _path = value; }
//            }

//            public string[] Paths
//            {
//                get { return _path.Split(new Char[]{';'}, StringSplitOptions.RemoveEmptyEntries); }
//            }
//        }

//        #endregion

//        #region Execute

//        protected override void ExecuteTask()
//        {
//            Project.Log(Level.Info, "Getting reference to " + _server);
//            TeamFoundationServer tfs = new TeamFoundationServer(Server, Credentials);

//            Project.Log(Level.Info, "Getting reference to Version Control");
//            VersionControlServer versionControl = (VersionControlServer)tfs.GetService(typeof(VersionControlServer));

//            Project.Log(Level.Info, "Listening for Source Control events");
//            versionControl.NonFatalError += this.OnNonFatalError;
//            versionControl.Getting += this.OnGetting;
            
//            // get changeset number
//            // This should only return 1 number, but we get into an enumerable, sort and choose the highest
//            IEnumerable changeSets = versionControl.QueryHistory(_projectPath, VersionSpec.Latest, 0, RecursionType.Full, null, null, null, 1, true, true);
//            List<Int32> csal = new List<Int32>();
//            foreach (Changeset cs in changeSets)
//                csal.Add(cs.ChangesetId);

//            csal.Sort();
            
//            if (csal.Count != 0)
//                Properties["changeset"] = csal[0].ToString();
//            else
//                Properties["changeset"] = "-1";

//            // Now we enumerate all the workspaces assigned to this username (null works, too)
//            Workspace[] workspaces = versionControl.QueryWorkspaces(WorkspaceName, Username, Workstation.Current.Name);
//            Workspace workspace = null;

//            if (workspaces.Length > 0)
//            {
//                Project.Log(Level.Info, "Using existing workspace " + workspace);
//                workspace = workspaces[0];
//            }
//            else
//            {
//                Project.Log(Level.Info, "Creating workspace " + workspace);
//                if (Username != null)
//                    workspace = versionControl.CreateWorkspace(WorkspaceName, Username);
//                else
//                    workspace = versionControl.CreateWorkspace(WorkspaceName);
//            }

//            // Now map, and get the requested files
//            try
//            {
//                Project.Log(Level.Info, "Mapping from " + _projectPath + " to " + _localPath);
//                workspace.Map(_projectPath, _localPath);
//                Project.Log(Level.Info, "Getting Items...");
//                workspace.Get(SourceItems,VersionSpec.Latest,_recursion,_options);
//            }
//            finally
//            {
//                if (DeleteWorkspace)
//                {
//                    Project.Log(Level.Info, "Deleting workspace " + WorkspaceName);
//                    workspace.Delete();
//                }
//            }
//        }

//        #endregion

//        #region Event Handlers

//        internal void OnNonFatalError(Object sender, ExceptionEventArgs e)
//        {
//            if (e.Exception != null)
//            {
//                Project.Log(Level.Error, "Non-fatal exception: " + e.Exception.Message);
//            }
//            else
//            {
//                Project.Log(Level.Error, "Non-fatal failure: " + e.Failure.Message);
//            }
//        }

//        internal void OnGetting(Object sender, GettingEventArgs e)
//        {
//            Project.Log(Level.Info, "\t\t[Getting] " + e.TargetLocalItem);
//        }

//        #endregion
//    }
//}
