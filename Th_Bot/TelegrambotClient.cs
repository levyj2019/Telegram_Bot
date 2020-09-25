using System;

namespace Th_Bot
{
    internal class TelegrambotClient
    {
        private string v;

        public TelegrambotClient(string v)
        {
            this.v = v;
        }

        public TimeSpan Timeout { get; set; }
    }
}