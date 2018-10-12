using Billbee.Api.Client.Model;
using System.Collections.Generic;

namespace Billbee.Api.Client.EndPoint
{
    /// <summary>
    /// Endpoint to register and deregister webhooks
    /// </summary>
    public class WebhookEndPoint : RestClientBaseClass
    {

        public WebhookEndPoint(ApiConfiguration config, ILogger logger) : base(logger, config)
        {
        }

        /// <summary>
        /// Deletes all existing WebHook registrations
        /// </summary>
        public void DeleteAllWebhooks()
        {
            delete("/webhooks");
        }

        /// <summary>
        /// Deletes one webhook, identified by the given id.
        /// </summary>
        /// <param name="id">Id of the webhook to delete.</param>
        public void Deletewebhook(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new InvalidValueException($"Property Id was not set.");
            }

            delete($"/webhooks/{id}");
        }

        /// <summary>
        /// Gets all registered webhooks for this account
        /// </summary>
        /// <returns>List of all registered webhooks.</returns>
        public List<Webhook> GetWebhooks()
        {
            return requestResource<List<Webhook>>("/webhooks");
        }

        /// <summary>
        /// Gets the webhook with the corresponding id
        /// </summary>
        /// <param name="id">id of the given webhook</param>
        /// <returns>The webhook itself, if the given id could be found.</returns>
        public Webhook GetWebhook(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new InvalidValueException($"Property Id was not set.");
            }

            return requestResource<Webhook>($"/webhooks/{id}");
        }

        /// <summary>
        /// Updates the information of a webhook
        /// </summary>
        /// <param name="webhook">The complete information of the hook, that should be updated. The webhook to update is identified by the Id parameter.</param>
        public void UpdateWebhook(Webhook webhook)
        {
            if (string.IsNullOrWhiteSpace(webhook.Id ))
            {
                throw new InvalidValueException($"Property Id was not set.");
            }

            put($"/webhooks/{webhook.Id}", webhook);

        }

        /// <summary>
        /// Queries a list of all usable filters for webhook registration.
        /// </summary>
        /// <returns>Dictionary of all registered and usable filter.</returns>
        public List<WebhookFilter> GetFilters()
        {
            return requestResource<List<WebhookFilter>>("/webhooks/filters");
        }

        /// <summary>
        /// Registers a new webhook with the given information
        /// </summary>
        /// <param name="webhook">The details of the webhook to register. The property Id must be null.</param>
        /// <returns>The Id of the registered webhook</returns>
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
