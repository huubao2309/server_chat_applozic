using System.Threading;
using Microsoft.AspNetCore.Mvc;
using ServerChatOmnicasa.Utils;

namespace ServerChatOmnicasa.Base
{
    public class BaseController : Controller
    {
        #region Fields

        /// <summary>
        /// Get or set CancelTokenSource
        /// </summary>
        private CancellationTokenSource _tokenSource;
        public CancellationTokenSource TokenSource
        {
            get => _tokenSource ?? (_tokenSource = new CancellationTokenSource());
            set => _tokenSource = value;
        }

        /// <summary>
        /// Log Service
        /// </summary>
        public static ServiceLogger Logger { get; set; }

        #endregion

        #region Initialize

        public BaseController()
        {
            Logger = new ServiceLogger(ConfigService.LogPath);
        }

        #endregion
    }
}
