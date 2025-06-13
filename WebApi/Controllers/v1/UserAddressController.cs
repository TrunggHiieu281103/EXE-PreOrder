using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Orders.Queries.GetOrderById;
using Application.Features.UserAddress.Commands.CreateUserAddress;
using Application.Features.UserAddress.Commands.SetDefaultUserAddress;
using Application.Features.UserAddress.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserAddressController : BaseApiController
    {
        /// <summary>
        /// Tạo địa chỉ người dùng mới
        /// </summary>
        /// <param name="command">Thông tin đăng nhập</param>
        /// <returns>id địa chỉ vừa mới tạo</returns>
        // GET: api/order
        // POST api/brand
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserAddressCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Lấy tất cả địa chỉ của người dùng theo userId
        /// </summary>
        /// <param name="userId">Thông tin đăng nhập</param>
        /// <returns>Danh sách địa chỉ theo userId</returns>
        // GET: api/order
        // GET: api/useraddress
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(long userId)
        {
            var result = await Mediator.Send(new GetAddressByUserIdQuery(userId));
            return Ok(result);
        }

        /// <summary>
        /// Chọn địa chỉ giao hàng default
        /// </summary>
        /// <param name="userId">thông tin user</param>
        /// <param name="userAddressId">Thông tin các địa chỉ của user đó</param>
        /// <returns>thông báo đã đổi địa chỉ default</returns>
        // GET: api/order
        // GET: api/useraddress
        [HttpPut("{userAddressId}")]
        public async Task<IActionResult> PutDefault(long userId, long userAddressId)
        {
            var result = await Mediator.Send(new SetDefaultUserAddressCommand(userId, userAddressId));
            return Ok(result);
        }
    }
}