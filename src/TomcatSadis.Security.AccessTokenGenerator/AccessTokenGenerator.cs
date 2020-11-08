using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace TomcatSadis.Security.AccessTokenGenerator
{
    public static class AccessTokenGenerator
    {
        /// <summary>
        /// Program to generate access token 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="privateKey"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <param name="notBefore"></param>
        /// <param name="expires"></param>
        /// <param name="additionClaims"></param>
        /// <returns>
        /// Generated access token
        /// </returns>
        public static string GenerateAccessToken(
            string userId,
            string privateKey,
            string issuer,
            string audience,
            DateTime notBefore,
            DateTime expires,
            List<Claim> additionClaims)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException($"{nameof(userId)} cannot be null or empty.");
            }

            if (string.IsNullOrWhiteSpace(privateKey))
            {
                throw new ArgumentException($"{nameof(privateKey)} cannot be null or empty.");
            }

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId)
            };

            if (additionClaims != null)
            {
                claims.AddRange(additionClaims);
            }

            using RSA rsa = RSA.Create();

            rsa.ImportRSAPrivateKey( 
                source: Convert.FromBase64String(privateKey),
                bytesRead: out int _);

            var signingCredentials = new SigningCredentials(
                key: new RsaSecurityKey(rsa),
                algorithm: SecurityAlgorithms.RsaSha256
            );

            var jwt = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: notBefore,
                expires: expires,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
