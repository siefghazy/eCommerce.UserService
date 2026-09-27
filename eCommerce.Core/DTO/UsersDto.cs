using eCommerce.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eCommerce.Core.DTO
{
    public class UsersDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public GenderOptions? Gender { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;
        [JsonIgnore]
        public RolesOptions? role { get; set; }
    }
}
