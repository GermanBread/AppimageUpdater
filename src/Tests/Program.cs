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
            // Remote server example
            UpdateConfig _config = new UpdateConfig("https://github.com/ppy/osu/releases/latest/download/osu.AppImage");
            _config.DownloadedFilePath = "test";
            Updater.Download(_config);
            Updater.Update();

            // Local file example
            File.Copy(Updater.AppImagePath, Path.Combine(Updater.AppImageDir, "Updated.AppImage"), true);
            Updater.Download("Updated.AppImage");
            Updater.Update();
            
            foreach (var log in Updater.Logs) {
                Console.WriteLine(log.Message);
                if (log.Exception != null)
                    Console.WriteLine(log.Exception);
            }
        }
    }
}
