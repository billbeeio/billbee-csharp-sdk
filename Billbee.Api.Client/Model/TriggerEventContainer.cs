using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public class TriggerEventContainer
    {
        /// <summary>Name of the event</summary>
        public string Name { get; set; }

        /// <summary>The delay in minutes until the rule is executed</summary>
        public uint DelayInMinutes { get; set; } = 0;
    }
}
