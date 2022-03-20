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
        /// <param name="url">The server from which to download the AppImage</param>
        public UpdateConfig(string url) {
            Url = url;
            AlternateUrl = null;
            DownloadedFilePath = null;
        }
        /// <summary>
        /// Initializes the struct with a fallback URL
        /// </summary>
        /// <param name="url">The server from which to download the AppImage</param>
        /// <param name="alternateUrl">Fallback URL</param>
        public UpdateConfig(string url, string alternateUrl) {
            Url = url;
            DownloadedFilePath = null;
            AlternateUrl = alternateUrl;
        }
        /// <summary>
        /// Initializes the struct with a download path
        /// </summary>
        /// <param name="url">The server from which to download the AppImage</param>
        /// <param name="alternateUrl">Fallback URL</param>
        /// <param name="downloadPath">Where to download the AppImage to</param>
        public UpdateConfig(string url, string alternateUrl, string downloadPath) {
            Url = url;
            AlternateUrl = alternateUrl;
            DownloadedFilePath = downloadPath;
        }
        
        /// <summary>
        /// The URL which will be used to download the AppImage
        /// </summary>
        public readonly string Url;
        /// <summary>
        /// Fallback URL in case the server returns a status code besides 2xx
        /// </summary>
        public readonly string AlternateUrl;
        /// <summary>
        /// Specifies the path where to download the updated AppImage to.
        /// </summary>
        public string DownloadedFilePath;
    }
}