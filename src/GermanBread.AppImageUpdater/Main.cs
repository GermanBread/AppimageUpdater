// System
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace GermanBread.AppImageUpdater
{
    public static partial class Updater
    {
        /// <summary>
        /// Whether or not the app is an AppImage.
        /// </summary>
        private static bool IsAppImage { get; } = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("APPIMAGE"));
        /// <summary>
        /// The full path to the AppImage
        /// </summary>
        public static string AppImagePath { get; } = Environment.GetEnvironmentVariable("APPIMAGE");
        /// <summary>
        /// The directory the AppImage is located in
        /// </summary>
        public static string AppImageDir { get; } = Path.GetDirectoryName(AppImagePath);
        public static LogMessage LastError => logs.FirstOrDefault(x => x.Severity <= LogSeverity.Error);
        public static LogMessage LastLog => logs.FirstOrDefault();
        public static IReadOnlyCollection<LogMessage> Logs => logs;

        private static List<LogMessage> logs = new List<LogMessage>();
    }
}