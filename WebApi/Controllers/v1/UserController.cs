using Application.Features.User.Commands.CreateUserAdmin;
using Application.Features.User.Commands.DeleteUser;
using Application.Features.User.Commands.SetActiveUser;
using Application.Features.User.Commands.UpdateUser;
using Application.Features.User.Commands.UpdateUserAvatar;
using Application.Features.User.Queries.GetAllUser;
using Application.Features.User.Queries.GetAllUserQuery;
using Application.Features.User.Queries.GetUserById;
using Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        /// <summary>
        /// Lấy danh sách người dùng (chỉ ADMIN)
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
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

        /// <summary>
        /// Lấy thông tin chi tiết người dùng theo ID (chỉ ADMIN)
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetById(long id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Cập nhật thông tin người dùng (không yêu cầu ADMIN)
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(long id, [FromBody] UpdatePersonalUserInfoCommand command)
        {
            var userId = User.GetUserId();

            if (userId == 0)
                return Unauthorized("User not authenticated.");

            if (userId != id)
                return Forbid("You are not allowed to update other users' information.");

            command.SetUserId(userId);

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Vô hiệu hóa người dùng (chỉ ADMIN, không được vô hiệu hóa người có role ADMIN)
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(long id)
        {
            var command = new DeleteUserCommand { Id = id };
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Khích hoạt lại tài khoản người dùng (chỉ ADMIN)
        /// </summary>
        [HttpPatch("{id}/user-recover")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Recover(long id)
        {
            var command = new SetActiveUserCommand { UserId = id };
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Tạo người dùng mới (chỉ ADMIN, người dùng được tạo sẽ mang role ADMIN)
        /// </summary>
        [HttpPost("create-admin")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] CreateUserAdminCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
        }

        /// <summary>
        /// Cập nhật ảnh đại diện người dùng (thay thế ảnh cũ)
        /// </summary>
        [HttpPut("avatar")]
        [Authorize]
        public async Task<IActionResult> UpdateAvatar([FromForm] UpdateUserAvatarCommand command)
        {
            var userId = User.GetUserId();
            if (userId == 0)
                return Unauthorized("User not authenticated.");

            command.UserId = userId; 

            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
