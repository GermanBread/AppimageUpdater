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
        public static bool IsReadyForUpdate => _ready;

        private static bool _ready;
        private static string _downloadPath = Path.Combine(AppImageDir ?? Environment.CurrentDirectory,
            "." + AppDomain.CurrentDomain.FriendlyName + ".new");

        /// <summary>
        /// Downloads the update using the provided configuration
        /// </summary>
        /// <param name="config">The configuration</param>
        /// <returns>TRUE if successful; If FALSE use Updater.LastError to get the error</returns>
        public static bool Download(UpdateConfig config) {
            if (!IsAppImage) {
                logs.Add(new LogMessage(LogSeverity.Error, "Updater.Download", "This app is not packaged in an AppImage"));
                return false;
            }

            if (!string.IsNullOrEmpty(config.DownloadedFilePath))
                _downloadPath = Path.IsPathRooted(config.DownloadedFilePath) ? config.DownloadedFilePath : Path.Combine(AppImageDir, config.DownloadedFilePath);

            if (!DownloadFile(config.Url, _downloadPath))
                if (!string.IsNullOrEmpty(config.AlternateUrl)
                 || !DownloadFile(config.AlternateUrl, _downloadPath))
                    return false;
            
            _ready = true;
            return true;
        }
        /// <summary>
        /// "Downloads" from a local file on the filesystem
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns>TRUE if successful; If FALSE use Updater.LastError to get the error</returns>
        public static bool Download(string filePath) {
            if (!IsAppImage) {
                logs.Add(new LogMessage(LogSeverity.Error, "Updater.Download",
                    "This app is not packaged in an AppImage"));
                return false;
            }

            try {
                if (filePath != null)
                {
                    var file = Path.IsPathRooted(filePath) ? filePath : Path.Combine(AppImageDir, filePath);
                    File.Copy(file, _downloadPath, true);
                }
            } catch (Exception ex) {
                logs.Add(new LogMessage(LogSeverity.Error, "Updater.Download",
                    $"An exception of type {ex.GetType()} was thrown, this is most likely an internal library error",
                    ex));
                return false;
            }
            
            _ready = true;
            return true;
        }

        private static bool DownloadFile(string url, string destination) {
            using var client = new WebClient();
            try {
                client.DownloadFile(url, destination);
            } catch (WebException wex) {
                logs.Add(new LogMessage(LogSeverity.Error, "Updater.Download",
                    $"The server responded with HTTP {wex.Status}", wex));
                return false;
            } catch (Exception ex) {
                logs.Add(new LogMessage(LogSeverity.Error, "Updater.Download",
                    $"An exception of type {ex.GetType()} was thrown, this is most likely an internal library error",
                    ex));
                return false;
            }

            return true;
        }
    }
}