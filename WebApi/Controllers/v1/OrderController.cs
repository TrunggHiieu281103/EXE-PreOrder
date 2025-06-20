
using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.Orders.Queries.GetOrderById;
using Application.Features.Orders.Queries.GetOrderByUserId;
using Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseApiController
    {
        /// <summary>
        /// Lấy tất cả đơn hàng
        /// </summary>
        /// <param name="filter">Thông tin đăng nhập</param>
        /// <returns>danh sách tất cả đơn hàng</returns>
        // GET: api/order
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllOrderParameter filter)
        {
            var query = new GetAllOrderQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Email = filter.Email
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Lấy chi tiết đơn hàng theo ID
        /// </summary>
        /// <param name="id">Id đơn hàng</param>
        /// <returns>Thông tin đơn hàng</returns>
        // GET: api/order
        // GET: api/order/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await Mediator.Send(new GetOrderByIdQuery(id));
            return Ok(result);
        }

        /// <summary>
        /// Tạo đơn hàng
        /// </summary>
        /// <param name="command">Thông tin đơn hàng cần tạo</param>
        /// <returns>id đơn hàng mới tạo</returns>
        // GET: api/order
        // POST: api/orders
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            var userId = User.GetUserId(); // lấy từ Claims
            if (userId == 0)
                return Unauthorized();

            command.SetUserId(userId);

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Lấy tất cả đơn hàng theo Id người dùng
        /// </summary>
        /// <param name="userId">Thông tin đăng nhập</param>
        /// <returns>Thông tin danh sách đơn của người dùng</returns>
        // GET: api/order
        // GET: api/order/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(long userId)
        {
            var result = await Mediator.Send(new GetOrderByUserIdQuery(userId));
            return Ok(result);
        }

        
    }
}