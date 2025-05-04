using System.Text.Json.Serialization;
using Application.Enums;

namespace Application.DTOs.Account;

public class AuthenticationResponse
{
    public string Id { get; set; }
    
    public string UserName { get; set; }
    
    
    public string Email { get; set; }

}