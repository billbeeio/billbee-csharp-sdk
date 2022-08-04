using Billbee.Api.Client.Model;
using System.Collections.Generic;
using Billbee.Api.Client.Endpoint.Interfaces;

namespace Billbee.Api.Client.EndPoint
{
    /// <inheritdoc cref="Billbee.Api.Client.Endpoint.Interfaces.IWebhookEndPoint" />
    public class WebhookEndPoint : IWebhookEndPoint
    {
        private readonly IBillbeeRestClient _restClient;

        internal WebhookEndPoint(IBillbeeRestClient restClient)
        {
            _restClient = restClient;
        }

        [ApiMapping("/api/v1/webhooks", HttpOperation.Delete)]
        public void DeleteAllWebhooks()
        {
            _restClient.Delete("/webhooks");
        }

        [ApiMapping("/api/v1/webhooks/{id}", HttpOperation.Delete)]
        public void DeleteWebhook(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new InvalidValueException($"Property Id was not set.");
            }

            _restClient.Delete($"/webhooks/{id}");
        }

        [ApiMapping("/api/v1/webhooks", HttpOperation.Get)]
        public List<Webhook> GetWebhooks()
        {
            return _restClient.Get<List<Webhook>>("/webhooks");
        }

        [ApiMapping("/api/v1/webhooks/{id}", HttpOperation.Get)]
        public Webhook GetWebhook(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new InvalidValueException($"Property Id was not set.");
            }

            return _restClient.Get<Webhook>($"/webhooks/{id}");
        }

        [ApiMapping("/api/v1/webhooks/{id}", HttpOperation.Put)]
        public void UpdateWebhook(Webhook webhook)
        {
            if (string.IsNullOrWhiteSpace(webhook.Id ))
            {
                throw new InvalidValueException($"Property Id was not set.");
            }

            _restClient.Put($"/webhooks/{webhook.Id}", webhook);
        }

        [ApiMapping("/api/v1/webhooks/filters", HttpOperation.Get)]
        public List<WebhookFilter> GetFilters()
        {
            return _restClient.Get<List<WebhookFilter>>("/webhooks/filters");
        }

        [ApiMapping("/api/v1/webhooks", HttpOperation.Post)]
        public Webhook CreateWebhook(Webhook webhook)
        {
            if (webhook.Id != null)
            {
                throw new InvalidValueException($"Property Id was set to '{webhook.Id}', but it must be null.");
            }

            if ( string.IsNullOrWhiteSpace(webhook.Secret) || webhook.Secret.Length < 32 || webhook.Secret.Length > 64)
            {
                throw new InvalidValueException($"Property secret is malformed. It must meet the following criteria: Not null or whitespaces only, between 32 and 64 charackters long.");
            }

            _restClient.Post("/webhooks", webhook);
        }
    }
}
