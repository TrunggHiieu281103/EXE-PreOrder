using Application.Features.Dashboard.GetRevenueByDate;
using Application.Features.Dashboard.GetTotalOrders;
using Application.Features.Dashboard.GetTotalRevenue;
using Application.Features.Dashboard.TotalUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [Authorize(Roles = "ADMIN")]
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : BaseApiController
    {
        private readonly IMediator _mediator;

        public DashBoardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tổng số người dùng
        /// </summary>
        [HttpGet("total-users")]
        public async Task<IActionResult> GetTotalUsers()
        {
            var result = await _mediator.Send(new GetTotalUsersQuery());
            return Ok(result);
        }

        /// <summary>
        /// Tổng số đơn hàng
        /// </summary>
        [HttpGet("total-orders")]
        public async Task<IActionResult> GetTotalOrders()
        {
            var result = await _mediator.Send(new GetTotalOrdersQuery());
            return Ok(result);
        }

        /// <summary>
        /// Tổng doanh thu
        /// </summary>
        [HttpGet("total-revenue")]
        public async Task<IActionResult> GetTotalRevenue()
        {
            var result = await _mediator.Send(new GetTotalRevenueQuery());
            return Ok(result);
        }

        ///// <summary>
        ///// Top sản phẩm bán chạy
        ///// </summary>
        //[HttpGet("top-selling-products")]
        //public async Task<IActionResult> GetTopSellingProducts([FromQuery] int top = 5)
        //{
        //    var query = new GetTopSellingProductsQuery { Top = top };
        //    var result = await _mediator.Send(query);
        //    return Ok(result);
        //}

        /// <summary>
        /// Doanh thu theo tháng trong năm
        /// </summary>
        [HttpGet("monthly-revenue")]
        public async Task<IActionResult> GetMonthlyRevenue([FromQuery] int year = 0)
        {
            var query = new GetMonthlyRevenueQuery { Year = year == 0 ? DateTime.Now.Year : year };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("daily-revenue")]
        public async Task<IActionResult> GetDailyRevenue([FromQuery] int year, [FromQuery] int month)
        {
            var query = new GetDailyRevenueByMonthQuery { Year = year, Month = month };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
