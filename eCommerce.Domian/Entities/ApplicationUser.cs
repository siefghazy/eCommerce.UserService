using eCommerce.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domian.Entities
{
    public class ApplicationUser
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public GenderOptions? Gender { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? RefreshToken { get; set; }
        public RolesOptions? Roles { get; set; }
        public bool EmailConfirmed { get; set; } = false;
        public string EmailConfirmationToken { get; set; }
    }
}
