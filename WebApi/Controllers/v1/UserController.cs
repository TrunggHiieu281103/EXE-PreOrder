using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.User.Queries.GetAllUser;
using Application.Features.User.Queries.GetAllUserQuery;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllUserParameter filter)
        {
            var query = new GetAllUserQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Email = filter.Email
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }    
}