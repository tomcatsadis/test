using Microsoft.IdentityModel.Tokens;

namespace TomcatSadis.Security.WebJobs.AccessTokenHandler.Settings
{
    public interface IAccessTokenSettings
    {
        string PublicKey { get; }

        string Issuer { get; }

        string Audience { get; }

        SecurityKey RsaKey { get; }
    }
}
