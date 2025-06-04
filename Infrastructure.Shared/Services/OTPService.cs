using Application.DTOs.Auth.Register;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services
{
    public class OTPService : IOTPService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly StackExchange.Redis.IDatabase _db;
        private readonly int _otpExpirationMinutes;

        public OTPService(IConnectionMultiplexer redis, IConfiguration configuration)
        {
            _redis = redis;
            _db = _redis.GetDatabase();
            _otpExpirationMinutes = configuration.GetValue<int>("Redis:OTPExpirationMinutes");
        }

        public async Task<bool> StoreOTPAsync(string key, string otp)
        {
            try
            {
                return await _db.StringSetAsync(key, otp, TimeSpan.FromMinutes(_otpExpirationMinutes));

            }
            catch (Exception)
            {

                throw new ApiException(ErrorMessage.Redis_Connection_Failed.GetMessage());
            }
        }

        public async Task<string?> GetOTPAsync(string key)
        {
            return await _db.StringGetAsync(key);
        }

        public async Task<bool> DeleteOTPAsync(string key)
        {
            return await _db.KeyDeleteAsync(key);
        }

        public async Task<bool> StoreRegisterDataAsync(string email, RegisterRequest request, string otp)
        {
            try
            {
                var data = new RegisterOtpData
                {
                    Request = request,
                    Otp = otp
                };

                var json = JsonSerializer.Serialize(data);
                return await _db.StringSetAsync($"register:{email}", json, TimeSpan.FromMinutes(_otpExpirationMinutes));
            }
            catch (Exception)
            {
                throw new ApiException(ErrorMessage.Redis_Connection_Failed.GetMessage());
            }
        }

        public async Task<RegisterOtpData> GetRegisterDataAsync(string email)
        {
            var key = $"register:{email}";
            var json = await _db.StringGetAsync(key);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

            try
            {
                var data = JsonSerializer.Deserialize<RegisterOtpData>(json!);
                if (data == null)
                    return null;

                return (data);
            }
            catch (Exception)
            {
                throw new ApiException("Failed to parse registration data from Redis.");
            }
        }

        public async Task DeleteRegisterDataAsync(string email)
        {
            await _db.KeyDeleteAsync($"register:{email}");
        }

        public async Task<bool> StoreForgetPasswordOtpAsync(string email, string otp)
        {
            try
            {
                return await _db.StringSetAsync($"forgetpass:{email}", otp, TimeSpan.FromMinutes(_otpExpirationMinutes));
            }
            catch
            {
                throw new ApiException(ErrorMessage.Redis_Connection_Failed.GetMessage());
            }
        }

        public async Task<string?> GetForgetPasswordOtpAsync(string email)
        {
            return await _db.StringGetAsync($"forgetpass:{email}");
        }

        public async Task DeleteForgetPasswordOtpAsync(string email)
        {
            await _db.KeyDeleteAsync($"forgetpass:{email}");
        }
    }
}
