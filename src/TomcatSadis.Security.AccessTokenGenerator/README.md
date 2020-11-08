#TomcatSadis.Security.AccessTokenGenerator

A library to generate access token. 

## Technologies / Depedencies
- .Net Standard 2.1
- JWT

## TomcatSadis.Security.AccessTokenCreator.AccessTokenGenerator 
A library to generate access token. 

Example scenario is for authentication service to handle user authentication/login, and return the access token to user.

### How to use
1. Add the library to your project (from offline or NuGet Packages)

2. Use the TomcatSadis.Security.AccessTokenCreator.AccessTokenGenerator.GenerateAccessToken from your projects.

## TomcatSadis.Security.AccessTokenCreator.KeyPairGenerator 
A library to generate RSA key pair (private and public key).
Private key is used to generate access token. 
Public key is used to validate the access token. 

### How to use
1. Add the library to your project (from offline or NuGet Packages)

2. Use the TomcatSadis.Security.AccessTokenCreator.KeyPairGenerator.GenerateRSAKeyPair from your projects.

## Source code and documentation
Source code and complete documentation can be found at
[This Repository](https://dev.azure.com/TomcatSadisLab/TomcatSadisPackage/_git/AccessTokenHandler)