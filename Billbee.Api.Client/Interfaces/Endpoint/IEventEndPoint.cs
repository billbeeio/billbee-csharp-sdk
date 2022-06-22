using System;
using System.Collections.Generic;
using Billbee.Api.Client.Enums;
using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.Interfaces.Endpoint
{
    /// <summary>
    /// EndPoint to access event related functions
    /// </summary>
    public interface IEventEndPoint
    {
        /// <summary>
        ///  Calls a list of events for the selected account.
        /// </summary>
        /// <param name="minDate">Date to select only newer events</param>
        /// <param name="maxDate">Date to select only older events</param>
        /// <param name="page">The page, selected</param>
        /// <param name="pageSize">The events per page</param>
        /// <param name="typeIds">Defines, which types if events should be listet</param>
        /// <param name="orderId">If given, only events of this order will be supplied.</param>
        /// <returns>List of the events, mathcing the search criteria.</returns>
        ApiPagedResult<List<Event>> GetEvents(
            DateTime? minDate = null,
            DateTime? maxDate = null,
            int page = 1,
            int pageSize = 50,
            List<EventTypeEnum> typeIds = null,
            long? orderId = null);
    }
}