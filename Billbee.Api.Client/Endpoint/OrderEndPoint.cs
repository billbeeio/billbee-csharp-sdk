using Billbee.Api.Client.Enums;
using Billbee.Api.Client.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Billbee.Api.Client.EndPoint
{
    /// <summary>
    /// EndPoint to access order functions
    /// </summary>
    public class OrderEndPoint : RestClientBaseClass
    {
        internal OrderEndPoint(ApiConfiguration config, ILogger logger = null) : base(logger, config)
        {
        }

        /// <summary>
        /// Selects an order by it's id
        /// </summary>
        /// <param name="id">id to search for</param>
        /// <param name="articleTitleSource">The source field for the article title. 0 = Order Position (default), 1 = Article Title, 2 = Article Invoice Text</param>
        /// <returns>Details of the order</returns>
        public ApiResult<Order> GetOrder(string id, int articleTitleSource = 0)
        {
            return requestResource<ApiResult<Order>>($"/orders/{id}?articleTitleSource={articleTitleSource}");
        }

        /// <summary>
        /// Gets a list of all fileds, that can be patched at an order.
        /// </summary>
        /// <returns>List of patchable fields.</returns>
        public ApiResult<List<string>> GetPatchableFields()
        {
            return requestResource<ApiResult<List<string>>>("/orders/PatchableFields");
        }

        /// <summary>
        /// Patches one or more fields of an order.
        /// </summary>
        /// <param name="id">The id of the order to patch.</param>
        /// <param name="FieldsToPatch"></param>
        /// <returns></returns>
        public ApiResult<object> PatchOrder(long id, Dictionary<string, object> FieldsToPatch)
        {
            JObject obj = new JObject();
            foreach (var keyElement in FieldsToPatch)
            {

                JProperty prop = new JProperty(keyElement.Key, keyElement.Value);
                obj.Add(prop);
            }


            return patch<ApiResult<object>>($"/orders/{id}", null, obj);
        }

        /// <summary>
        /// Selects an order by it's external id
        /// </summary>
        /// <param name="id">The external id, that should be used for selection</param>
        /// <returns>Details of the order</returns>
        public ApiResult<Order> GetOrderByExternalReference(string id)
        {
            return requestResource<ApiResult<Order>>($"/orders/findbyextref/{id}");
        }

        /// <summary>
        /// Selects an order by it's external id and its partner
        /// </summary>
        /// <param name="partner">If set, this is the internal partner name, this order should be searched at.</param>
        /// <param name="id">The external id, that should be used for selection</param>
        /// <returns>Details of the order</returns>
        public ApiResult<Order> GetOrderByExternalIdAndPartner(string partner, string id)
        {
            return requestResource<ApiResult<Order>>($"/orders/find/{id}/{partner}");
        }

        /// <summary>
        /// Delivers a list of orders
        /// </summary>
        /// <param name="minOrderDate">Minimum date of the order</param>
        /// <param name="maxOrderDate">Maximum date of the order</param>
        /// <param name="page">Selected page</param>
        /// <param name="pageSize">Selected number of entries per page</param>
        /// <param name="shopId">Id of the shop, the order belongs to</param>
        /// <param name="orderStateId">State of the order.<<see cref="OrderStateEnum"/></param>
        /// <param name="tag">If given, only orders with this tag are returned</param>
        /// <param name="minimumBillBeeOrderId">Minimum internal id of the order. As ids are in squence, this can be used to get only orders, not already imported.</param>
        /// <param name="modifiedAtMin">Minimum date of last modification</param>
        /// <param name="modifiedAtMax">Maximum date of last modification</param>
        /// <param name="excludeTags">Defines, that no tags should be supplied</param>
        /// <returns></returns>
        public ApiResult<List<Order>> GetOrderList(DateTime? minOrderDate = null,
            DateTime? maxOrderDate = null,
            int page = 1,
            int pageSize = 50,
            List<long> shopId = null,
            List<OrderStateEnum> orderStateId = null,
            List<string> tag = null,
            long? minimumBillBeeOrderId = null,
            DateTime? modifiedAtMin = null,
            DateTime? modifiedAtMax = null,
            bool excludeTags = false)
        {
            NameValueCollection parameters = new NameValueCollection();

            if (minOrderDate != null)
            {
                parameters.Add("minOrderDate", minOrderDate.Value.ToString("yyyy-MM-dd HH:mm"));
            }

            if (maxOrderDate != null)
            {
                parameters.Add("maxOrderDate", maxOrderDate.Value.ToString("yyyy-MM-dd HH:mm"));
            }

            if (modifiedAtMin != null)
            {
                parameters.Add("modifiedAtMin", modifiedAtMin.Value.ToString("yyyy-MM-dd HH:mm"));
            }

            if (modifiedAtMax != null)
            {
                parameters.Add("modifiedAtMax", modifiedAtMax.Value.ToString("yyyy-MM-dd HH:mm"));
            }

            if (minimumBillBeeOrderId != null)
            {
                parameters.Add("minimumBillBeeOrderId", minimumBillBeeOrderId.ToString());
            }

            if (shopId != null)
            {
                int i = 0;
                foreach (var id in shopId)
                {
                    parameters.Add($"shopId[{i++}]", id.ToString());
                }
            }

            if (tag != null)
            {
                int i = 0;
                foreach (var id in tag)
                {
                    parameters.Add($"tag[{i++}]", id.ToString());
                }
            }

            if (orderStateId != null)
            {
                int i = 0;
                foreach (var id in orderStateId)
                {
                    parameters.Add($"orderStateId[{i++}]", ((int)id).ToString());
                }
            }

            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            parameters.Add("excludeTags", excludeTags.ToString());

            return requestResource<ApiResult<List<Order>>>("/orders", parameters);
        }

        /// <summary>
        /// Delivers a list of invoices
        /// </summary>
        /// <param name="minInvoiceDate">Minimum date of the invoice</param>
        /// <param name="maxInvoiceDate">Maximum date of the invoice</param>
        /// <param name="page">Selected page</param>
        /// <param name="pageSize">Selected number of entries per page</param>
        /// <param name="shopId">Id of the shop, the order belongs to</param>
        /// <param name="orderStateId">State of the order. <<see cref="OrderStateEnum"/></param>
        /// <param name="tag">If given, only orders with this tag are returned</param>
        /// <param name="minPayDate">Minimum date, where the payment occured</param>
        /// <param name="maxPayDate">Maximum date, where the payment occured</param>
        /// <param name="includePositions">Should the invoice data contain all invoice positions?</param>
        /// <param name="excludeTags">Defines, that no tags should be supplied</param>
        /// <returns></returns>
        public ApiResult<List<InvoiceDetail>> GetInvoiceList(
            DateTime? minInvoiceDate = null,
            DateTime? maxInvoiceDate = null,
            int page = 1, int pageSize = 50,
            List<long> shopId = null,
            List<int> orderStateId = null,
            List<string> tag = null,
            DateTime? minPayDate = null,
            DateTime? maxPayDate = null,
            bool includePositions = false,
            bool excludeTags = false)
        {
            NameValueCollection parameters = new NameValueCollection();

            if (minInvoiceDate != null)
            {
                parameters.Add("minInvoiceDate", minInvoiceDate.Value.ToString("yyyy-MM-dd HH:mm"));
            }

            if (maxInvoiceDate != null)
            {
                parameters.Add("maxInvoiceDate", maxInvoiceDate.Value.ToString("yyyy-MM-dd HH:mm"));
            }

            if (minPayDate != null)
            {
                parameters.Add("minPayDate", minPayDate.Value.ToString("yyyy-MM-dd HH:mm"));
            }

            if (maxPayDate != null)
            {
                parameters.Add("maxPayDate", maxPayDate.Value.ToString("yyyy-MM-dd HH:mm"));
            }

            if (shopId != null)
            {
                int i = 0;
                foreach (var id in shopId)
                {
                    parameters.Add($"shopId[{i++}]", id.ToString());
                }
            }

            if (tag != null)
            {
                int i = 0;
                foreach (var id in tag)
                {
                    parameters.Add($"tag[{i++}]", id.ToString());
                }
            }

            if (orderStateId != null)
            {
                int i = 0;
                foreach (var id in orderStateId)
                {
                    parameters.Add($"orderStateId[{i++}]", id.ToString());
                }
            }


            parameters.Add("includePositions", includePositions.ToString());
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            parameters.Add("excludeTags", excludeTags.ToString());

            return requestResource<ApiResult<List<InvoiceDetail>>>("/orders/invoices", parameters);
        }

        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="order">An order object, to create in billbee</param>
        /// <param name="shopId">The id of the shop. Neccessary, to attach an order directly to a shop connection</param>
        /// <returns></returns>
        public ApiResult<OrderResult> PostNewOrder(Order order, long shopId)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("shopId", shopId.ToString());

            return post<ApiResult<OrderResult>>("/orders", order, parameters);
        }

        /// <summary>
        /// Reset the tags on an order and add the given ones.
        /// All previuosly added tags will be deleted.
        /// </summary>
        /// <param name="tags">Tasg to add</param>
        /// <param name="orderId">Id of the order, where the tags should be edited.</param>
        /// <returns>ApiResult with the result of the update operation</returns>
        public ApiResult<dynamic> AddTags(List<string> tags, long orderId)
        {
            return post<ApiResult<dynamic>>($"/orders/{orderId}/tags", new { Tags = tags });
        }

        /// <summary>
        /// Reset the tags on an order and add the given ones.
        /// All previuosly added tags will be deleted.
        /// </summary>
        /// <param name="tags">Tasg to add</param>
        /// <param name="orderId">Id of the order, where the tags should be edited.</param>
        /// <returns>ApiResult with the result of the update operation</returns>
        public ApiResult<dynamic> UpdateTags(List<string> tags, long orderId)
        {
            return put<ApiResult<dynamic>>($"/orders/{orderId}/tags", new { Tags = tags });
        }

        /// <summary>
        /// Add a shipment, that was created in an external system to an order.
        /// </summary>
        /// <param name="shipment"></param>
        public void AddShipment(OrderShipment shipment)
        {
            post($"/orders/{shipment.OrderId}/shipment", shipment);
        }

        /// <summary>
        /// Requests the delivery note information for the given order.
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="includePdf">Should the pdf file be included in the response?</param>
        /// /// <param name="sendToCloudId">If set, the document will be uploaded to the given cloud storage after creation</param>
        /// <returns>The delivery note information, if the the order was found and an delivery note was already created.</returns>
        public ApiResult<DeliveryNote> CreateDeliveryNote(long orderId, bool includePdf = false, long? sendToCloudId = null)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("includePdf", includePdf.ToString());

            if (sendToCloudId.HasValue)
                parameters.Add("sendToCloudId", sendToCloudId.ToString());

            return post<ApiResult<DeliveryNote>>($"/orders/CreateDeliveryNote/{orderId}", parameters);
        }

        /// <summary>
        /// Requests the invoice document for the given order.
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="includePdf">Should the pdf file be included in the response?</param>
        /// <param name="sendToCloudId">If set, the invoice will be uploaded to the given cloud storage after creation</param>
        /// <param name="templateId">Id set, the given template will be used for creation</param>
        /// <returns>The invoice data, if the the order was found and an invoice was already created.</returns>
        public ApiResult<Invoice> CreateInvoice(long orderId, bool includePdf = false, long? templateId = null, long? sendToCloudId = null)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("includeInvoicePdf", includePdf.ToString());

            if (sendToCloudId.HasValue)
                parameters.Add("sendToCloudId", sendToCloudId.ToString());

            if (templateId.HasValue)
                parameters.Add("templateId", templateId.ToString());

            return post<ApiResult<Invoice>>($"/orders/CreateInvoice/{orderId}", parameters);
        }

        /// <summary>
        /// Changes the main state of a single order
        /// </summary>
        /// <param name="id">The internal id of the order</param>
        /// <param name="state">The data used to change the state</param>
        public void ChangeOrderState(long id, OrderStateEnum state)
        {
            put<object>($"/orders/{id}/orderstate", new { NewStateId = (int)state }, null);
        }

        /// <summary>
        /// Sends a message to a customer of a specific order.
        /// </summary>
        /// <param name="orderId">Id of the order, this message should be attached to.</param>
        /// <param name="message">The contsnt of the message</param>
        public void SendMailForOrder(long orderId, SendMessage message)
        {
            post($"/orders/{orderId}/send-message", message);
        }

        /// <summary>
        /// Triggers a rule event
        /// </summary>
        /// <param name="orderId">Order to trigger the event for</param>
        /// <param name="eventName">Name of the event to trigger</param>
        /// <param name="delayInMinutes">If set, the trigger will by delayed for the given number of minutes</param>
        public void CreateEventAtOrder(long orderId, string eventName, uint delayInMinutes = 0)
        {
            var model = new TriggerEventContainer
            {
                DelayInMinutes = delayInMinutes,
                Name = eventName
            };

            post($"/orders/{orderId}/trigger-event", model);
        }
    }
}
