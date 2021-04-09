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
        private static string downloadPath = Path.Combine(AppImageDir ?? Environment.CurrentDirectory, "." + AppDomain.CurrentDomain.FriendlyName + ".new");

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

            if (!string.IsNullOrEmpty(Config.DownloadedFilePath))
                downloadPath = Path.IsPathRooted(Config.DownloadedFilePath) ? Config.DownloadedFilePath : Path.Combine(AppImageDir, Config.DownloadedFilePath);

            if (!DownloadFile(Config.URL, downloadPath))
                if (!string.IsNullOrEmpty(Config.AlternateURL)
                 || !DownloadFile(Config.AlternateURL, downloadPath))
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
                string _file = Path.IsPathRooted(FilePath) ? FilePath : Path.Combine(AppImageDir, FilePath);
                File.Copy(_file, downloadPath, true);
            } catch (Exception ex) {
                logs.Add(new LogMessage(Logseverity.Error, "Updater.Download", $"An exception of type {ex.GetType()} was thrown, this is most likely an internal library error", ex));
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
                    logs.Add(new LogMessage(Logseverity.Error, "Updater.Download", $"An exception of type {ex.GetType()} was thrown, this is most likely an internal library error", ex));
                    return false;
                }
            }
            return true;
        }
    }
}