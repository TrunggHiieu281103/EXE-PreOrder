using Application.DTOs.Auth.Login;
using Application.DTOs.Auth.Register;
using Application.Features.Brands.Commands.CreateBrand;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
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
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Xác thực OTP khi đăng nhập lần đầu
        /// </summary>
        /// <param name="email">Email người dùng</param>
        /// <param name="otp">Mã OTP người dùng nhập</param>
        /// <returns>Thông tin đăng nhập và token nếu thành công</returns>
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromQuery] string email, [FromQuery] string otp)
        {
            var result = await _authService.VerifyOTPAsync(email, otp);
            return Ok(result);
        }

        /// <summary>
        /// Gửi lại mã OTP khi mã cũ hết hạn
        /// </summary>
        /// <param name="email">Email người dùng</param>
        /// <returns>Kết quả gửi lại OTP</returns>
        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOtp([FromQuery] string email)
        {
            var result = await _authService.ResendOTPAsync(email);
            return Ok(result);
        }
    }
}
