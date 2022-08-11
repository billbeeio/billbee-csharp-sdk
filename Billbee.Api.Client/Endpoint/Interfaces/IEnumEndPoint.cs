using System.Collections.Generic;
using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.Endpoint.Interfaces
{
    public interface IEnumEndPoint
    {
        /// <summary>
        /// Gets a list of all payment types
        /// </summary>
        /// <returns>The list of all payment types</returns>
        List<EnumEntry> GetPaymentTypes();

        /// <summary>
        /// Gets a list of all shipping carriers
        /// </summary>
        /// <returns>The list of all shipping carriers</returns>
        List<EnumEntry> GetShippingCarriers();

        /// <summary>
        /// Gets a list of all shipment types
        /// </summary>
        /// <returns>The list of all shipment types</returns>
        List<EnumEntry> GetShipmentTypes();
        
        /// <summary>
        /// Gets a list of all order states
        /// </summary>
        /// <returns>The list of all order states</returns>
        List<EnumEntry> GetOrderStates();
    }
}