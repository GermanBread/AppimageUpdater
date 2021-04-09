// System
using System;
using System.IO;

// AppImageUpdater
using GermanBread.AppImageUpdater;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            // Used later
            string _copy = Path.Combine(Updater.AppImageDir, "Updated.AppImage");
            File.Copy(Updater.AppImagePath, _copy, true);
            
            // Remote server example
            UpdateConfig _config = new UpdateConfig("https://github.com/ppy/osu/releases/latest/download/osu.AppImage");
            _config.DownloadedFilePath = "test";
            Updater.Download(_config);
            Updater.Update();

            // Local file example
            Updater.Download("Updated.AppImage");
            Updater.Update();

            // Remove the copy
            if (File.Exists(_copy))
                File.Delete(_copy);
            
            foreach (var log in Updater.Logs) {
                Console.WriteLine(log.Message);
                if (log.Exception != null)
                    Console.WriteLine(log.Exception);
            }
        }
    }
}
