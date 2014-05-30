using System;
using System.IO;

namespace NetMailSample
{
    public class LoggerEventArgs : EventArgs
    {
        private DateTime _logTime;
        private string _logDetails;

        public LoggerEventArgs(DateTime LogTime, string LogDetails)
        {
            _logTime = LogTime;
            _logDetails = LogDetails;
        }

        public DateTime LogTime
        {
            get { return _logTime; }
        }

        public string LogDetails
        {
            get { return _logDetails; }
        }
    }

    public class ClassLogger
    {
        private StreamWriter _logStream = null;
        private string _logPath = "";
        private bool _logDateAndTime = true;

        public delegate void LoggerEventHandler(object sender, LoggerEventArgs a);
        public event LoggerEventHandler LogAdded;

        public ClassLogger(string LogFile)
        {
            try
            {
                _logStream = File.AppendText(LogFile);
                _logPath = LogFile;
            }
            catch { }
        }

        ~ClassLogger()
        {
            try
            {
                _logStream.Flush();
                _logStream.Close();
            }
            catch { }
        }

        protected virtual void OnLogAdded(LoggerEventArgs e)
        {
            LoggerEventHandler handler = LogAdded;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        public bool LogDateAndTime
        {
            get { return _logDateAndTime; }
            set { _logDateAndTime = value; }
        }

        public void ClearFile()
        {
        }

        public void Log(string Details, string Description = "", bool SuppressEvent = false)
        {
            try
            {
                DateTime oLogTime = DateTime.Now;

                if (String.IsNullOrEmpty(Description))
                {
                    if (_logDateAndTime)
                        _logStream.WriteLine(String.Format("{0:dd/MM/yy HH:mm:ss}", oLogTime) + " ==> " + Details);
                }
                else
                {
                    _logStream.WriteLine("");
                    if (_logDateAndTime)
                        _logStream.WriteLine(String.Format("{0:dd/MM/yy HH:mm:ss}", oLogTime) + " ==> " + Description);
                    _logStream.WriteLine(Details);
                }
                _logStream.Flush();

                if (!SuppressEvent)
                    OnLogAdded(new LoggerEventArgs(oLogTime, Description + Environment.NewLine + Details));
            }
            catch {}
        }
    }
}
