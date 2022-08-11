using Billbee.Api.Client.Model;
using Billbee.Api.Client.Model.Rechnungsdruck.WebApp.Model.Api;
using Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

namespace Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers
{
    public static partial class TestData
    {
        public static ShipmentWithLabel GetShipmentWithLabel(long orderId, string printerName, long productId, long providerId) => new ShipmentWithLabel
        {
            OrderId = orderId,
            Dimension = new ShipmentDimensions
            {
                height = 10,
                length = 10,
                width = 10,
            },
            ClientReference = "clientRef",
            PrinterName = printerName,
            ProductId = productId,
            ProviderId = providerId,
            ShipDate = DateTime.Now,
            WeightInGram = 500,
            ChangeStateToSend = true,
        };

        public static PostShipment GetPostShipment(string printerName, string providerName, byte shippingCarrier, string productCode) =>
            new PostShipment
            {
                Dimension = new ShipmentDimensions
                {
                    height = 10,
                    length = 10,
                    width = 10,
                },
                ClientReference = "clientRef",
                PrinterName = printerName,
                ProviderName = providerName,
                ShipDate = DateTime.Now,
                WeightInGram = 500,
                shippingCarrier = shippingCarrier,
                Services = new List<object>(),
                ReceiverAddress = new ShipmentAddress
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Street = "Teststraße",
                    Housenumber = "1",
                    Zip = "12345",
                    City = "Teststadt",
                    CountryCode = "DE",
                    Email = "john@doe.com",
                    Telephone = "0123456789",
                },
                OrderSum = 10.0M,
                OrderCurrencyCode = "EUR",
                TotalNet = 8.40M,
                ProductCode = productCode,
            };
    }
}

namespace Billbee.Api.Client.Test.EndPointIntegrationTests
{
    [TestClass]
    public class ShipmentEndPointIntegrationTest
    {
#pragma warning disable CS8618
        public TestContext TestContext { get; set; }
#pragma warning restore CS8618

        [TestInitialize]
        public void TestInitialize()
        {
            IntegrationTestHelpers.CheckAccess(TestContext.ManagedType, TestContext.ManagedMethod);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Shipment_GetShipments_IntegrationTest()
        {
            CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Shipment.GetShipments());
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Shipment_GetShippingProvider_IntegrationTest()
        {
            CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Shipment.GetShippingProvider());
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Shipment_ShipOrderWithLabel_IntegrationTest()
        {
            var provider = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Shipment.GetShippingProvider())
                .First();
            var printer = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.CloudStorages.GetCloudStorageList())
                .Data
                .First(x => x.UsedAsPrinter);

            var orderId = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Orders.GetOrderList()).Data.First()
                .BillBeeOrderId;
            Assert.IsNotNull(orderId);
            var providerId = provider.id;
            var productId = provider.products.First().id;

            var shipmentWithLabel = TestData.GetShipmentWithLabel(orderId.Value, printer.Name, productId, providerId);
            var result = CrudHelpers.CreateApiResult(
                s => IntegrationTestHelpers.ApiClient.Shipment.ShipOrderWithLabel(s), shipmentWithLabel, false);
            Assert.AreEqual(shipmentWithLabel.OrderId, result.Data.OrderId);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Shipment_PostShipment_IntegrationTest()
        {
            var provider = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Shipment.GetShippingProvider())
                .First();
            var printer = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.CloudStorages.GetCloudStorageList())
                .Data
                .First(x => x.UsedAsPrinter);
            var carrier = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Shipment.GetShippingCarriers())
                .First();
            var carriers = IntegrationTestHelpers.ApiClient.Shipment.GetShippingCarriers().Select(x => $"{x.Id}:{x.Name}");
            Console.WriteLine(carriers);

            var postShipment = TestData.GetPostShipment(printer.Name, provider.name, carrier.Id, provider.products.First().productName);
            var shipment = CrudHelpers.CreateApiResult(x => IntegrationTestHelpers.ApiClient.Shipment.PostShipment(x),
                postShipment, false);
            Console.WriteLine($"Shipment created, ShippingId={shipment.Data.ShippingId}");
            
            Assert.IsFalse(string.IsNullOrWhiteSpace(shipment.Data.ShippingId));
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Shipment_GetShippingCarriers_IntegrationTest()
        {
            CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Shipment.GetShippingCarriers());
        }
    }
}