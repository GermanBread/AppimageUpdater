namespace GermanBread.AppImageUpdater
{
    /// <summary>
    /// This struct contains the configuration needed for the updater to function
    /// </summary>
    public struct UpdateConfig
    {
        /// <summary>
        /// Initializes the struct without a fallback URL
        /// </summary>
        /// <param name="URL">The server from which to download the AppImage</param>
        public UpdateConfig(string URL) {
            this.URL = URL;
            AlternateURL = null;
            DownloadedFilePath = null;
        }
        /// <summary>
        /// Initializes the struct with a fallback URL
        /// </summary>
        /// <param name="URL">The server from which to download the AppImage</param>
        /// <param name="AlternateURL">Fallback URL</param>
        public UpdateConfig(string URL, string AlternateURL) {
            this.URL = URL;
            this.DownloadedFilePath = null;
            this.AlternateURL = AlternateURL;
        }
        /// <summary>
        /// Initializes the struct with a download path
        /// </summary>
        /// <param name="URL">The server from which to download the AppImage</param>
        /// <param name="AlternateURL">Fallback URL</param>
        /// <param name="DownloadPath">Where to download the AppImage to</param>
        public UpdateConfig(string URL, string AlternateURL, string DownloadPath) {
            this.URL = URL;
            this.AlternateURL = AlternateURL;
            this.DownloadedFilePath = DownloadPath;
        }
        
        /// <summary>
        /// The URL which will be used to download the AppImage
        /// </summary>
        public string URL;
        /// <summary>
        /// Fallback URL in case the server returns a status code besides 2xx
        /// </summary>
        public string AlternateURL;
        /// <summary>
        /// Specifies the path where to download the updated AppImage to.
        /// </summary>
        public string DownloadedFilePath;
    }
}