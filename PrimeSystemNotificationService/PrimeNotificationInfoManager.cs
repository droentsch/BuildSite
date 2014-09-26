using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Timers;
using System.Xml.Linq;

namespace PrimeSystemNotificationService
{
    public class PrimeNotificationInfoManager
    {
        #region const
        private const string FILE_EXISTS = "FileExists";
        private const string MAILFILE_FORM = "MailFileForm";
        #endregion
        #region members
        private Log _log;
        private INotificationInfo _info;
        private Timer _sendTimer;
        private string _smtpServer
        {
            get
            {
                if (ConfigurationManager.AppSettings["SmtpServer"] == null)
                {
                    _log.LogMessage("SmtpServer is missing from AppConfig");
                    throw new Exception("SmtpServer is missing from AppConfig");
                }
                return ConfigurationManager.AppSettings["SmtpServer"].ToString();
            }
        }
        private string _applicationName
        {
            get
            {
                if (ConfigurationManager.AppSettings["ApplicationName"] == null)
                {
                    _log.LogMessage("ApplicationName is missing from AppConfig");
                    throw new Exception("ApplicationName is missing from AppConfig");
                }
                return ConfigurationManager.AppSettings["ApplicationName"].ToString();
            }
        }
        private string _applicationVersion
        {
            get
            {
                if (ConfigurationManager.AppSettings["ApplicationVersion"] == null)
                {
                    _log.LogMessage("ApplicationVersion is missing from AppConfig");
                    throw new Exception("ApplicationVersion is missing from AppConfig");
                }
                return ConfigurationManager.AppSettings["ApplicationVersion"].ToString();
            }
        }
        private string _applicationAdministrator
        {
            get
            {
                if (ConfigurationManager.AppSettings["ApplicationAdministrator"] == null)
                {
                    _log.LogMessage("ApplicationAdministrator is missing from AppConfig");
                    throw new Exception("ApplicationAdministrator is missing from AppConfig");
                }
                return ConfigurationManager.AppSettings["ApplicationAdministrator"].ToString();
            }
        }
        #endregion
        public PrimeNotificationInfoManager(INotificationInfo info)
        {
            try
            {
                init(info);
            }
            catch (Exception ex)
            {
                if (_log != null)
                {
                    _log.LogNotify("An error occurred initializing the Prime Notification Service.  The exception thrown was: {0} - {1}.", ex.Message, ex.StackTrace);
                }
            }
        }

        private void init(INotificationInfo info)
        {
            _info = info; /*assign the injected class to a  member*/

            loadConfig(); /*the elements of the config file will appear as members of the _info.Config instance. */

            _log = new Log(_info.Config.LogPath, _smtpServer, _applicationAdministrator, _info.Config.ErrorEmailFromAddress);

            _log.LogMessage(String.Format("{0} version {1} started.", _applicationName.ToUpper(), _applicationVersion));

            SendNotifications(); /* First session of sending. */

            StartCountdown();  /*start the timer. */

            _log.LogMessage("{0} manager initialized.", _info.Name);
        }
        #region private
        private bool NotificationsArePending()
        {
            if (_info.Config.FlagType == FILE_EXISTS)
            {
                if (File.Exists(_info.Config.FlagFile))
                {
                    return true;
                }
                return false;
            }
            _log.LogNotify("No FlagType specified in configuration of {0} info class.", _info.Name);
            return false;
        }
        private void loadConfig()
        {
            _info.Config = LoadXmlFile<NotificationConfig>(_info.ConfigPath);
        }
        private void StartCountdown()
        {
            _sendTimer = new Timer(_info.Config.Interval * 60 * 1000);
            _sendTimer.Enabled = true;
            _sendTimer.Elapsed += new ElapsedEventHandler(TimedSend);
            _log.LogMessage("Countdown started with interval of {0} minutes ({1} ms).", _info.Config.Interval, _info.Config.Interval * 60000);
        }

        private void TimedSend(object sender, ElapsedEventArgs e)
        {
            loadConfig();
            SendNotifications();
        }
        private void SendNotifications()
        {
            if (NotificationsArePending())
            {
                if (_info.Config.NotificationForm == MAILFILE_FORM)
                {
                    _info.PrimeMail = LoadXmlFile<PrimeEmail>(_info.Config.NotificationFile);
                    Send();
                    if (_info.Config.DeleteXmlFileAfterUse == true)
                    {
                        File.Delete(_info.Config.NotificationFile);
                    }
                    File.Delete(_info.Config.FlagFile);
                }
            }
        }
        private void Send()
        {
            MailMessage mailMessage = new MailMessage();
            StringBuilder sb = new StringBuilder();

            List<string> tos = _info.PrimeMail.To.Split(';').ToList();
            foreach (string to in tos)
            {
                mailMessage.To.Add(to);
            }
            mailMessage.From = new MailAddress(_info.PrimeMail.From);
            if (_info.PrimeMail.Bodys != null && _info.PrimeMail.Bodys.Count() > 0)
            {
                foreach (Body body in _info.PrimeMail.Bodys)
                {
                    sb.Append(body.Message);
                    sb.Append("\n");
                }
                mailMessage.Body = sb.ToString();
            }
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.Subject = _info.PrimeMail.Subject;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            SmtpClient client = new SmtpClient(_smtpServer);

            try
            {
                client.Send(mailMessage);

                _log.LogMessage("Sending notification mail via SMTP.");
            }
            catch (Exception ex)
            {
                _log.LogMessage("Error sending error mail via SMTP: {0}\nStack Trace: {1}", ex.Message, ex.StackTrace);
            }
        }
        private T LoadXmlFile<T>(string path)
        {
            XDocument xdoc = new XDocument();
            xdoc = XDocument.Load(path);
            return xdoc.Deserialize<T>();
        }

        #endregion
    }
}
