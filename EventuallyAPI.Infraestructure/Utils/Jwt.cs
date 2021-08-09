using System;

namespace EventuallyAPI.Infraestructure.Utils
{
    public class Jwt
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
