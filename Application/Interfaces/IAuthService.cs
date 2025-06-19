using Application.DTOs.Auth;
using Application.DTOs.Auth.ChangePassword;
using Application.DTOs.Auth.ForgetPassword;
using Application.DTOs.Auth.Login;
using Application.DTOs.Auth.Register;
using Application.DTOs.Google;
using Application.DTOs.OTP;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        Task<BaseResponse<string>> SendForgotPasswordOTPAsync(string email);
        Task<BaseResponse<string>> VerifyForgetPasswordOtpAsync(string email, string otp, string newPassword, string confirmPassword);

        //Task<ResponseDto> ChangePasswordAsync(ClaimsPrincipal user, ChangePasswordRequest request);
        Task<BaseResponse<UserProfileDto>> GetUserProfileAsync(long userId);

    }

}
