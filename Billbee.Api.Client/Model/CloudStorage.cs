using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public class CloudStorage
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool UsedAsPrinter { get; set; }
    }
}
