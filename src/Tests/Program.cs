// System
using System;
using System.IO;

// AppImageUpdater
using GermanBread.AppImageUpdater;

namespace Tests
{
    internal static class Program
    {
        private static void Main()
        {
            // Used later
            var copy = Path.Combine(Updater.AppImageDir, "Updated.AppImage");
            File.Copy(Updater.AppImagePath, copy, true);
            
            // Remote server example
            var config = new UpdateConfig("https://github.com/ppy/osu/releases/latest/download/osu.AppImage")
                {
                    DownloadedFilePath = "test"
                };
            Updater.Download(config);
            Updater.Update();

            // Local file example
            Updater.Download("Updated.AppImage");
            Updater.Update();

            // Remove the copy
            if (File.Exists(copy))
                File.Delete(copy);
            
            foreach (var log in Updater.Logs) {
                Console.WriteLine(log.Message);
                if (log.Exception != null)
                    Console.WriteLine(log.Exception);
            }
        }
    }
}
