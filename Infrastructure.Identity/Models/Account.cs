using Microsoft.AspNetCore.Identity;

namespace Identity.Models;

public class Account : IdentityUser
{
    public string Email { get; set; }
    public string Password { get; set; }
}