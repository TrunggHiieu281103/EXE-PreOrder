using Application.DTOs.Account;
using Application.Wrappers;

namespace Application.Interfaces;

public interface IAccountService
{   
    Task<BaseResponse<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request);
}