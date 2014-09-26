using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebBuildLib.Contract;
using System.IO;

namespace WebBuildLib
{
    public class ProjectBuildManager
    {
        private BuildConfigManager _currentManager;
        private string _recipientList;
        private bool _runDataGenerator;
        private bool _startFileTransferService;
        private bool _minify;
        #region properties
        public bool Minify
        {
            get { return _minify; }
            set { _minify = value; }
        }
        #endregion
        #region constructor
        public ProjectBuildManager(BuildConfigManager config_manager)
        {
            _currentManager = config_manager;
            _recipientList = null;
            _runDataGenerator = false;
            _startFileTransferService = false;
            _minify = false;
        }
        #endregion
        #region public
        public IProjectBuild RunBuild(string build_type, string build_level, string build_name)
        {
            BuildConfig bc = GetConfig(build_type, build_level, build_name);
            IProjectBuild build = new ProjectBuild(bc);
            if (_recipientList != null)
            {
                build.AddRecipients(_recipientList);
            }
            build.RunDataGenerator = _runDataGenerator;
            build.StartFileTransferService = _startFileTransferService;
            build.Minify = Minify;
            build.Run();
            return build;
        }
        public List<IProjectBuild> RunBuilds(string build_type, string build_level)
        {
            BuildConfigs bcs = GetConfig(build_type, build_level);
            List<IProjectBuild> builds = new List<IProjectBuild>();

            foreach (var bc in bcs)
            {
                IProjectBuild build = new ProjectBuild(bc);
                if (_recipientList != null)
                {
                    build.AddRecipients(_recipientList);
                }
                build.RunDataGenerator = _runDataGenerator;
                build.StartFileTransferService = _startFileTransferService;
                build.Run();
                builds.Add(build);
            }
            return builds;
        }
        public void AddRecipients(string recipient_list)
        {
            //Just passing it to the build(s)...
            _recipientList = recipient_list;
        }
        public void AddRunDataGenerator()
        {
            //Just passing it to the build(s)...
            _runDataGenerator = true;
        }
        public void AddStartFileTransferService()
        {
            //Just passing it to the build(s)...
            _startFileTransferService = true;
        }
        #endregion
        #region private
        private BuildConfig GetConfig(string build_type, string build_level, string build_name)
        {
            BuildConfig config = _currentManager.FindConfig(build_type, build_level, build_name);
            if (config == null)
            {
                throw new Exception(String.Format("No builds match build-type {0} build-level {1} and build-name {2}.", build_type, build_level, build_name));
            }
            return config;
        }
        //private BuildConfig GetConfig(string build_level, string build_name)
        //{
        //    BuildConfig config = _currentManager.FindConfig(build_level, build_name);
        //    if (config == null)
        //    {
        //        throw new Exception(String.Format("No builds match build-level {0} and build-name {1}.", build_level, build_name));
        //    }
        //    return config;
        //}
        private BuildConfigs GetConfig(string build_type, string build_level)
        {
            BuildConfigs configs = _currentManager.FindConfig(build_type, build_level);
            if (configs == null || configs.Count < 1)
            {
                throw new Exception(String.Format("No builds match build-level {0}.", build_level));
            }
            return configs;
        }
        #endregion
    }
}
