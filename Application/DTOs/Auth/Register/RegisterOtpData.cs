using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.Register
{
    public class RegisterOtpData
    {
        public RegisterRequest Request { get; set; } = default!;
        public string Otp { get; set; } = default!;
    }
}
