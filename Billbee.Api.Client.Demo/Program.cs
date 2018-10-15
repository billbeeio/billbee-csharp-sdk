using Billbee.Api.Client.Enums;
using Billbee.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Billbee.Api.Client.Demo
{
    /// <summary>
    /// Demo project to show the basic functionality of the Billbee .NET API SDK
    /// 
    /// To use this demo, you have to enable the API in your account. Please refer to https://www.billbee.de/api/ for further information.
    /// </summary>
    internal class Program
    {
        private static int Main()
        {
            #region Initialization

            // Creating an individual logger, that implements ILogger
            ILogger logger = new Logger();

            // Creating new instance of ApiClient           
            string configPath = "config.json";
            ApiClient client = null;

            if (File.Exists(configPath))
            {
                // From config file
                client = new ApiClient(configPath, logger: logger);
            }
            else
            {
                // from naual given config        
                client = new ApiClient(logger: logger);

                // Enter your api key here. If you don't have an api key. Please contact support@billbee.de with a description on what you would like to do, to get one.
                client.Configuration.ApiKey = "";
                // Enter the username of your main account here.
                client.Configuration.Username = "";
                // Enter the password of your api here.
                client.Configuration.Password = "";
            }

            // Test the configuration
            if (client.TestConfiguration())
            {
                logger.LogMsg("Api test successful", LogSeverity.Info);
            }
            else
            {
                logger.LogMsg("Api test failed. Please control your configuration", LogSeverity.Error);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return 1;
            }

            #endregion

            #region Example calls

            // Getting all available webhook filters
            var webhookFilters = client.Webhooks.GetFilters();

            // Registering a webhook for a new order
            client.Webhooks.CreateWebhook(new Webhook
            {
                Id = null,
                WebHookUri = "https://webhook.site/5290627f-b5e3-4123-a715-26a721054617?noecho",
                Secret = "4e4451af-63c5-44f4-a3c5-1dcf8617fc5c",
                Description = "A simple description",
                IsPaused = true,
                Filters = new List<string> { "order.created" },
                Headers = new Dictionary<string, string> { { "TestHeader", "TestHeaderValue" }, { "Another Testheader", "Another Value" } },
                Properties = new Dictionary<string, object>()
            });


            // Requesting webhooks
            var webHooks = client.Webhooks.GetWebhooks();

            // Requesting a specific webhook
            var webhook = client.Webhooks.GetWebhook(webHooks.FirstOrDefault().Id);

            // Updating webhook
            webhook.IsPaused = false;
            client.Webhooks.UpdateWebhook(webhook);

            // Artificial brake to prevent throttling
            Thread.Sleep(1000);

            // Deleting webhook
            client.Webhooks.Deletewebhook(webhook.Id);

            // Deleting all webhooks
            client.Webhooks.DeleteAllWebhooks();

            // Artificial brake to prevent throttling
            Thread.Sleep(1000);

            // Requesting Custom Product fields
            var customFields = client.Products.GetCustomFields(1, 50);

            if (customFields.Data.Count > 0)
            {
                var firstCustomField = client.Products.GetCustomField(customFields.Data.First().Id.Value);
            }

            // Artificial brake to prevent throttling
            Thread.Sleep(1000);

            var patchableFields = client.Products.GetPatchableProductFields();

            var patchedProduct = client.Products.PatchArticle(5617463, new Dictionary<string, string> { { "EAN", "0815" } });

            // Artificial brake to prevent throttling
            Thread.Sleep(1000);

            var customers = client.Customer.GetCustomerList(1, 50);

            client.Customer.AddCustomer(new CustomerForCreation { Name = "Max Mustermann", Email = "max@mustermann.de", Address = new CustomerAddress { FirstName = "Max", LastName = "Mustermann", Street = "Mustergasse", Housenumber = "1", Zip = "12345", City = "Musterhausen", AddressType = 1, CountryCode = "DE" } });

            // Artificial brake to prevent throttling
            Thread.Sleep(1000);

            if (customers.Data.Count > 0)
            {
                var customer = client.Customer.GetCustomer(customers.Data.First().Id.Value);

                customer.Data.Name = "Tobias Tester";

                customer = client.Customer.UpdateCustomer(customer.Data);

                // Artificial brake to prevent throttling
                Thread.Sleep(1000);

                var customerOrder = client.Customer.GetOrdersForCustomer(customer.Data.Id.Value, 1, 50);
                var customerAddresses = client.Customer.GetAddressesForCustomer(customer.Data.Id.Value, 1, 50);
            }

            // Artificial brake to prevent throttling
            Thread.Sleep(1000);

            // Getting events for this account
            var events = client.Events.GetEvents();

            // Getting my shipping providers
            var shippingProvider = client.Shipment.GetShippingProvider();

            // Doing some stock manipulations
            Console.WriteLine(
                "Please enter one SKU of an article to update it's stock. Be aware, that these changes are permanent, so better use a demo article. Leave blank to skip.");
            var sku = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(sku))
            {
                var updateStockCodeResult =
                    client.Products.UpdateStockCode(
                        new UpdateStockCode { Sku = sku, StockCode = "Testlager" });
                var updateStockResult = client.Products.UpdateStock(
                    new UpdateStock
                    {
                        Sku = sku,
                        NewQuantity = 15,
                        Reason = "Change due to api tests."
                    });
                var updateStockMultipleResult = client.Products.UpdateStockMultiple(
                    new List<UpdateStock>
                    {
                        new UpdateStock {Sku = sku, NewQuantity = 15},
                        new UpdateStock {Sku = "4712", NewQuantity = 23}
                    });
            }

            // Requesting the urls of the terms and conditions for the usage of billbee.
            var termsAndConditions = client.AutomaticProvision.TermsInfo();

            // Getting a list of all orders with order state 'confirmed'
            var orders = client.Orders.GetOrderList(page: 1, pageSize: 20,
                orderStateId: new List<OrderStateEnum> { OrderStateEnum.Bestaetigt });
            // var x = client.Orders.GetInvoiceList();

            // Example to create a new order. Please create a complete order object for usage.
            // var createOrderResult = client.Orders.PostNewOrder(new Billbee.Api.Client.Model.Order() { });

            Console.WriteLine(
                "Please enter one order number for further test manipulations. Be aware, that these changes are permanent. Please use an demo order. Leave blank to skip.");
            var orderId = Console.ReadLine();
            int orderIdInt;
            if (!string.IsNullOrWhiteSpace(orderId) && int.TryParse(orderId, out orderIdInt))
            {
                // Remove all old tags and add the given ones.
                var updateTagsResult = client.Orders.UpdateTags(new List<string>() { "Test C", "Test D" }, orderIdInt);

                // Add new tags.
                var addTagsResult = client.Orders.AddTags(new List<string>() { "Test A", "Test B" }, orderIdInt);
                // client.Orders.AddShipment(new OrderShipment { Comment = "Test", OrderId = orderIdInt, ShippingId = shippingId, ShippingProviderId = providerId, ShippingProviderProductId = productId });

                // Getting documents
                var deliveryNoteResult = client.Orders.CreateDeliveryNote(orderIdInt, true);
                var invoiceResult = client.Orders.CreateInvoice(orderIdInt, true);
            }

            #endregion

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            return 0;
        }
    }
}
