using Application.DTOs.Auth.ChangePassword;
using Application.DTOs.Auth.ForgetPassword;
using Application.DTOs.Auth.Login;
using Application.DTOs.Auth.Register;
using Application.DTOs.Google;
using Application.DTOs.OTP;
using Application.Features.Brands.Commands.CreateBrand;
using Application.Interfaces;
using Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Đăng nhập bằng email hoặc số điện thoại
        /// </summary>
        /// <param name="request">Thông tin đăng nhập</param>
        /// <returns>Thông tin người dùng và token</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Application.DTOs.Auth.Login.LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Đăng ký người dùng mới
        /// </summary>
        /// <param name="request">Thông tin đăng ký</param>
        /// <returns>Kết quả đăng ký</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Application.DTOs.Auth.Register.RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Xác thực OTP khi tạo tài khoản
        /// </summary>
        /// <param name="email">Email người dùng</param>
        /// <param name="otp">Mã OTP người dùng nhập</param>
        /// <returns>Thông tin đăng nhập và token nếu thành công</returns>
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            var result = await _authService.VerifyOTPAsync(request.Email, request.Otp);
            return Ok(result);
        }

        /// <summary>
        /// Gửi lại mã OTP khi mã cũ hết hạn
        /// </summary>
        /// <param name="email">Email người dùng</param>
        /// <returns>Kết quả gửi lại OTP</returns>
        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOtp([FromBody] ResendOtpRequest request)
        {
            var result = await _authService.ResendOTPAsync(request.Email);
            return Ok(result);
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            var result = await _authService.GoogleLoginAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Gửi OTP để reset mật khẩu
        /// </summary>
        /// <param name="request">Email người dùng</param>
        /// <returns>Kết quả gửi OTP</returns>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] SendOtpRequest request)
        {
            var result = await _authService.SendForgotPasswordOTPAsync(request.Email);
            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Xác thực OTP để reset lại password
        /// </summary>
        /// <param name="request">Email và mã OTP</param>
        /// <returns>Kết quả xác thực</returns>
        [HttpPost("verify-forgot-password-otp")]
        public async Task<IActionResult> VerifyForgotPasswordOtp([FromBody] Application.DTOs.Auth.ForgetPassword.ResetPasswordRequest request)
        {
            var result = await _authService.VerifyForgetPasswordOtpAsync(request.Email, request.Otp, request.NewPassword, request.ConfirmPassword);
            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }


        //[HttpPost("change-password")]
        //[Authorize]
        //public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        //{
        //    var result = await _authService.ChangePasswordAsync(User, request);
        //    if (!result.Succeeded)
        //    return BadRequest(result);

        //    return Ok(result);
        //}

        /// <summary>
        /// Lấy thông tin người dùng hiện tại (profile)
        /// </summary>
        /// <param name="request">Mật khẩu cũ và mới</param>
        /// <returns>User profile</returns>
        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.GetUserId();
            var userEmail = User.GetEmail();
            //var userRole = User.FindFirst(ClaimTypes.Role)?.Value; 
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("User not authenticated");
            }

            // Assuming you have a method to get user profile by username
            var profile = await _authService.GetUserProfileAsync(userId);
            if (profile == null)
            {
                return NotFound("User profile not found");
            }
            return Ok(profile);
        }
    }
}