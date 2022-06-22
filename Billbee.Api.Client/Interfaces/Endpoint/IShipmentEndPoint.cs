using System.Collections.Generic;
using Billbee.Api.Client.Model;
using Billbee.Api.Client.Model.Rechnungsdruck.WebApp.Model.Api;

namespace Billbee.Api.Client.Interfaces.Endpoint
{
    /// <summary>
    /// EndPoint for generation of shipments
    /// </summary>
    public interface IShipmentEndPoint
    {
        /// <summary>
        /// Requests a list of all available shipping providers and their products.
        /// </summary>
        /// <returns>List of shipping providers.</returns>
        List<ShippingProvider> GetShippingProvider();
        
        /// <summary>
        /// Creates a new shipment.
        /// </summary>
        /// <param name="shipment">The shipment specification, that should be created.</param>
        /// <returns>The result of the shipment <see cref="ShipmentResult"/></returns>
        ApiResult<ShipmentResult> PostShipment(PostShipment shipment);

        ApiResult<ShipmentWithLabelResult> ShipOrderWithLabel(ShipmentWithLabel shipmentRequest);
        
        /// <summary>
        /// Delivers a list of all registered shipping carriers
        /// </summary>
        /// <returns>List of available shipping carriers</returns>
        List<ShippingCarrier> GetShippingCarriers();
    }
}