using Application.Enums;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
