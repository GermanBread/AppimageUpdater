using System.IO;
// System
using System;
using System.Net;

namespace GermanBread.AppImageUpdater
{
    public static partial class Updater
    {
        /// <summary>
        /// Whether or not the updater is ready
        /// </summary>
        public static bool IsReadyForUpdate { get => ready; }

        private static bool ready;
        private static string _downloadName = AppDomain.CurrentDomain.FriendlyName + ".new";

        /// <summary>
        /// Downloads the update using the provided configuration
        /// </summary>
        /// <param name="Config">The configuration</param>
        /// <returns>TRUE if successful; If FALSE use Updater.LastError to get the error</returns>
        public static bool Download(UpdateConfig Config) {
            if (!IsAppImage) {
                logs.Add(new LogMessage(Logseverity.Error, "Updater.Download", "This app is not packaged in an AppImage"));
                return false;
            }

            if (!DownloadFile(Config.URL, _downloadName))
                if (!string.IsNullOrEmpty(Config.AlternateURL)
                 || !DownloadFile(Config.AlternateURL, _downloadName))
                    return false;
            
            ready = true;
            return true;
        }
        /// <summary>
        /// "Downloads" from a local file on the filesystem
        /// </summary>
        /// <param name="FilePath">Path to the file</param>
        /// <returns>TRUE if successful; If FALSE use Updater.LastError to get the error</returns>
        public static bool Download(string FilePath) {
            if (!IsAppImage) {
                logs.Add(new LogMessage(Logseverity.Error, "Updater.Download", "This app is not packaged in an AppImage"));
                return false;
            }
            
            try {
                File.Copy(Path.Combine(AppImageDir, FilePath), Path.Combine(AppImageDir, _downloadName), true);
            } catch (Exception ex) {
                logs.Add(new LogMessage(Logseverity.Error, "Updater.Download", $"An exception of type {ex.GetType()} was thrown", ex));
                return false;
            }
            ready = true;
            return true;
        }

        internal static bool DownloadFile(string URL, string Destination) {
            using (var client = new WebClient())
            {
                try {
                    client.DownloadFile(URL, Destination);
                } catch (WebException wex) {
                    logs.Add(new LogMessage(Logseverity.Error, "Updater.Download", $"The server responded with HTTP {wex.Status}", wex));
                    return false;
                } catch (Exception ex) {
                    logs.Add(new LogMessage(Logseverity.Error, "Updater.Download", $"An exception of type {ex.GetType()} was thrown", ex));
                    return false;
                }
            }
            return true;
        }
    }
}