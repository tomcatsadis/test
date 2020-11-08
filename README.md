# AzureFunctionAccessTokenHandler
AzureFunctionAccessTokenHandler is libraries project to handle access token validation. 

This libraries is also published in NuGet Package: https://pkgs.dev.azure.com/TomcatSadis/_packaging/TomcatSadis/nuget/v3/index.json

## The Libraries

Library projects can be found at src folder.

### TomcatSadis.Security.AcessTokenGenerator
A library to generate access token and RSA key pair.

README details can be found at the project folder.

### TomcatSadis.Security.AcessTokenHandler 
A library to handle access token. 

Use this library in your Azure Function HTTP Trigger to validate access token that exist in HTTP authorization request header.

Also you can extract information (e.g. user id, claims) from the access token.

This library use dependency injection, so you just need to inject the library to your Startup, and use it in your API Service (HTTP Trigger).

Technologies / depedencies:
- .Net Standard 2.1
- Depedency injection
- JWT

README details can be found at the project folder.

### TomcatSadis.Security.WebJobs.AcessTokenHandler
A library to handle access token.

Use this library in your Azure Function HTTP Trigger to validate access token that exist in HTTP authorization request header.
Also you can extract information (e.g. user id, claims) from the access token.

This library use custom input binding, so you just need to add the binding to your WebJobs Startup, and use the binding in your API Service (HTTP Trigger).

Technologies / depedencies
- Azure Function v3
- .NET Core 3.1
- Custom input binding
- JWT

README details can be found at the project folder.

## Examples

Project examples can be found at examples folder.

### TomcatSadis.Security.AcessTokenGenerator.Examples
Project example that demonstrate the use of TomcatSadis.Security.AcessTokenGenerator library.

This project example use Azure function HTTP Trigger.

Set this project as StartUp project, run, then send http request (from curl or postman) to try it.

### TomcatSadis.Security.AcessTokenHandler.Examples
Project example that demonstrate the use of TomcatSadis.Security.AcessTokenHandler library.

This project example use Azure function HTTP Trigger.

Set this project as StartUp project, run, then send http request (from curl or postman) to try it.

### TomcatSadis.Security.WebJobs.AcessTokenHandler.Examples
Project example that demonstrate the use of TomcatSadis.Security.WebJobs.AcessTokenHandler library.

This project example use Azure function HTTP Trigger.

Set this project as StartUp project, run, then send http request (from curl or postman) to try it.

## Tests

Tests project can be found at tests folder.

### TomcatSadis.Security.AcessTokenGenerator.Tests
Unit tests to test the TomcatSadis.Security.AcessTokenGenerator code.

### TomcatSadis.Security.AcessTokenHandler.Tests
Unit tests to test the TomcatSadis.Security.AcessTokenHandler code.