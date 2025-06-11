
using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.Orders.Queries.GetOrderById;
using Application.Features.Orders.Queries.GetOrderByUserId;
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
        /// Lấy đơn hàng theo ID
        /// </summary>
        /// <param name="id">Thông tin đăng nhập</param>
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
        /// <param name="command">Thông tin đăng nhập</param>
        /// <returns>id đơn hàng mới tạo</returns>
        // GET: api/order
        // POST: api/orders
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        //// PUT: api/order/{id}/confirm
        //[HttpPut("{id}/confirm")]
        //public async Task<IActionResult> Confirm(long id)
        //{
        //    var result = await Mediator.Send(new ConfirmOrderCommand(id));
        //    return Ok(result);
        //}

        //// PUT: api/order/{id}/status
        //[HttpPut("{id}/status")]
        //public async Task<IActionResult> UpdateStatus(long id, [FromBody] UpdateOrderStatusCommand command)
        //{
        //    if (id != command.Id)
        //        return BadRequest("ID in URL does not match ID in request body.");

        //    var result = await Mediator.Send(command);
        //    return Ok(result);
        //}

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

        //// GET: api/order/status/{status}
        //[HttpGet("status/{status}")]
        //public async Task<IActionResult> GetOrdersByStatus(string status)
        //{
        //    var result = await Mediator.Send(new GetOrdersByStatusQuery(status));
        //    return Ok(result);
        //}

        //// GET: api/order/statistics
        //[HttpGet("statistics")]
        //public async Task<IActionResult> GetStatistics()
        //{
        //    var result = await Mediator.Send(new GetOrderStatisticsQuery());
        //    return Ok(result);
        //}

        //// GET: api/order/export
        //[HttpGet("export")]
        //public async Task<IActionResult> ExportOrdersToExcel()
        //{
        //    var file = await Mediator.Send(new ExportOrderQuery());
        //    return File(file.Content, file.ContentType, file.FileName);
        //}

        //// PUT: api/order/{id}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(long id, [FromBody] UpdateOrderCommand command)
        //{
        //    if (id != command.Id)
        //    {
        //        return BadRequest("ID in URL does not match ID in request body.");
        //    }
        //    var result = await Mediator.Send(command);
        //    return Ok(result);
        //}
        //// DELETE: api/order/{id}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(long id)
        //{
        //    var result = await Mediator.Send(new DeleteOrderCommand(id));
        //    return Ok(result);
        //}
    }
}