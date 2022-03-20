// System

using System;

namespace GermanBread.AppImageUpdater
{
    public struct LogMessage
    {
        public LogMessage(LogSeverity severity, string source, string message, Exception exception) {
            _source = source;
            Message = message;
            Severity = severity;
            Exception = exception;
            _timestamp = DateTime.UtcNow;
        }
        public LogMessage(LogSeverity severity, string source, string message) {
            _source = source;
            Exception = null;
            Message = message;
            Severity = severity;
            _timestamp = DateTime.UtcNow;
        }

        private readonly string _source;
        public readonly string Message;
        /// <summary>
        /// Value which indicates when this message was created. In UTC time.
        /// </summary>
        private readonly DateTime _timestamp;
        public readonly Exception Exception;
        /// <summary>
        /// The severity which (in the case of errors) can indicate the severity of such. 0 = most urgent
        /// </summary>
        public readonly LogSeverity Severity;

        public override string ToString() {
            var output = null as string;
            
            output += $"{_timestamp.ToShortDateString()} {_timestamp.ToShortTimeString()}: ";
            output += $"[{Severity.ToString()}, 8] ";
            output += $"{_source} - ";
            output += $"{Message}";
            if (Exception != null)
                output += $"\n({Exception})";
            
            return output;
        }
    }
}