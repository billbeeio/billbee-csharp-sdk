using System.Collections.Generic;

namespace Billbee.Api.Client.Model
{
    public class Webhook
    {
            public string Id { get; set; }
        public string WebHookUri { get; set; }
        public string Secret { get; set; }
        public string Description { get; set; }
        public bool IsPaused { get; set; }
        public List<string> Filters { get; set; }
        public Dictionary<string,string> Headers { get; set; }
        public Dictionary<string,object> Properties { get; set; }
    }
}
