namespace TomcatSadis.Security.WebJobs.AccessTokenHandler
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Azure.WebJobs.Host.Bindings;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using TomcatSadis.Security.WebJobs.AccessTokenHandler.Settings;

    /// <summary>
    /// Creates a <see cref="ClaimsPrincipal"/> instance for the supplied header and configuration values.
    /// </summary>
    /// <remarks>
    /// This is where the actual authentication happens - replace this code to implement a different authentication solution.
    /// </remarks>
    public class AccessTokenValueProvider : IValueProvider
    {
        private const string AUTH_HEADER_NAME = "Authorization";
        private const string BEARER_PREFIX = "Bearer ";
        private readonly HttpRequest _request;
        private readonly IAccessTokenSettings _accessTokenSettings;

        public AccessTokenValueProvider(HttpRequest request, IAccessTokenSettings accessTokenSettings)
        {
            
            _request = request;
            
            _accessTokenSettings = accessTokenSettings;
        }

        public Task<object> GetValueAsync()
        {
            try
            {
                // Get the token from the header
                if (_request.Headers.ContainsKey(AUTH_HEADER_NAME) &&
                   _request.Headers[AUTH_HEADER_NAME].ToString().StartsWith(BEARER_PREFIX))
                {
                    var token = _request.Headers["Authorization"].ToString().Substring(BEARER_PREFIX.Length);

                    // Create the parameters
                    var tokenParams = new TokenValidationParameters()
                    {
                        RequireSignedTokens = true,
                        ValidAudience = _accessTokenSettings.Audience,
                        ValidateAudience = true,
                        ValidIssuer = _accessTokenSettings.Issuer,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = _accessTokenSettings.RsaKey,
                        RequireExpirationTime = true, // <- JWTs are required to have "exp" property set
                    };

                    // Validate the token
                    var handler = new JwtSecurityTokenHandler();
                    var result = handler.ValidateToken(token, tokenParams, out var securityToken);
                    return Task.FromResult<object>(AccessTokenResult.Success(result));
                }
                else
                {
                    return Task.FromResult<object>(AccessTokenResult.NoToken());
                }
            }
            catch (SecurityTokenExpiredException)
            {
                return Task.FromResult<object>(AccessTokenResult.Expired());
            }
            catch (Exception ex)
            {
                return Task.FromResult<object>(AccessTokenResult.Error(ex));
            }
        }

        public Type Type => typeof(ClaimsPrincipal);

        public string ToInvokeString() => string.Empty;
    }
}
