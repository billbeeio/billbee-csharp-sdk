using Billbee.Api.Client.Model;
using System.Collections.Generic;
using Billbee.Api.Client.Endpoint.Interfaces;

namespace Billbee.Api.Client.EndPoint
{
    /// <inheritdoc cref="Billbee.Api.Client.Endpoint.Interfaces.IWebhookEndPoint" />
    public class WebhookEndPoint : RestClientBaseClass, IWebhookEndPoint
    {
        public WebhookEndPoint(ApiConfiguration config, ILogger logger) : base(logger, config)
        {
        }

        public void DeleteAllWebhooks()
        {
            delete("/webhooks");
        }

        public void DeleteWebhook(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new InvalidValueException($"Property Id was not set.");
            }

            delete($"/webhooks/{id}");
        }

        public List<Webhook> GetWebhooks()
        {
            return requestResource<List<Webhook>>("/webhooks");
        }

        public Webhook GetWebhook(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new InvalidValueException($"Property Id was not set.");
            }

            return requestResource<Webhook>($"/webhooks/{id}");
        }

        public void UpdateWebhook(Webhook webhook)
        {
            if (string.IsNullOrWhiteSpace(webhook.Id ))
            {
                throw new InvalidValueException($"Property Id was not set.");
            }

            put($"/webhooks/{webhook.Id}", webhook);

        }

        public List<WebhookFilter> GetFilters()
        {
            return requestResource<List<WebhookFilter>>("/webhooks/filters");
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

            post("/webhooks", webhook);
        }
    }
}
