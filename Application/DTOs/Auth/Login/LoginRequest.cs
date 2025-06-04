using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.Login
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "The email format is incorrect.")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        //[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
        //    ErrorMessage = "The password must be at least 8 characters, including letters, numbers, and special characters.")]
        public string Password { get; set; }
    }
}
