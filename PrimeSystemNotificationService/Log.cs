using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PrimeSystemNotificationService
{
    public class Log
    {
        #region const
        private const string ERROR_SUBJECT = "Prime Communication Services experienced a technical error.";
        #endregion

        private string _logPath;
        private string _smtpServer;
        private string _applicationAdministrator;
        private string _applicationEmail;

        public Log(string log_path, string smtp, string admin, string appEmail)
        {
            _logPath = log_path;
            _smtpServer = smtp;
            _applicationAdministrator = admin;
            _applicationEmail = appEmail;
        }
        #region LogNotify
        public void LogNotify(string text, object arg, object arg2, object arg3, object arg4)
        {
            string msg = string.Format(text, arg, arg2, arg3, arg4);
            LogNotify(msg);
        }
        public void LogNotify(string text, object arg, object arg2, object arg3)
        {
            string msg = string.Format(text, arg, arg2, arg3);
            LogNotify(msg);
        }
        public void LogNotify(string text, object arg, object arg2)
        {
            string msg = string.Format(text, arg, arg2);
            LogNotify(msg);
        }
        public void LogNotify(string text, object arg)
        {
            string msg = string.Format(text, arg);
            LogNotify(msg);
        }
        public void LogNotify(string text, object[] args)
        {
            string msg = string.Format(text, args);
            LogNotify(msg);
        }
        public void LogNotify(string message)
        {
            this.smtpErrorMail(message);
            this.LogMessage(message);
        }
        #endregion
        #region LogMessage
        public void LogMessage(string message)
        {
            string logEntry = string.Format("{0} - {1}", DateTime.Now, message);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(_logPath, true))
            {
                file.WriteLine(logEntry);
            }
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
        public void smtpErrorMail(string failureMessage)
        {
            MailMessage _mailMessage = new MailMessage();
            List<string> tos = _applicationAdministrator.Split(';').ToList();
            foreach (string to in tos)
            {
                _mailMessage.To.Add(to);
            }
            _mailMessage.From = new MailAddress(_applicationEmail);
            _mailMessage.Body = failureMessage;
            _mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            _mailMessage.Subject = ERROR_SUBJECT;
            _mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            SmtpClient client = new SmtpClient(_smtpServer);

            try
            {
                client.Send(_mailMessage);

                this.LogMessage(String.Format("Sending error mail via SMTP to {0}", _applicationAdministrator));
            }
            catch (Exception ex)
            {
                this.LogMessage(String.Format("Error sending error mail via SMTP to {0} : {1}", _applicationAdministrator, ex.Message));
            }
        }
    }
}
