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
            Console.WriteLine("Beware, uncommenting lines may harm your productive data!");
            Console.WriteLine("Abort if you are not sure to proceed or press any key to continue.");
            Console.ReadKey();


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

            #region webhooks

            // Getting all available webhook filters
            var webhookFilters = client.Webhooks.GetFilters();

            // Registering a webhook for a new order
            //client.Webhooks.CreateWebhook(new Webhook
            //{
            //    Id = null,
            //    WebHookUri = "https://webhook.site/5290627f-b5e3-4123-a715-26a721054617?noecho",
            //    Secret = "4e4451af-63c5-44f4-a3c5-1dcf8617fc5c",
            //    Description = "A simple description",
            //    IsPaused = true,
            //    Filters = new List<string> { "order.created" },
            //    Headers = new Dictionary<string, string> { { "TestHeader", "TestHeaderValue" }, { "Another Testheader", "Another Value" } },
            //    Properties = new Dictionary<string, object>()
            //});


            // Requesting webhooks
            var webHooks = client.Webhooks.GetWebhooks();

            // Requesting a specific webhook
            if (webHooks.Count > 0)
            {
                var webhook = client.Webhooks.GetWebhook(webHooks.FirstOrDefault().Id);

                // Updating webhook
                webhook.IsPaused = false;
                //client.Webhooks.UpdateWebhook(webhook);

                // Artificial brake to prevent throttling
                Thread.Sleep(1000);

                // Deleting webhook
                //client.Webhooks.Deletewebhook(webhook.Id);
            }

            // Deleting all webhooks
            //client.Webhooks.DeleteAllWebhooks();

            // Artificial brake to prevent throttling
            Thread.Sleep(1000);
            #endregion

            #region custom fields
            // Requesting Custom Product fields
            var customFields = client.Products.GetCustomFields(1, 50);

            if (customFields.Data.Count > 0)
            {
                var firstCustomField = client.Products.GetCustomField(customFields.Data.First().Id.Value);
            }

            // Artificial brake to prevent throttling
            Thread.Sleep(1000);
            #endregion

            #region products

            var products = client.Products.GetProducts(1, 50);

            var patchableFields = client.Products.GetPatchableProductFields();

            if (products.Data.Count > 0)
            {
                // var patchedProduct = client.Products.PatchArticle(products.Data.First().Id.Value, new Dictionary<string, string> { { "EAN", "0815" } });
            }

            // Artificial brake to prevent throttling
            Thread.Sleep(1000);

            // Doing some stock manipulations
            Console.WriteLine(
                "Please enter one SKU of an article to update it's stock. Be aware, that these changes are permanent, so better use a demo article. Leave blank to skip.");
            var sku = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(sku))
            {
                var updateStockCodeResult =
                    client.Products.UpdateStockCode(
                        new UpdateStockCode { Sku = sku, StockCode = "Testlager" });

                // Artificial brake to prevent throttling
                Thread.Sleep(1000);

                var updateStockResult = client.Products.UpdateStock(
                    new UpdateStock
                    {
                        Sku = sku,
                        NewQuantity = 15,
                        Reason = "Change due to api tests."
                    });

                // Artificial brake to prevent throttling
                Thread.Sleep(1000);

                var updateStockMultipleResult = client.Products.UpdateStockMultiple(
                    new List<UpdateStock>
                    {
                        new UpdateStock {Sku = sku, NewQuantity = 15},
                        new UpdateStock {Sku = "4712", NewQuantity = 23}
                    });
            }


            #endregion

            #region product images

            if (products.Data.Count > 0)
            {
                var articleId = products.Data.First().Id.Value;

                var articleImages = client.Products.GetArticleImages(articleId);

                // Artificial brake to prevent throttling
                Thread.Sleep(1000);

                if (articleImages.Data.Count > 0)
                {
                    var imageId = articleImages.Data.First().Id;



                    var articleImage = client.Products.GetArticleImage(articleId, imageId);


                }

                // Artificial brake to prevent throttling
                Thread.Sleep(1000);

                var newArticleImage = client.Products.AddArticleImage(new ArticleImage { ArticleId = articleId, Id = 0, IsDefault = false, Url = "http://static.bbc.co.uk/history/img/ic/640/images/resources/histories/titanic.jpg" });

                // Artificial brake to prevent throttling
                Thread.Sleep(1000);

                client.Products.DeleteArticleImage(newArticleImage.Data.Id);

            }

            // Artificial brake to prevent throttling
            Thread.Sleep(1000);
            #endregion

            #region customer
            var customers = client.Customer.GetCustomerList(1, 50);

            //client.Customer.AddCustomer(new CustomerForCreation { Name = "Max Mustermann", Email = "max@mustermann.de", Address = new CustomerAddress { FirstName = "Max", LastName = "Mustermann", Street = "Mustergasse", Housenumber = "1", Zip = "12345", City = "Musterhausen", AddressType = 1, CountryCode = "DE" } });

            // Artificial brake to prevent throttling
            Thread.Sleep(1000);

            if (customers.Data.Count > 0)
            {
                var customer = client.Customer.GetCustomer(customers.Data.First().Id.Value);

                customer.Data.Name = "Tobias Tester";

                //customer = client.Customer.UpdateCustomer(customer.Data);

                // Artificial brake to prevent throttling
                Thread.Sleep(1000);

                var customerOrder = client.Customer.GetOrdersForCustomer(customer.Data.Id.Value, 1, 50);
                var customerAddresses = client.Customer.GetAddressesForCustomer(customer.Data.Id.Value, 1, 50);
            }

            // Artificial brake to prevent throttling
            Thread.Sleep(1000);
            #endregion

            #region search
            client.Search.SearchTerm(new Search { Term = "4711", Type = new List<string> { "order", "product", "customer" } });

            // Artificial brake to prevent throttling
            Thread.Sleep(1000);

            #endregion

            #region events
            // Getting events for this account
            var events = client.Events.GetEvents();

            // Artificial brake to prevent throttling
            Thread.Sleep(1000);

            #endregion

            #region shipping
            // Getting my shipping providers
            var shippingProvider = client.Shipment.GetShippingProvider();

            // Artificial brake to prevent throttling
            Thread.Sleep(1000);

            #endregion

            #region miscellaneous
            // Requesting the urls of the terms and conditions for the usage of billbee.
            var termsAndConditions = client.AutomaticProvision.TermsInfo();

            // Artificial brake to prevent throttling
            Thread.Sleep(1000);
            #endregion

            #region orders

            // Getting a list of all orders with order state 'confirmed'
            var orders = client.Orders.GetOrderList(page: 1, pageSize: 20,
                orderStateId: new List<OrderStateEnum> { OrderStateEnum.Bestaetigt });
            // var x = client.Orders.GetInvoiceList();

            // Example to create a new order. Please create a complete order object for usage.
            // var createOrderResult = client.Orders.PostNewOrder(new Billbee.Api.Client.Model.Order() { });

            Console.WriteLine(
                "Please enter one order number for further test manipulations. Be aware, that these changes are permanent. Please use an demo order. Leave blank to skip.");
            var orderId = Console.ReadLine();
            long orderIdInt;
            if (!string.IsNullOrWhiteSpace(orderId) && long.TryParse(orderId, out orderIdInt))
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

            #endregion

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            return 0;
        }
    }
}
