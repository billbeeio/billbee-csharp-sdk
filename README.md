[![Logo](https://app01.billbee.de/static/billbee/img/logo.png)](https://www.billbee.de)

# Billbee API
With this package you can implement the official Billbee API in your C# application.

## Prerequisites
- For accessing the Billbee API you need an API Key.
To get an API key, send a mail to [support@billbee.de](mailto:support@billbee.de) and send us a short note about what you are building.
- The API module must be activated in the account ([https://app01.billbee.de/de/settings/api](https://app01.billbee.de/de/settings/api))

## Install
Download this package and decompress the solution to a place of your choice.
For faster approach use our NuGet package.

## Official API Documentation
[https://app01.billbee.de/swagger/ui/index](https://app01.billbee.de/swagger/ui/index)

## Usage

Simply open the solution in your Visual Studio or other C# IDE.

Then open the Billbee.Api.Client.Demo project and take a look at the examples in Program.cs

##Demo

###Initialization

```bash
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

###Demo Calls


## Contributing
Feel free to fork the repository and create pull-requests
