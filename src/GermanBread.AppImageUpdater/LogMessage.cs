// System
using System;

namespace GermanBread.AppImageUpdater
{
    public struct LogMessage
    {
        public LogMessage(Logseverity Severity, string Source, string Message, Exception Exception) {
            this.Source = Source;
            this.Message = Message;
            this.Severity = Severity;
            this.Exception = Exception;
            this.Timestamp = DateTime.UtcNow;
        }
        public LogMessage(Logseverity Severity, string Source, string Message) {
            this.Source = Source;
            this.Exception = null;
            this.Message = Message;
            this.Severity = Severity;
            this.Timestamp = DateTime.UtcNow;
        }

        public string Source;
        public string Message;
        /// <summary>
        /// Value which indicates when this message was created. In UTC time.
        /// </summary>
        public DateTime Timestamp;
        public Exception Exception;
        /// <summary>
        /// The severity which (in the case of errors) can indicate the severity of such. 0 = most urgent
        /// </summary>
        public Logseverity Severity;
    }
}