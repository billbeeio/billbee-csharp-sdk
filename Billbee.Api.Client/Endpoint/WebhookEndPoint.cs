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

        public void DeleteAllWebhooks()
        {
            _restClient.Delete("/webhooks");
        }

        public void DeleteWebhook(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new InvalidValueException($"Property Id was not set.");
            }

            _restClient.Delete($"/webhooks/{id}");
        }

        public List<Webhook> GetWebhooks()
        {
            return _restClient.Get<List<Webhook>>("/webhooks");
        }

        public Webhook GetWebhook(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new InvalidValueException($"Property Id was not set.");
            }

            return _restClient.Get<Webhook>($"/webhooks/{id}");
        }

        public void UpdateWebhook(Webhook webhook)
        {
            if (string.IsNullOrWhiteSpace(webhook.Id ))
            {
                throw new InvalidValueException($"Property Id was not set.");
            }

            _restClient.Put($"/webhooks/{webhook.Id}", webhook);
        }

        public List<WebhookFilter> GetFilters()
        {
            return _restClient.Get<List<WebhookFilter>>("/webhooks/filters");
        }

        public void CreateWebhook(Webhook webhook)
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
