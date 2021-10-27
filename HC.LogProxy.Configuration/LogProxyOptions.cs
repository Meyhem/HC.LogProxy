using System;

namespace HC.LogProxy.Configuration
{
    public class LogProxyOptions
    {
        public string LogServerApiKey { get; set; }
        public Uri LogServerUri { get; set; }
    }
}