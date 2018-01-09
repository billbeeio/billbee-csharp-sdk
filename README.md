[![Logo](https://app01.billbee.de/static/billbee/img/logo.png)](https://www.billbee.de)

# Billbee API
With this package you can implement the official Billbee API in your C# application.

## Prerequisites
- For accessing the Billbee API you need an API Key.
To get an API key, send a mail to [support@billbee.de](mailto:support@billbee.de) and send us a short note about what you are building.
- The API module must be activated in the account ([https://app01.billbee.de/de/settings/api](https://app01.billbee.de/de/settings/api))

## Install
Download this package and decompress the solution to a place of your choice.
If you don't want to compile by yourself, feel free, to use our NuGet package.

## Official API Documentation
[https://app01.billbee.de/swagger/ui/index](https://app01.billbee.de/swagger/ui/index)

## Usage

Simply open the solution in your Visual Studio or other C# IDE.

Then open the Billbee.Api.Client.Demo project and take a look at the examples in Program.cs

## Demo

### Initialization

```csharp
// Creating an individual logger, that implements ILogger
ILogger logger = new Logger();

// Creating new instance of ApiClient           
ApiClient client = new ApiClient(logger: logger);

// Enter your api key here. If you don't have an api key. Please contact support@billbee.de with a description on what you would like to do, to get one.
client.Configuration.ApiKey = "";
// Enter the username of your main account here.
client.Configuration.Username = "";
// Enter the password of your api here.
client.Configuration.Password = "";

// Test the configuration
if (client.TestConfiguration())
{
	logger.LogMsg("Api test successful", LogSeverity.Info);
}
else
{
	logger.LogMsg("Api test failed. Please control your configuration", LogSeverity.Error);
}
```

### Demo Calls
```csharp
// Getting events for this account
var events = client.Events.GetEvents();

// Getting my shipping providers
var shippingProvider = client.Shipment.GetShippingProvider();

// Doing some stock manipulations
Console.WriteLine("Please enter one SKU of an article to update it's stock. Be aware, that these changes are permanent, so better use a demo article. Leave blank to skip.");
var sku = Console.ReadLine();
if (!string.IsNullOrWhiteSpace(sku))
{
    var updateStockCodeResult = client.Products.UpdateStockCode(new Billbee.Api.Client.Model.UpdateStockCode { Sku = sku, StockCode = "Testlager" });
    var updateStockResult = client.Products.UpdateStock(new Billbee.Api.Client.Model.UpdateStock { Sku = sku, NewQuantity = 15, Reason = "Change due to api tests." });
    var updateStockMultipleResult = client.Products.UpdateStockMultiple(new List<Billbee.Api.Client.Model.UpdateStock> { new Billbee.Api.Client.Model.UpdateStock { Sku = sku, NewQuantity = 15 }, new Billbee.Api.Client.Model.UpdateStock { Sku = "4712", NewQuantity = 23 } });
}

// Requesting the urls of the terms and conditions for the usage of billbee.
var termsAndConditions = client.AutomaticProvision.TermsInfo();

// Getting a list of all orders with order state 'confirmed'
var orders = client.Orders.GetOrderList(page: 1, pageSize: 20, orderStateId: new List<int> { 2 });
// var x = client.Orders.GetInvoiceList();

// Example to create a new order. Please create a complete order object for usage.
// var createOrderResult = client.Orders.PostNewOrder(new Billbee.Api.Client.Model.Order() { });

Console.WriteLine("Please enter one order number for further test manipulations. Be aware, that these changes are permanent. Please use an demo order. Leave blank to skip.");
var orderId = Console.ReadLine();
int orderIdInt;
if (!string.IsNullOrWhiteSpace(orderId) && int.TryParse(orderId, out orderIdInt))
{
    // Remove all old tags and add the given ones.
    var updateTagsResult = client.Orders.UpdateTags(new List<string>() { "Test C", "Test D" }, orderIdInt);

    // Add new tags.
    var addTagsResult = client.Orders.AddTags(new List<string>() { "Test A", "Test B" }, orderIdInt);

    // Add a shipment to an order. Please fill in the following and uncomment. the last line.
    string shippingId = "0815";
    int providerId = 0;
    int productId = 0;
    // client.Orders.AddShipment(new OrderShipment { Comment = "Test", OrderId = orderIdInt, ShippingId = shippingId, ShippingProviderId = providerId, ShippingProviderProductId = productId });

    // Getting documents
    var deliveryNoteResult = client.Orders.CreateDeliveryNote(orderIdInt, true);
    var invoiceResult = client.Orders.CreateInvoice(orderIdInt, true);
}
```

## Contributing
Feel free to fork the repository and create pull-requests
