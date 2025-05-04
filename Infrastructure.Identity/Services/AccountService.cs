using Application.DTOs.Account;
using Application.Exceptions;
using Application.Wrappers;
using Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Services;
using Application.Interfaces;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IEmailService _emailService;
    
    public AccountService(UserManager<User> userManager, 
        RoleManager<IdentityRole> roleManager, 
        SignInManager<User> signInManager,
        IEmailService emailService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        this._emailService = emailService;
    }
    
    public async Task<BaseResponse<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new ApiException($"No Accounts Registered with {request.Email}.");
        }
        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            throw new ApiException($"Invalid Credentials for '{request.Email}'.");
        }
        AuthenticationResponse response = new AuthenticationResponse();
        response.Id = user.Id;
        response.UserName = user.UserName;
        response.Email = user.Email;
        
        return new BaseResponse<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
    }
}