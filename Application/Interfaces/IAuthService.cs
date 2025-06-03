using Application.DTOs.Auth.Login;
using Application.DTOs.Auth.Register;
using Application.DTOs.Google;
using Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<BaseResponse<LoginResponse>> LoginAsync(LoginRequest request);
        Task<BaseResponse<string>> RegisterAsync(RegisterRequest request);
        Task<BaseResponse<LoginResponse>> VerifyOTPAsync(string email, string inputOtp);
        Task<BaseResponse<string>> ResendOTPAsync(string email);
        Task<BaseResponse<LoginResponse>> GoogleLoginAsync(GoogleLoginRequest request);
    }
}
