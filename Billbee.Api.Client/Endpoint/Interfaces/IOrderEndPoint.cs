using System;
using System.Collections.Generic;
using Billbee.Api.Client.Enums;
using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.Endpoint.Interfaces
{
    /// <summary>
    /// EndPoint to access order functions
    /// </summary>
    public interface IOrderEndPoint
    {
        /// <summary>
        /// Selects an order by it's id
        /// </summary>
        /// <param name="id">id to search for</param>
        /// <param name="articleTitleSource">The source field for the article title. 0 = Order Position (default), 1 = Article Title, 2 = Article Invoice Text</param>
        /// <returns>Details of the order</returns>
        ApiResult<Order> GetOrder(string id, int articleTitleSource = 0);

        /// <summary>
        /// Gets a list of all fileds, that can be patched at an order.
        /// </summary>
        /// <returns>List of patchable fields.</returns>
        ApiResult<List<string>> GetPatchableFields();

        /// <summary>
        /// Patches one or more fields of an order.
        /// </summary>
        /// <param name="id">The id of the order to patch.</param>
        /// <param name="fieldsToPatch">Name-Value pairs of fields, to be patched.</param>
        /// <returns></returns>
        ApiResult<Order> PatchOrder(long id, Dictionary<string, object> fieldsToPatch);

        /// <summary>
        /// Selects an order by it's external id
        /// </summary>
        /// <param name="id">The external id, that should be used for selection</param>
        /// <returns>Details of the order</returns>
        ApiResult<Order> GetOrderByExternalReference(string id);

        /// <summary>
        /// Selects an order by it's external id and its partner
        /// </summary>
        /// <param name="partner">If set, this is the internal partner name, this order should be searched at.</param>
        /// <param name="id">The external id, that should be used for selection</param>
        /// <returns>Details of the order</returns>
        ApiResult<Order> GetOrderByExternalIdAndPartner(string partner, string id);

        /// <summary>
        /// Delivers a list of orders
        /// </summary>
        /// <param name="minOrderDate">Minimum date of the order</param>
        /// <param name="maxOrderDate">Maximum date of the order</param>
        /// <param name="page">Selected page</param>
        /// <param name="pageSize">Selected number of entries per page</param>
        /// <param name="shopId">Id of the shop, the order belongs to</param>
        /// <param name="orderStateId">State of the order.<see cref="OrderStateEnum"/></param>
        /// <param name="tag">If given, only orders with this tag are returned</param>
        /// <param name="minimumBillBeeOrderId">Minimum internal id of the order. As ids are in sequence, this can be used to get only orders, not already imported.</param>
        /// <param name="modifiedAtMin">Minimum date of last modification</param>
        /// <param name="modifiedAtMax">Maximum date of last modification</param>
        /// <param name="excludeTags">Defines, that no tags should be supplied</param>
        /// <returns></returns>
        ApiPagedResult<List<Order>> GetOrderList(DateTime? minOrderDate = null,
            DateTime? maxOrderDate = null,
            int page = 1,
            int pageSize = 50,
            List<long> shopId = null,
            List<OrderStateEnum> orderStateId = null,
            List<string> tag = null,
            long? minimumBillBeeOrderId = null,
            DateTime? modifiedAtMin = null,
            DateTime? modifiedAtMax = null,
            bool excludeTags = false);

        /// <summary>
        /// Delivers a list of invoices
        /// </summary>
        /// <param name="minInvoiceDate">Minimum date of the invoice</param>
        /// <param name="maxInvoiceDate">Maximum date of the invoice</param>
        /// <param name="page">Selected page</param>
        /// <param name="pageSize">Selected number of entries per page</param>
        /// <param name="shopId">Id of the shop, the order belongs to</param>
        /// <param name="orderStateId">State of the order. <see cref="OrderStateEnum"/></param>
        /// <param name="tag">If given, only orders with this tag are returned</param>
        /// <param name="minPayDate">Minimum date, where the payment occured</param>
        /// <param name="maxPayDate">Maximum date, where the payment occured</param>
        /// <param name="includePositions">Should the invoice data contain all invoice positions?</param>
        /// <param name="excludeTags">Defines, that no tags should be supplied</param>
        /// <returns></returns>
        ApiResult<List<InvoiceDetail>> GetInvoiceList(
            DateTime? minInvoiceDate = null,
            DateTime? maxInvoiceDate = null,
            int page = 1, int pageSize = 50,
            List<long> shopId = null,
            List<int> orderStateId = null,
            List<string> tag = null,
            DateTime? minPayDate = null,
            DateTime? maxPayDate = null,
            bool includePositions = false,
            bool excludeTags = false);

        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="order">An order object, to create in billbee</param>
        /// <param name="shopId">The id of the shop. Necessary, to attach an order directly to a shop connection</param>
        /// <returns></returns>
        ApiResult<OrderResult> PostNewOrder(Order order, long shopId);

        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="order">An order object, to create in billbee</param>
        /// <returns></returns>
        ApiResult<Order> PostNewOrder(Order order);

        /// <summary>
        /// Reset the tags on an order and add the given ones.
        /// All previuosly added tags will be deleted.
        /// </summary>
        /// <param name="tags">Tasg to add</param>
        /// <param name="orderId">Id of the order, where the tags should be edited.</param>
        /// <returns>ApiResult with the result of the update operation</returns>
        ApiResult<object> AddTags(List<string> tags, long orderId);

        /// <summary>
        /// Reset the tags on an order and add the given ones.
        /// All previuosly added tags will be deleted.
        /// </summary>
        /// <param name="tags">Tasg to add</param>
        /// <param name="orderId">Id of the order, where the tags should be edited.</param>
        /// <returns>ApiResult with the result of the update operation</returns>
        ApiResult<object> UpdateTags(List<string> tags, long orderId);

        /// <summary>
        /// Add a shipment, that was created in an external system to an order.
        /// </summary>
        /// <param name="shipment"></param>
        void AddShipment(OrderShipment shipment);

        /// <summary>
        /// Requests the delivery note information for the given order.
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="includePdf">Should the pdf file be included in the response?</param>
        /// /// <param name="sendToCloudId">If set, the document will be uploaded to the given cloud storage after creation</param>
        /// <returns>The delivery note information, if the the order was found and an delivery note was already created.</returns>
        ApiResult<DeliveryNote> CreateDeliveryNote(long orderId, bool includePdf = false, long? sendToCloudId = null);

        /// <summary>
        /// Requests the invoice document for the given order.
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="includePdf">Should the pdf file be included in the response?</param>
        /// <param name="sendToCloudId">If set, the invoice will be uploaded to the given cloud storage after creation</param>
        /// <param name="templateId">Id set, the given template will be used for creation</param>
        /// <returns>The invoice data, if the the order was found and an invoice was already created.</returns>
        ApiResult<Invoice> CreateInvoice(long orderId, bool includePdf = false, long? templateId = null, long? sendToCloudId = null);

        /// <summary>
        /// Changes the main state of a single order
        /// </summary>
        /// <param name="id">The internal id of the order</param>
        /// <param name="state">The data used to change the state</param>
        void ChangeOrderState(long id, OrderStateEnum state);

        /// <summary>
        /// Sends a message to a customer of a specific order.
        /// </summary>
        /// <param name="orderId">Id of the order, this message should be attached to.</param>
        /// <param name="message">The content of the message</param>
        void SendMailForOrder(long orderId, SendMessage message);

        /// <summary>
        /// Triggers a rule event
        /// </summary>
        /// <param name="orderId">Order to trigger the event for</param>
        /// <param name="eventName">Name of the event to trigger</param>
        /// <param name="delayInMinutes">If set, the trigger will by delayed for the given number of minutes</param>
        void CreateEventAtOrder(long orderId, string eventName, uint delayInMinutes = 0);

        /// <summary>
        /// Gets a list of layout templates
        /// </summary>
        /// <returns>The list of layout templates</returns>
        ApiResult<List<LayoutTemplate>> GetLayouts();

        /// <summary>
        /// Parses a text and replaces all placeholders
        /// </summary>
        /// <param name="orderId">The id of the order to parse</param>
        /// <param name="parsePlaceholdersQuery">The text to be parsed</param>
        /// <returns>The parsed text</returns>
        ParsePlaceholdersResult ParsePlaceholders(long orderId, ParsePlaceholdersQuery parsePlaceholdersQuery);


        /// <summary>
        /// Adds a message to the specified order
        /// </summary>
        /// <param name="orderId">The id of the order to add the message</param>
        /// <param name="orderMessage">The content of the Order-Message</param>
        void AddMessage(long orderId, OrderMessage orderMessage);

    }
}