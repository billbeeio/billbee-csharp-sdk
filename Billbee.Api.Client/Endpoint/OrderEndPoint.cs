using Billbee.Api.Client.Enums;
using Billbee.Api.Client.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Billbee.Api.Client.Endpoint.Interfaces;

namespace Billbee.Api.Client.EndPoint
{
    /// <inheritdoc cref="Billbee.Api.Client.Endpoint.Interfaces.IOrderEndPoint" />
    public class OrderEndPoint : IOrderEndPoint
    {
        private readonly IBillbeeRestClient _restClient;

        internal OrderEndPoint(IBillbeeRestClient restClient)
        {
            _restClient = restClient;
        }

        public ApiResult<Order> GetOrder(string id, int articleTitleSource = 0)
        {
            return _restClient.Get<ApiResult<Order>>($"/orders/{id}?articleTitleSource={articleTitleSource}");
        }

        public ApiResult<List<string>> GetPatchableFields()
        {
            return _restClient.Get<ApiResult<List<string>>>("/orders/PatchableFields");
        }

        public ApiResult<object> PatchOrder(long id, Dictionary<string, object> fieldsToPatch)
        {
            JObject obj = new JObject();
            foreach (var keyElement in fieldsToPatch)
            {

                JProperty prop = new JProperty(keyElement.Key, keyElement.Value);
                obj.Add(prop);
            }


            return _restClient.Patch<ApiResult<object>>($"/orders/{id}", null, obj);
        }

        public ApiResult<Order> GetOrderByExternalReference(string id)
        {
            return _restClient.Get<ApiResult<Order>>($"/orders/findbyextref/{id}");
        }

        public ApiResult<Order> GetOrderByExternalIdAndPartner(string partner, string id)
        {
            return _restClient.Get<ApiResult<Order>>($"/orders/find/{id}/{partner}");
        }

        public ApiPagedResult<List<Order>> GetOrderList(DateTime? minOrderDate = null,
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

            return _restClient.Get<ApiPagedResult<List<Order>>>("/orders", parameters);
        }

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

            return _restClient.Get<ApiResult<List<InvoiceDetail>>>("/orders/invoices", parameters);
        }

        public ApiResult<OrderResult> PostNewOrder(Order order, long shopId)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("shopId", shopId.ToString());

            return _restClient.Post<ApiResult<OrderResult>>("/orders", order, parameters);
        }

        public ApiResult<dynamic> AddTags(List<string> tags, long orderId)
        {
            return _restClient.Post<ApiResult<dynamic>>($"/orders/{orderId}/tags", new { Tags = tags });
        }

        public ApiResult<dynamic> UpdateTags(List<string> tags, long orderId)
        {
            return _restClient.Put<ApiResult<dynamic>>($"/orders/{orderId}/tags", new { Tags = tags });
        }
        
        public void AddShipment(OrderShipment shipment)
        {
            _restClient.Post($"/orders/{shipment.OrderId}/shipment", shipment);
        }

        public ApiResult<DeliveryNote> CreateDeliveryNote(long orderId, bool includePdf = false, long? sendToCloudId = null)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("includePdf", includePdf.ToString());

            if (sendToCloudId.HasValue)
                parameters.Add("sendToCloudId", sendToCloudId.ToString());

            return _restClient.Post<ApiResult<DeliveryNote>>($"/orders/CreateDeliveryNote/{orderId}", parameters);
        }

        public ApiResult<Invoice> CreateInvoice(long orderId, bool includePdf = false, long? templateId = null, long? sendToCloudId = null)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("includeInvoicePdf", includePdf.ToString());

            if (sendToCloudId.HasValue)
                parameters.Add("sendToCloudId", sendToCloudId.ToString());

            if (templateId.HasValue)
                parameters.Add("templateId", templateId.ToString());

            return _restClient.Post<ApiResult<Invoice>>($"/orders/CreateInvoice/{orderId}", parameters);
        }

        public void ChangeOrderState(long id, OrderStateEnum state)
        {
            _restClient.Put<object>($"/orders/{id}/orderstate", new { NewStateId = (int)state }, null);
        }

        public void SendMailForOrder(long orderId, SendMessage message)
        {
            _restClient.Post($"/orders/{orderId}/send-message", message);
        }

        public void CreateEventAtOrder(long orderId, string eventName, uint delayInMinutes = 0)
        {
            var model = new TriggerEventContainer
            {
                DelayInMinutes = delayInMinutes,
                Name = eventName
            };

            _restClient.Post($"/orders/{orderId}/trigger-event", model);
        }
    }
}
