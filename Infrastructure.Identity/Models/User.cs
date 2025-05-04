using Microsoft.AspNetCore.Identity;

namespace Identity.Models;

public class User : IdentityUser
{
    public string Email { get; set; }
    public string Password { get; set; }
}