using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public class DeletedImages
    {
        public List<long> Deleted { get; set; }
        public List<long> NotFound { get; set; }
    }
}
