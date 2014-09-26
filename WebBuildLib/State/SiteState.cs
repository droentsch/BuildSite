using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using WebBuildLib.Extensions;

namespace WebBuildLib.State
{
    public class SiteState
    {
        private string _statusPath;
        private string _logPath;
        private Status _status;
        private const int MAX_LOGS_TO_KEEP = 25;
        private Logs _logs;

        public Logs Logs
        {
            get
            {
                LoadLogs();
                return _logs;
            }
        }
        public Status Status
        {
            get { return _status; }
        }
        public string LastBuildInitiator
        {
            get
            {
                return _status.LastBuildInitiatedBy;
            }
            set
            {
                _status.LastBuildInitiatedBy = value;
                _status.Serialize<Status>(_statusPath);
            }
        }
        public bool BuildIsRunning
        {
            get { return _status.BuildIsRunning; }
            set
            {
                _status.BuildIsRunning = value;
                _status.Serialize<Status>(_statusPath);
            }
        }

        public SiteState(string path_to_status_data, string path_to_log)
        {
            _statusPath = path_to_status_data;
            _logPath = path_to_log;

            XDocument xdoc = new XDocument();
            xdoc = XDocument.Load(_statusPath);
            _status = xdoc.Deserialize<Status>();
            _logs = new Logs();
        }
        #region LogMessage
        public void LogMessage(string message)
        {
            LoadLogs();
            string ts = String.Format("{0} - {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
            Log entry = new Log { Message = message, TimeStamp = ts };
            _logs.Add(entry);
            if (_logs.Count > MAX_LOGS_TO_KEEP)
            {
                _logs.Remove(_logs[0]);
            }
            _logs.Serialize<Logs>(_logPath);
        }


        public void LogMessage(string text, object arg, object arg2, object arg3, object arg4)
        {
            string msg = String.Format(text, arg, arg2, arg3, arg4);

            LogMessage(msg);
        }
        public void LogMessage(string text, object arg, object arg2, object arg3)
        {
            string msg = String.Format(text, arg, arg2, arg3);

            LogMessage(msg);
        }
        public void LogMessage(string text, object arg, object arg2)
        {
            string msg = String.Format(text, arg, arg2);

            LogMessage(msg);
        }
        public void LogMessage(string text, object arg)
        {
            string msg = String.Format(text, arg);

            LogMessage(msg);
        }
        public void LogMessage(string text, object[] args)
        {
            string msg = String.Format(text, args);

            LogMessage(msg);
        }
        #endregion
        #region private methods
        private void LoadLogs()
        {
            if (File.Exists(_logPath))
            {
                XDocument xdoc = new XDocument();
                try
                {
                    xdoc = XDocument.Load(_logPath);
                    _logs = xdoc.Deserialize<Logs>();
                    if (_logs == null)
                    {
                        _logs = new Logs();
                    }
                }
                catch (XmlException)
                {
                    _logs = new Logs();
                }
            }
        }
        #endregion
    }
}