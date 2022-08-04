using System.Collections.Generic;
using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.Endpoint.Interfaces
{
    /// <summary>
    /// Endpoint to register and deregister webhooks
    /// </summary>
    public interface IWebhookEndPoint
    {
        /// <summary>
        /// Deletes all existing WebHook registrations
        /// </summary>
        void DeleteAllWebhooks();

        /// <summary>
        /// Deletes one webhook, identified by the given id.
        /// </summary>
        /// <param name="id">Id of the webhook to delete.</param>
        void DeleteWebhook(string id);

        /// <summary>
        /// Gets all registered webhooks for this account
        /// </summary>
        /// <returns>List of all registered webhooks.</returns>
        List<Webhook> GetWebhooks();

        /// <summary>
        /// Gets the webhook with the corresponding id
        /// </summary>
        /// <param name="id">id of the given webhook</param>
        /// <returns>The webhook itself, if the given id could be found.</returns>
        Webhook GetWebhook(string id);

        /// <summary>
        /// Updates the information of a webhook
        /// </summary>
        /// <param name="webhook">The complete information of the hook, that should be updated. The webhook to update is identified by the Id parameter.</param>
        void UpdateWebhook(Webhook webhook);

        /// <summary>
        /// Queries a list of all usable filters for webhook registration.
        /// </summary>
        /// <returns>Dictionary of all registered and usable filter.</returns>
        List<WebhookFilter> GetFilters();

        /// <summary>
        /// Registers a new webhook with the given information
        /// </summary>
        /// <param name="webhook">The details of the webhook to register. The property Id must be null.</param>
        /// <returns>The registered webhook</returns>
        Webhook CreateWebhook(Webhook webhook);
    }
}