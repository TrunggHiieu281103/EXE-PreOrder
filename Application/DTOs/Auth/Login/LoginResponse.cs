using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.Login
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public UserDto User { get; set; }
    }
}
