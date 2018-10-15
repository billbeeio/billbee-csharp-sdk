using Billbee.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.EndPoint
{
    /// <summary>
    /// Endpoint to access customer base data
    /// </summary>
    public class CustomerEndPoint: RestClientBaseClass
    {
        internal CustomerEndPoint(ApiConfiguration config, ILogger logger = null) : base(logger, config)
        {
        }

        public ApiPagedResult<List<Customer>> GetCustomerList(int page, int pageSize)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());

            return requestResource<ApiPagedResult<List<Customer>>>($"/customers", parameters);
        }
    }
}
