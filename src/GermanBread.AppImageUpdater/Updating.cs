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
                logs.Add(new LogMessage(Logseverity.Error, "Updater.Update", "This app is not packaged in an AppImage"));
                return false;
            }
            
            if (!ready) {
                logs.Add(new LogMessage(Logseverity.Error, "Updater.Update", "Updater is not ready yet"));
                return false;
            }

            // First, create a backup
            logs.Add(new LogMessage(Logseverity.Info, "Updater.Update", "Creating backup"));
            try {
                File.Copy(AppImagePath, AppImagePath + "~", true);
            }
            catch (Exception ex) {
                // I'd be very suprised if this throws an exception
                logs.Add(new LogMessage(Logseverity.Error, "Updater.Update", $"An exception of type {ex.GetType()} was thrown, this is most likely an internal library error", ex));
                return false;
            }
            
            // Mark as executable
            logs.Add(new LogMessage(Logseverity.Info, "Updater.Update", "Marking as executable"));
            try {
                var _file = UnixFileInfo.GetFileSystemEntry(downloadPath);
                // This will do for now...
                _file.FileAccessPermissions = FileAccessPermissions.UserReadWriteExecute;
            }
            catch (Exception ex) {
                logs.Add(new LogMessage(Logseverity.Error, "Updater.Update", $"An exception of type {ex.GetType()} was thrown, this is most likely an internal library error", ex));
                return false;
            }
            
            // Then overwrite
            logs.Add(new LogMessage(Logseverity.Info, "Updater.Update", "Overwriting"));
            try {
                File.Delete(AppImagePath);
                File.Move(downloadPath, AppImagePath);
            }
            catch (Exception ex) {
                logs.Add(new LogMessage(Logseverity.Error, "Updater.Update", $"An exception of type {ex.GetType()} was thrown, this is most likely an internal library error", ex));
                return false;
            }

            // Delete backups
            logs.Add(new LogMessage(Logseverity.Info, "Updater.Update", "Deleting backup"));
            try {
                File.Delete(AppImagePath + "~");
            }
            catch (Exception ex) {
                logs.Add(new LogMessage(Logseverity.Error, "Updater.Update", $"An exception of type {ex.GetType()} was thrown, this is most likely an internal library error", ex));
                return false;
            }

            logs.Add(new LogMessage(Logseverity.Info, "Updater.Update", "Done, restarting the app is recommended"));
            
            return true;
        }
    }
}