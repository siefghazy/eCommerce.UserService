using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.DTO
{
    public class RegisterResponse
    {
        public string Message { get; set; }
        public HttpStatusCode Code { get; set; }
    }
}
