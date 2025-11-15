using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs.IdentityDto_s
{
    public class RegisterDto
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string Username { get; set; } =null!;

        public string DisplayName { get; set; } = null!;
        [Phone]
        public string phoneNumber { get; set; } = null!;


    }
}
