using eCommerce.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.DTO
{
    public class RegisterRequestDto
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string ? Email { get; set; }
        public GenderOptions? Gender { get; set; }
        public RolesOptions? Roles { get; set; }
    }
}
