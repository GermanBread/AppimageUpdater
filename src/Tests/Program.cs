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
            Updater.Download("Updated.AppImage");
            /*
            // Remote server example
            UpdateConfig _config = new UpdateConfig("http://example.com");
            Updater.Download(_config);
            */
            Updater.Update();
            foreach (var log in Updater.Logs) {
                Console.WriteLine(log.Message);
                if (log.Exception != null)
                    Console.WriteLine(log.Exception);
            }
        }
    }
}
