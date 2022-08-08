using Billbee.Api.Client.Enums;
using Billbee.Api.Client.Model;
using Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

namespace Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers
{
    public static partial class TestData
    {
        public static Order Order => new Order
        {
            UpdatedAt = DateTime.Now,
            CreatedAt = DateTime.Now,
            VatMode = VatModeEnum.DisplayVat,
            TaxRate1 = 19.0M,
            TaxRate2 = 7.0M,
            OrderNumber = "12345",
            OrderItems = new List<OrderItem>
            {
                new OrderItem {
                    TotalPrice = 10M,
                    Quantity = 1,
                    Product = new OrderItemProduct
                    {
                        Title = "Test Product",
                        Weight = 500,
                        SKU = "4711"
                    }
                }
            }
        };
    }
}

namespace Billbee.Api.Client.Test.EndPointIntegrationTests
{
    [TestClass]
    public class OrderEndPointIntegrationTest
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
        public void GetLayouts_IntegrationTest()
        {
            var result = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Orders.GetLayouts());
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void GetOrder_IntegrationTest()
        {
            PostNewOrder_IntegrationTest();
        }

        [TestMethod]
        [RequiresApiAccess]
        public void GetPatchableFields_IntegrationTest()
        {
            var patchableFields =
                CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Orders.GetPatchableFields());
            Console.WriteLine("Patchable Order Fields:");
            foreach (var field in patchableFields.Data)
            {
                Console.WriteLine(field);
            }
        }

        [TestMethod]
        [RequiresApiAccess]
        public void PatchOrder_IntegrationTest()
        {
            var order = _createOrder();
            Assert.AreEqual(TestData.Order.SellerComment, order.SellerComment);
            
            var fieldsToPatch = new Dictionary<string, object>
            {
                { "SellerComment", "Modified" }
            };
            var patchResult = CrudHelpers.Patch<Order>(
                (id, fields) => IntegrationTestHelpers.ApiClient.Orders.PatchOrder(id, fields),
                order.BillBeeOrderId!.Value, fieldsToPatch);
            
            Assert.AreEqual("Modified", patchResult.Data.SellerComment);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void GetOrderByExternalReference_IntegrationTest()
        {
            var extRef = Guid.NewGuid().ToString();
            var order = _createOrder(extRef);
            
            var orderResult = CrudHelpers.GetOneApiResult<Order>(e => IntegrationTestHelpers.ApiClient.Orders.GetOrderByExternalReference(e),
                extRef, false);
            Assert.IsNotNull(orderResult);
            Assert.AreEqual(order.BillBeeOrderId, orderResult.Data.BillBeeOrderId);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void GetOrderByExternalIdAndPartner_IntegrationTest()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        [RequiresApiAccess]
        public void GetOrderList_IntegrationTest()
        {
            var result = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Orders.GetOrderList());
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void GetInvoiceList_IntegrationTest()
        {
            var result = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Orders.GetInvoiceList());
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void PostNewOrder_IntegrationTest()
        {
            var order = _createOrder();
            
            var orderResult = CrudHelpers.GetOneApiResult<Order>(id => IntegrationTestHelpers.ApiClient.Orders.GetOrder(id),
                order.BillBeeOrderId!.Value.ToString(), false);
            Assert.IsNotNull(orderResult);
            Assert.AreEqual(order.BillBeeOrderId, orderResult.Data.BillBeeOrderId);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void AddTags_IntegrationTest()
        {
            _createOrderWithTags();
        }

        [TestMethod]
        [RequiresApiAccess]
        public void UpdateTags_IntegrationTest()
        {
            var order = _createOrderWithTags();

            var updatedTags = new List<string> { "tag3", "tag4" };
            var result = IntegrationTestHelpers.ApiClient.Orders.UpdateTags(updatedTags, order.BillBeeOrderId!.Value);
            Assert.IsNotNull(result);
            Assert.AreEqual((int)ApiResult<object>.ErrorCodeEnum.NoError, (int)result.ErrorCode);
            
            var orderResult = CrudHelpers.GetOneApiResult<Order>(id => IntegrationTestHelpers.ApiClient.Orders.GetOrder(id),
                order.BillBeeOrderId.Value.ToString(), false);
            Assert.IsNotNull(orderResult);
            Assert.IsFalse(orderResult.Data.Tags.Contains("tag1"));
            Assert.IsFalse(orderResult.Data.Tags.Contains("tag2"));
            Assert.IsTrue(orderResult.Data.Tags.Contains("tag3"));
            Assert.IsTrue(orderResult.Data.Tags.Contains("tag4"));
            
        }

        [TestMethod]
        [RequiresApiAccess]
        public void AddShipment_IntegrationTest()
        {
            var provider = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Shipment.GetShippingProvider())
                .First();
            var productId = provider.products.First().id;
            var carrier = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Shipment.GetShippingCarriers())
                .First();
            
            var order = _createOrder();
            var orderShipment = new OrderShipment
            {
                Comment = "comment",
                CarrierId = carrier.Id,
                OrderId = order.BillBeeOrderId!.Value,
                ShippingId = "123",
                ShippingProviderId = provider.id,
                ShippingProviderProductId = productId
            };
            IntegrationTestHelpers.ApiClient.Orders.AddShipment(orderShipment);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void CreateDeliveryNote_IntegrationTest()
        {
            var order = _createOrder();
            var result = IntegrationTestHelpers.ApiClient.Orders.CreateDeliveryNote(order.BillBeeOrderId!.Value);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(order.OrderNumber, result.Data.OrderNumber);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void CreateInvoice_IntegrationTest()
        {
            var order = _createOrder();
            
            // throws an exception, because no invoice has been created yet for this new order
            Assert.ThrowsException<Exception>(() => IntegrationTestHelpers.ApiClient.Orders.CreateInvoice(order.BillBeeOrderId!.Value));
        }

        [TestMethod]
        [RequiresApiAccess]
        public void ChangeOrderState_IntegrationTest()
        {
            var order = _createOrder();
            
            var newState = OrderStateEnum.Bestaetigt;
            Console.WriteLine($"Old state: {order.State.ToString()}");
            Assert.AreNotEqual(newState, order.State);
            
            IntegrationTestHelpers.ApiClient.Orders.ChangeOrderState(order.BillBeeOrderId!.Value, newState);
            
            var orderResult = CrudHelpers.GetOneApiResult<Order>(id => IntegrationTestHelpers.ApiClient.Orders.GetOrder(id),
                order.BillBeeOrderId.Value.ToString(), false);
            Assert.IsNotNull(orderResult);
            Assert.IsNotNull(orderResult.Data);
            Console.WriteLine($"New state: {order.State.ToString()}");
            Assert.AreEqual(newState, orderResult.Data.State);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void SendMailForOrder_IntegrationTest()
        {
            var order = _createOrder();
            var sendMessage = new SendMessage
            {
                Subject = new List<MultiLanguageString>
                {
                    new MultiLanguageString
                    {
                        Text = "Foo",
                        LanguageCode = "DE"
                    }
                },
                Body = new List<MultiLanguageString>
                {
                    new MultiLanguageString
                    {
                        Text = "Bar",
                        LanguageCode = "DE"
                    }
                }
            };
            
            IntegrationTestHelpers.ApiClient.Orders.SendMailForOrder(order.BillBeeOrderId!.Value, sendMessage);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void CreateEventAtOrder_IntegrationTest()
        {
            var order = _createOrder();
            IntegrationTestHelpers.ApiClient.Orders.CreateEventAtOrder(order.BillBeeOrderId!.Value, "myEvent");
        }

        [TestMethod]
        [RequiresApiAccess]
        public void ParsePlaceholders_IntegrationTest()
        {
            var order = _createOrder();
            
            var parsePlaceholdersQuery = new ParsePlaceholdersQuery
            {
                TextToParse = "This is my text for Order {OrderNumber}"
            };
            var result = IntegrationTestHelpers.ApiClient.Orders.ParsePlaceholders(order.BillBeeOrderId!.Value, parsePlaceholdersQuery);
            Assert.IsNotNull(result);
            Assert.AreEqual($"This is my text for Order {order.OrderNumber}", result.Result);
        }

        private Order _createOrder(string? extRef = null)
        {
            var testOrder = TestData.Order;
            if (!string.IsNullOrWhiteSpace(extRef))
            {
                testOrder.OrderNumber = extRef;
            }
            var result = CrudHelpers.CreateApiResult(w => IntegrationTestHelpers.ApiClient.Orders.PostNewOrder(w),
                testOrder);
            var order = result.Data;
            Assert.IsNotNull(order);
            Assert.AreEqual(testOrder.OrderNumber, order.OrderNumber);
            Assert.IsNotNull(order.BillBeeOrderId);
            Console.WriteLine($"Order created, BillbeeOrderId={order.BillBeeOrderId}");
            return order;
        }

        private Order _createOrderWithTags()
        {
            var order = _createOrder();

            var tags = new List<string> { "tag1", "tag2" };
            var result = IntegrationTestHelpers.ApiClient.Orders.AddTags(tags, order.BillBeeOrderId!.Value);
            Assert.IsNotNull(result);
            Assert.AreEqual((int)ApiResult<object>.ErrorCodeEnum.NoError, (int)result.ErrorCode);
            
            var orderResult = CrudHelpers.GetOneApiResult<Order>(id => IntegrationTestHelpers.ApiClient.Orders.GetOrder(id),
                order.BillBeeOrderId.Value.ToString(), false);
            Assert.IsNotNull(orderResult);
            Assert.IsTrue(orderResult.Data.Tags.Contains("tag1"));
            Assert.IsTrue(orderResult.Data.Tags.Contains("tag2"));

            return orderResult.Data;
        }
    }
}