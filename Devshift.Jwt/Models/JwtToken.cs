
namespace Devshift.Jwt.Models
{
    public class JwtBase
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string SystemId { get; set; }
        public string Nbf { get; set; }
        public string Exp { get; set; }
        public string Iss { get; set; }
        public string SystemName { get; set; }
    }

    public class JwtToken : JwtBase
    {
        public string Role { get; set; }
        public string IdNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Permissions { get; set; }
        public string Status { get; set; }
        public string BranchCode { get; set; }
    }

    public class JwtTokenMss : JwtBase
    {
        public string RoleId { get; set; }
    }
    public class JwtTokenGid : JwtBase
    {
        public string RoleId { get; set; }
    }
}