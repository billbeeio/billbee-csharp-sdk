The folder EndPointTests contains UnitTests for each endpoint, where the api is mocked. 
Thus, these UnitTests do not access the Billbee Api.

The folder EndPointIntegrationTests contains UnitTests for each endpoint, which DO require access to the Billbee Api.
It's required to provide a file named 'config.prod.json' on the project-level of this UnitTest project.
To protect your Billbee data, these UnitTests will not be executed by default.
