/// <summary>
/// This struct contains the configuration needed for the updater to function
/// </summary>
namespace GermanBread.AppImageUpdater
{
    public struct UpdateConfig
    {
        /// <summary>
        /// Initializes the struct without a fallback URL
        /// </summary>
        /// <param name="URL">The server from which to download the AppImage</param>
        public UpdateConfig(string URL) {
            this.URL = URL;
            AlternateURL = null;
        }
        /// <summary>
        /// Initializes the struct
        /// </summary>
        /// <param name="URL">The server from which to download the AppImage</param>
        /// <param name="AlternateURL">Fallback URL</param>
        public UpdateConfig(string URL, string AlternateURL) {
            this.URL = URL;
            this.AlternateURL = AlternateURL;
        }
        
        /// <summary>
        /// The URL which will be used to download the AppImage
        /// </summary>
        public string URL;
        /// <summary>
        /// Fallback URL in case the server returns a status code besides 2xx
        /// </summary>
        public string AlternateURL;
    }
}