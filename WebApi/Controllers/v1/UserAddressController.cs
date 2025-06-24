using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Orders.Queries.GetOrderById;
using Application.Features.UserAddress.Commands.CreateUserAddress;
using Application.Features.UserAddress.Commands.SetDefaultUserAddress;
using Application.Features.UserAddress.Queries;
using Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class UserAddressController : BaseApiController
    {
        /// <summary>
        /// Tạo địa chỉ người dùng mới
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserAddressCommand command)
        {
            var userId = User.GetUserId();
            if (userId == 0)
                return Unauthorized();

            command.SetUserId(userId); // giả sử bạn có hàm SetUserId trong command
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Lấy tất cả địa chỉ của người dùng hiện tại (đã đăng nhập)
        /// </summary>
        [HttpGet("me")]
        public async Task<IActionResult> GetMyAddresses()
        {
            var userId = User.GetUserId();
            if (userId == 0)
                return Unauthorized();

            var result = await Mediator.Send(new GetAddressByUserIdQuery(userId));
            return Ok(result);
        }

        /// <summary>
        /// Chọn địa chỉ giao hàng mặc định
        /// </summary>
        [HttpPut("{userAddressId}/default")]
        public async Task<IActionResult> PutDefault(long userAddressId)
        {
            var userId = User.GetUserId();
            if (userId == 0)
                return Unauthorized();

            var result = await Mediator.Send(new SetDefaultUserAddressCommand(userId, userAddressId));
            return Ok(result);
        }
    }
}