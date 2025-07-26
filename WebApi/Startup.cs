using Application;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Identity;
using Identity.Services;
using Infrastructure.Shared;
using Microsoft.AspNetCore.Identity;
using Persistence;
using Persistence.Repositories;
using System.Text.Json.Serialization;
using WebApi.Extensions;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration _config { get; }

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // ✅ CORS config
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", builder =>
                {
                    builder
                        .WithOrigins("http://gundam.thanhnt-tech.id.vn")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });


            services.AddApplicationLayer();
            services.AddIdentityInfrastructure(_config);
            services.AddPersistenceInfrastructure(_config);
            services.AddSharedInfrastructure(_config);
            services.AddSwaggerExtension();
            services.AddHttpContextAccessor();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddApiVersioningExtension();
            services.AddHealthChecks();

            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddScoped<IBrandRepositoryAsync, BrandRepositoryAsync>();
            services.AddScoped<IPasswordHasher<Users>, PasswordHasher<Users>>();
            services.AddTransient<ICategoryRepositoryAsync, CategoryRepositoryAsync>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // 🔥 CORS phải đặt ngay sau UseRouting và trước UseAuthentication / UseAuthorization
            app.UseRouting();
            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseSwaggerExtension();
            app.UseErrorHandlingMiddleware();
            app.UseHealthChecks("/health");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
