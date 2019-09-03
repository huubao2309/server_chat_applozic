using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServerChatOmnicasa.Entities;
using ServerChatOmnicasa.Logger.Utils;

namespace ServerChatOmnicasa.Base
{
    public class BaseController : Controller
    {
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

        public BaseController()
        {
            Logger = new ServiceLogger(Config.LogPath);
        }
    }
}
