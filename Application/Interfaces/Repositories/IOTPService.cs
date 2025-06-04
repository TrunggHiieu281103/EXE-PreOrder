using Application.DTOs.Auth.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOTPService
    {
        Task<bool> StoreOTPAsync(string key, string otp);
        Task<string?> GetOTPAsync(string key);
        Task<bool> DeleteOTPAsync(string key);
        Task<bool> StoreRegisterDataAsync(string email, RegisterRequest request, string otp);
        Task<RegisterOtpData> GetRegisterDataAsync(string email);
        Task DeleteRegisterDataAsync(string email);
        Task<bool> StoreForgetPasswordOtpAsync(string email, string otp);
        Task<string?> GetForgetPasswordOtpAsync(string email);
        Task DeleteForgetPasswordOtpAsync(string email);
    }
}
