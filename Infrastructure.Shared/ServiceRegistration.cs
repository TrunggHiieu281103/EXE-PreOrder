using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Settings;
using Infrastructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using VNPAY.NET;


namespace Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.Configure<CloudinarySettings>(_config.GetSection("Cloudinary"));
            // Redis for storing OTP
            var redisConnectionString = _config.GetValue<string>("Redis:ConnectionString");
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));

            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IOTPService, OTPService>();
            services.AddTransient<IVnpayPaymentService, VnpayPaymentService>();
            services.AddTransient<IVnpay, Vnpay>();
            services.AddTransient<IPendingOrderService, PendingOrderService>();


        }
    }
}
