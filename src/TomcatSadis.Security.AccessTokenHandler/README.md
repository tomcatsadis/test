# TomcatSadis.Security.AccessTokenHandler

A library to handle JWT access token.

Use this library in your Azure Function HTTP Trigger to validate access token that exist in HTTP authorization request header.

Also you can extract information (e.g. user id, claims) from the access token.

This library use dependency injection, so you just need to inject the library to your Startup, and use it in your API Service (HTTP Trigger).

## Technologies / Dependencies
- .Net Standard 2.1
- Dependency injection
- JWT

## How to use

1. Add the library to your project (from offline or NuGet Packages)

2. Inject the library in your Startup class as shown below.
``` csharp
...
using TomcatSadis.Security.AccessTokenHandler;
...
[assembly: FunctionsStartup(typeof(MyHttpService.Startup))]
namespace MyHttpService
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            ...
            builder.Services.AddAccessTokenHandler();
            ...
        }
    }
}
```

3. Provide access token settings in your project as shown below. Change the settings value with your settings value.
``` json
{
  "AccessTokenSettings:PublicKey": "MIIBCgKCAQEA7oGXGFiW63LXxxi2So4Y19wbp8R7ij1iFn7EvFJnDHcvKvi+VLSdmvJjDf/eJDDRestuSN5MS6+35kvl74nSqXPDmTFjCWWQQlqPBX0S7K2XOtg7l/goBGwPMF47AM7gEbqR2WXDoVwy/ALwPeRZhCQrKM9aIx+FjRpZSnU4sYF7zUDpKeb6VjcpyPc9qxPGsIRqKBECHQIpzgq90/UAsqQRK3+QktXPhxOiDna372D5P5MIXx1lziXiZ2bdxJezPkgfUgLC0xO3BUuaxt+KPg9vRcYjVd/Rpo1K+zi9zMKvTCMMFZWBFNqvZXvmZ0oQvJiGhXcFdXCIWwjHPjlJkQIDAQAB",
  "AccessTokenSettings:Issuer": "TomcatSadis.AuthService",
  "AccessTokenSettings:Audience": "TomcatSadis.AccessTokenHandler"
}
```

4. To use the library to validate access token and extract information (e.g. user id and claims) in your API Service, add code as shown below.
``` csharp
...
using TomcatSadis.Security.AccessTokenHandler;
...
namespace MyHttpService
{
    public class MyController
    {
        private readonly IAccessTokenProvider _accessTokenProvider;

        public MyController(IAccessTokenProvider accessTokenProvider)
        {
            _accessTokenProvider = accessTokenProvider;
        }

        [FunctionName("MyController")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "my-controller")] HttpRequest req,
            ILogger log)
        {
            ...
            var accessTokenResult = _accessTokenProvider.ValidateToken(req);

            if (accessTokenResult.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            var userId = accessTokenResult.Principal.GetUserId();

            ...
        }
    }
}
```

Source code and complete documentation can be found at
[This Repository](https://dev.azure.com/TomcatSadisLab/TomcatSadisPackage/_git/AccessTokenHandler)