using System.Collections.Generic;

namespace Devshift.Jwt.Models
{
    public class JwtTokenBase
    {
        public string Nbf { get; set; }
        public string Exp { get; set; }
        public string Iss { get; set; }
        public string Type { get; set; }
    }

    public class JwtOneTimeToken : JwtTokenBase
    {
        public string UserName { get; set; }
        public string SystemName { get; set; }
    }

    public class JwtAccessToken : JwtTokenBase
    {
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Scopes { get; set; }
        public string SystemName { get; set; }
    }

    public class JwtResetPasswordToken : JwtOneTimeToken
    {
        public string SecretToken { get; set; }
    }

    public class JwtRefreshToken : JwtTokenBase
    {
        public string Aud { get; set; } //system uuid
        public string Sub { get; set; } //user uuid
    }
}