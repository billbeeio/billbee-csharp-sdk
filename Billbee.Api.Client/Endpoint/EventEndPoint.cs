using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Billbee.Api.Client.Enums;
using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.EndPoint
{
    /// <summary>
    /// EndPoint to access event related functions
    /// </summary>
    public class EventEndPoint : RestClientBaseClass
    {
        internal EventEndPoint(ApiConfiguration config, ILogger logger = null) : base(logger, config)
        {
        }

        /// <summary>
        ///  Calls a list of events for the selected account.
        /// </summary>
        /// <param name="minDate">Date to select only newer events</param>
        /// <param name="maxDate">Date to select only older events</param>
        /// <param name="page">The page, selected</param>
        /// <param name="pageSize">The events per page</param>
        /// <param name="typeIds">Defines, which types if events should be listet</param>
        /// <returns>List of the events, mathcing the search criteria.</returns>
        public ApiPagedResult<List<Event>> GetEvents(
            DateTime? minDate = null,
            DateTime? maxDate = null,
            int page = 1,
            int pageSize = 50,
            List<EventTypeEnum> typeIds = null)
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
            int index = 0;
            if (typeIds != null)
            {
                foreach (var typeId in typeIds)
                {
                    parameters.Add($"typeId[{index}]", ((int) typeId).ToString());
                    index++;
                }
            }

            return requestResource<ApiPagedResult<List<Event>>>("/events", parameters);
        }
    }
}
