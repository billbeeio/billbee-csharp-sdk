using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

public static class TestData
{
    public static CustomerAddress CustomerAddress =>
        new CustomerAddress
        {
            FirstName = "John",
            LastName = "Doe",
            Street = "Mustergasse",
            Housenumber = "1",
            Zip = "12345",
            City = "Musterstadt",
            AddressType = 1,
            CountryCode = "DE",
            CustomerId = 0
        };

    public static Product Product => new Product
    {
        Title = new List<MultiLanguageString>
        {
            new MultiLanguageString()
            {
                Text = "the Title",
                LanguageCode = "de"
            }
        },
        Type = 0,
        Images = new List<ArticleImage>(),
        InvoiceText = new List<MultiLanguageString>
        {
            new MultiLanguageString()
            {
                Text = "invoice text",
                LanguageCode = "de"
            }
        },
    };

    public static Webhook WebHook => new Webhook
    {
        Id = null,
        WebHookUri = "https://webhook.site/5290627f-b5e3-4123-a715-26a721054617?noecho",
        Secret = "4e4451af-63c5-44f4-a3c5-1dcf8617fc5c",
        Description = "A simple description",
        IsPaused = true,
        Filters = new List<string> { "order.created" },
        Headers = new Dictionary<string, string>
            { { "TestHeader", "TestHeaderValue" }, { "Another Testheader", "Another Value" } },
        Properties = new Dictionary<string, object>()
    };

    public static CustomerForCreation Customer => new CustomerForCreation
    {
        Name = "John Doe",
        Address = TestData.CustomerAddress,
        Email = "john@doe.com",
        DefaultMailAddress = new CustomerMetaData
        {
            Value = "john@doe.com",
            TypeId = 1,
        },
        Type = 0
    };

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
        ChangeStateToSend = true
    };

    public static CustomerAddress GetCustomerAddress(long? customerId)
    {
        var address = CustomerAddress;
        address.CustomerId = customerId;
        return address;
    }
}