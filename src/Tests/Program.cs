// System
using System;

// AppImageUpdater
using GermanBread.AppImageUpdater;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            // Local file example
            //Updater.Download("Updated.AppImage");
            // Remote server example
            UpdateConfig _config = new UpdateConfig("https://github.com/ppy/osu/releases/latest/download/osu.AppImage");
            Updater.Download(_config);
            Updater.Update();
            
            foreach (var log in Updater.Logs) {
                Console.WriteLine(log.Message);
                if (log.Exception != null)
                    Console.WriteLine(log.Exception);
            }
        }
    }
}
