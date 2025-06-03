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
    }
}
