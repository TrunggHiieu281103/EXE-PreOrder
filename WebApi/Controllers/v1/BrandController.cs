using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Commands.DeleteBrand;
using Application.Features.Brands.Commands.UpdateBrand;
using Application.Features.Brands.Queries.GetAllBrand;
using Application.Features.Brands.Queries.GetBrandById;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : BaseApiController
    {
        /// <summary>
        /// Lấy danh sách thương hiệu có phân trang và lọc theo tên.
        /// </summary>
        /// <param name="filter">Bộ lọc gồm số trang, kích thước trang và tên thương hiệu.</param>
        /// <returns>Danh sách thương hiệu.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllBrandsParameter filter)
        {
            var query = new GetAllBrandsQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                Name = filter.Name
            };

            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Lấy thông tin chi tiết của một thương hiệu theo ID.
        /// </summary>
        /// <param name="id">ID của thương hiệu.</param>
        /// <returns>Thông tin chi tiết thương hiệu.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await Mediator.Send(new GetBrandByIdQuery { Id = id });
            return Ok(result);
        }

        /// <summary>
        /// Tạo mới một thương hiệu.
        /// </summary>
        /// <param name="command">Thông tin thương hiệu cần tạo.</param>
        /// <returns>Thương hiệu được tạo cùng với ID.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBrandCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = result.Data }, result);
        }

        /// <summary>
        /// Cập nhật thông tin một thương hiệu.
        /// </summary>
        /// <param name="id">ID của thương hiệu cần cập nhật.</param>
        /// <param name="command">Thông tin mới của thương hiệu.</param>
        /// <returns>Kết quả cập nhật.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] UpdateBrandCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Xóa một thương hiệu theo ID.
        /// </summary>
        /// <param name="id">ID của thương hiệu cần xóa.</param>
        /// <returns>Kết quả xóa.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var command = new DeleteBrandCommand { Id = id };
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}