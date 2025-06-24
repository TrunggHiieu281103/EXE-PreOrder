
using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.PreOrder.Commands.CreatePreOrder;
using Application.Features.PreOrder.Queries.GetAllPreOrders;
using Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreOrderController : BaseApiController
    {
        /// <summary>
        /// Tạo đơn hàng đặt trước
        /// </summary>
        /// <param name="command">Thông tin đơn hàng cần tạo</param>
        /// <returns>id đơn hàng mới tạo</returns>
        // POST: api/preorders
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePreOrderCommand command)
        {
            var userId = User.GetUserId(); // lấy từ Claims
            if (userId == 0)
                return Unauthorized();

            command.SetUserId(userId); // gán vào command

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Lấy tất cả đơn hàng đặt trước (chỉ ADMIN)
        /// </summary>
        /// <param name="filter">Thông tin đơn hàng cần tạo</param>
        /// <returns>danh sách tất cả đơn hàng đặt trước</returns>
        // GET: api/order
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllPreOrderParameter filter)
        {
            var query = new GetAllPreOrderQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                UserEmail = filter.UserEmail
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

    }
}