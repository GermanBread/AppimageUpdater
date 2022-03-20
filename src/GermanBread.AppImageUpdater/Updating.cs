// System
using System;
using System.IO;

// Mono.Posix
using Mono.Unix;

namespace GermanBread.AppImageUpdater
{
    public static partial class Updater
    {
        /// <summary>
        /// Starts the updating process.
        /// </summary>
        /// <returns>TRUE if successful; If FALSE use Updater.LastError to get the error</returns>
        public static bool Update() {
            if (!IsAppImage) {
                logs.Add(new LogMessage(LogSeverity.Error, "Updater.Update", "This app is not packaged in an AppImage"));
                return false;
            }
            
            if (!_ready) {
                logs.Add(new LogMessage(LogSeverity.Error, "Updater.Update", "Updater is not ready yet"));
                return false;
            }

            // First, create a backup
            logs.Add(new LogMessage(LogSeverity.Info, "Updater.Update", "Creating backup"));
            try {
                File.Copy(AppImagePath, AppImagePath + "~", true);
            }
            catch (Exception ex) {
                // I'd be very suprised if this throws an exception
                logs.Add(new LogMessage(LogSeverity.Error, "Updater.Update", $"An exception of type {ex.GetType()} was thrown, this is most likely an internal library error", ex));
                return false;
            }
            
            // Mark as executable
            logs.Add(new LogMessage(LogSeverity.Info, "Updater.Update", "Marking as executable"));
            try {
                var _file = UnixFileInfo.GetFileSystemEntry(_downloadPath);
                // This will do for now...
                _file.FileAccessPermissions = FileAccessPermissions.UserReadWriteExecute;
            }
            catch (Exception ex) {
                logs.Add(new LogMessage(LogSeverity.Error, "Updater.Update", $"An exception of type {ex.GetType()} was thrown, this is most likely an internal library error", ex));
                return false;
            }
            
            // Then overwrite
            logs.Add(new LogMessage(LogSeverity.Info, "Updater.Update", "Overwriting"));
            try {
                File.Delete(AppImagePath);
                File.Move(_downloadPath, AppImagePath);
            }
            catch (Exception ex) {
                logs.Add(new LogMessage(LogSeverity.Error, "Updater.Update", $"An exception of type {ex.GetType()} was thrown, this is most likely an internal library error", ex));
                return false;
            }

            // Delete backups
            logs.Add(new LogMessage(LogSeverity.Info, "Updater.Update", "Deleting backup"));
            try {
                File.Delete(AppImagePath + "~");
            }
            catch (Exception ex) {
                logs.Add(new LogMessage(LogSeverity.Error, "Updater.Update", $"An exception of type {ex.GetType()} was thrown, this is most likely an internal library error", ex));
                return false;
            }

            logs.Add(new LogMessage(LogSeverity.Info, "Updater.Update", "Done, restarting the app is recommended"));
            
            return true;
        }
    }
}