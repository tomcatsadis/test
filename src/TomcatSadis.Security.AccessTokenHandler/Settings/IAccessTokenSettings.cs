using Microsoft.IdentityModel.Tokens;

namespace TomcatSadis.Security.AccessTokenHandler.Settings
{
    public interface IAccessTokenSettings
    {
        string PublicKey { get; }

        string Issuer { get; }

        string Audience { get; }

        SecurityKey RsaKey { get; }

        /// <summary>
        /// Validate model attributes
        /// </summary>
        /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException">
        /// Thrown when attribute is invalid
        /// </exception>
        void ValidateAttributes();
    }
}
