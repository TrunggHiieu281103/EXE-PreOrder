using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.Register
{
    public class RegisterRequest
    {
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone must be exactly 10 digits and contain only numbers.")]
        public string Phone { get; set; }
    }
}
