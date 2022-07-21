using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Billbee.Api.Client.Endpoint.Interfaces;
using Billbee.Api.Client.Enums;
using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.EndPoint
{
    /// <inheritdoc cref="Billbee.Api.Client.Endpoint.Interfaces.IEventEndPoint" />
    public class EventEndPoint : IEventEndPoint
    {
        private readonly IBillbeeRestClient _restClient;

        internal EventEndPoint(IBillbeeRestClient restClient)
        {
            _restClient = restClient;
        }

        public ApiPagedResult<List<Event>> GetEvents(
            DateTime? minDate = null,
            DateTime? maxDate = null,
            int page = 1,
            int pageSize = 50,
            List<EventTypeEnum> typeIds = null,
            long? orderId = null)
        {
            NameValueCollection parameters = new NameValueCollection();

            if (minDate.HasValue)
            {
                parameters.Add("minDate", minDate.Value.ToString("yyyy-MM-dd HH:mm"));
            }

            if (maxDate.HasValue)
            {
                parameters.Add("maxDate", maxDate.Value.ToString("yyyy-MM-dd"));
            }

            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());


            if (orderId != null)
            {
                parameters.Add("orderId", orderId.ToString());
            }

            int index = 0;
            if (typeIds != null)
            {
                foreach (var typeId in typeIds)
                {
                    parameters.Add($"typeId[{index}]", ((int) typeId).ToString());
                    index++;
                }
            }

            return _restClient.Get<ApiPagedResult<List<Event>>>("/events", parameters);
        }
    }
}
