using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Devshift.Jwt.Constants;
using Devshift.Jwt.Models;
using Microsoft.IdentityModel.Tokens;

namespace Devshift.Jwt
{
    public static class JwtUtil
    {
        public static string GenerateJwt(string secretKey, Claim[] claims, int lifeTime, string iss = "devshift")
        {
            var startDate = DateTime.UtcNow;

            var endDate = DateTime.UtcNow.AddSeconds(lifeTime);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(iss, null, claims, startDate, endDate, credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static JwtBase ExtractJwt(string token, string systemName)
        {
            return null;
        }

        public static T ExtractJwtToken<T>(string token) where T : JwtTokenBase
        {
            var handler = new JwtSecurityTokenHandler();
            var readToken = handler.CanReadToken(token);

            if (!readToken) return null;

            var jwt = handler.ReadJwtToken(token);
            var tokenType = _GetClaimValue(jwt, "type");

            switch (tokenType)
            {
                case TokenType.OneTimeToken: return (T)(object)_ExtractOneTimeToken(jwt);
                case TokenType.AccessToken: return (T)(object)_ExtractAccessToken(jwt);
                case TokenType.RefreshToken: return (T)(object)_ExtractRefreshToken(jwt);
                case TokenType.ResetPasswordToken: return (T)(object)_ExtractResetPasswordToken(jwt);
                default:
                    return null;
            }
        }

        public static JwtToken ExtractJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var readToken = handler.CanReadToken(token);

            if (!readToken) return null;

            var jwt = handler.ReadJwtToken(token);

            var systemName = _GetClaimValue(jwt, "systemName");
            var iss = _GetClaimValue(jwt, "iss");
            var exp = _GetClaimValue(jwt, "exp");
            var nbf = _GetClaimValue(jwt, "nbf");
            var role = _GetClaimValue(jwt, "role");
            var idNumber = _GetClaimValue(jwt, "idNumber");


            return new JwtToken
            {
                SystemName = systemName,
                Iss = iss,
                Nbf = nbf,
                Exp = exp,
                Role = role,
                IdNumber = idNumber
            };
        }

        #region Private Function

        private static JwtOneTimeToken _ExtractOneTimeToken(JwtSecurityToken jwt)
        {
            return new JwtOneTimeToken
            {
                UserName = _GetClaimValue(jwt, "userName"),
                Iss = _GetClaimValue(jwt, "iss"),
                Nbf = _GetClaimValue(jwt, "nbf"),
                Exp = _GetClaimValue(jwt, "exp"),
                SystemName = _GetClaimValue(jwt, "systemName"),
                Type = _GetClaimValue(jwt, "type")
            };
        }

        private static JwtAccessToken _ExtractAccessToken(JwtSecurityToken jwt)
        {
            return new JwtAccessToken
            {
                UserName = _GetClaimValue(jwt, "userName"),
                Iss = _GetClaimValue(jwt, "iss"),
                Nbf = _GetClaimValue(jwt, "nbf"),
                Exp = _GetClaimValue(jwt, "exp"),
                SystemName = _GetClaimValue(jwt, "systemName"),
                Type = _GetClaimValue(jwt, "type")
            };
        }

        private static JwtResetPasswordToken _ExtractResetPasswordToken(JwtSecurityToken jwt)
        {
            return new JwtResetPasswordToken
            {
                UserName = _GetClaimValue(jwt, "userName"),
                Iss = _GetClaimValue(jwt, "iss"),
                Nbf = _GetClaimValue(jwt, "nbf"),
                Exp = _GetClaimValue(jwt, "exp"),
                SystemName = _GetClaimValue(jwt, "systemName"),
                SecretToken = _GetClaimValue(jwt, "secretToken"),
                Type = _GetClaimValue(jwt, "type")
            };
        }

        private static JwtRefreshToken _ExtractRefreshToken(JwtSecurityToken jwt)
        {
            return new JwtRefreshToken
            {
                Aud = _GetClaimValue(jwt, "aud"),
                Sub = _GetClaimValue(jwt, "sub"),
                Type = _GetClaimValue(jwt, "type"),
                Iss = _GetClaimValue(jwt, "iss"),
                Nbf = _GetClaimValue(jwt, "nbf"),
                Exp = _GetClaimValue(jwt, "exp")
            };
        }
        private static string _GetClaimValue(JwtSecurityToken jwt, string claimType)
        {
            var claim = jwt.Claims.FirstOrDefault(claim => claim.Type == claimType);

            return (claim == null) ? null : claim.Value;
        }
        #endregion
    }
}