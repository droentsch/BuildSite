using Microsoft.Build.Evaluation;
using WebBuildLib.Contract;
using System;
using Microsoft.Build.Logging;
using Microsoft.Build.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WebBuildLib
{
    public class ProjectBuild : IProjectBuild
    {
        public Project Build { get; set; }
        public string ShortName { get; set; }
        public string Link { get; set; }
        public string Filepath { get; set; }
        public string CurrentVersion { get; set; }
        public IBuildConfig Configuration { get; set; }
        public bool RunDataGenerator { get; set; }
        public bool Minify { get; set; }
        public bool StartFileTransferService { get; set; }
        public List<string> NotificationRecipients;

        #region constructor
        public ProjectBuild(IBuildConfig config)
        {
            Configuration = config;
            Build = new Project(config.Path);
            ShortName = FindProperty("TargetProject");
            Link = FindProperty("SuccessUri");
            Filepath = FindProperty("DeployDir");
            setLogger(config.LogFilePrefix, config.LogPath);
            NotificationRecipients = new List<string>();
        }
        #endregion
        public void Run()
        {
            try
            {
                ModifyBuild();
                Build.Build();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                Build.ProjectCollection.UnregisterAllLoggers();
                Build.ProjectCollection.UnloadAllProjects();
            }
        }
        public void Unload()
        {
            if (Build != null && Build.ProjectCollection != null && Build.ProjectCollection.Count > 0)
            {
                Build.ProjectCollection.UnregisterAllLoggers();
                Build.ProjectCollection.UnloadAllProjects();
            }
        }
        public void AddRecipients(string recipients_to_add)
        {
            if (recipients_to_add.IndexOf(";") == -1)
            {
                NotificationRecipients.Add(recipients_to_add);
                return;
            }
            NotificationRecipients = recipients_to_add.Split(';').ToList();
        }
        #region private
        private void ModifyBuild()
        {
            ProjectProperty minifyProp = null;
            if (NotificationRecipients.Count > 0)
            {
                foreach (var recipient in NotificationRecipients)
                {
                    Build.AddItemFast("SuccessMailRecipients", recipient);
                    Build.AddItemFast("ErrorMailRecipients", recipient);
                }
            }
            if (RunDataGenerator == true)
            {
                ProjectProperty rundgProp = Build.Properties.Where(x => x.Name=="RunDataGenerator").FirstOrDefault();
                if (rundgProp != null)
                {
                    rundgProp.UnevaluatedValue = "true";
                }
            }
            if (StartFileTransferService == true)
            {
                ProjectProperty startftProp = Build.Properties.Where(x => x.Name == "StartServiceOnDeploy").FirstOrDefault();
                if (startftProp != null)
                {
                    startftProp.UnevaluatedValue = "true";
                }
            }
            if (Minify == true)
            {
                minifyProp = Build.Properties.Where(x => x.Name == "MinifyCSSJS").FirstOrDefault();
                if (minifyProp != null)
                {
                    minifyProp.UnevaluatedValue = "true";
                }
            }
            else
            {
                minifyProp = Build.Properties.Where(x => x.Name == "MinifyCSSJS").FirstOrDefault();
                if (minifyProp != null)
                {
                    minifyProp.UnevaluatedValue = "false";
                }
            }
        }
        private void setLogger(string log_prefix, string log_path)
        {
            List<FileLogger> loggers = new List<FileLogger>();
            FileLogger logger = new FileLogger();
            logger.ShowSummary = true;
            logger.Verbosity = LoggerVerbosity.Diagnostic;
            logger.Parameters = String.Format("LogFile={0}\\msbuild.{1}.log", log_path, log_prefix);
            loggers.Add(logger);
            logger = new FileLogger();
            logger.ShowSummary = true;
            logger.Verbosity = LoggerVerbosity.Quiet;
            logger.Parameters = String.Format("LogFile={0}\\{1}.errors.log", log_path, log_prefix);
            loggers.Add(logger);
            logger = new FileLogger();
            logger.ShowSummary = true;
            logger.Verbosity = LoggerVerbosity.Quiet;
            logger.Parameters = String.Format("LogFile={0}\\{1}.warnings.log", log_path, log_prefix);
            loggers.Add(logger);
            Build.ProjectCollection.RegisterLoggers(loggers);
        }
        private string FindProperty(string name_to_find)
        {
            string n = name_to_find;

            ICollection<ProjectProperty> props = Build.Properties;
            foreach (var prop in props)
            {
                if (prop.Name == n)
                {
                    return prop.EvaluatedValue;
                }
            }
            return String.Empty;
        }
    }
        #endregion
}
