using eCommerce.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eCommerce.Core.DTO
{
    public class AuthenticationResponse
    {
        public Guid Id { get; set; }

        public string AccessToken { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }

    }
}
