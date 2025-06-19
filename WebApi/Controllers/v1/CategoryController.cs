using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Commands.DeleteCategory;
using Application.Features.Categories.Commands.UpdateCategory;
using Application.Features.Categories.Queries.GetAllCategory;
using Application.Features.Categories.Queries.GetCategoryById;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseApiController
    {
        // GET: api/category
        /// <summary>
        /// Lấy danh sách tất cả danh mục có phân trang và lọc theo tên.
        /// </summary>
        /// <param name="filter">Bộ lọc gồm số trang, kích thước trang và tên danh mục.</param>
        /// <returns>Danh sách danh mục.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllCategoryParameter filter)
        {
            var query = new GetAllCategoryQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                CategoryName = filter.CategoryName
            };

            var result = await Mediator.Send(query);
            return Ok(result);
        }

        // GET: api/category/{id}
        /// <summary>
        /// Lấy thông tin chi tiết của một danh mục theo ID.
        /// </summary>
        /// <param name="id">ID của danh mục cần lấy.</param>
        /// <returns>Thông tin danh mục tương ứng.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await Mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(result);
        }

        // POST: api/category
        /// <summary>
        /// Tạo mới một danh mục.
        /// </summary>
        /// <param name="command">Thông tin danh mục cần tạo.</param>
        /// <returns>Kết quả tạo mới danh mục.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // PUT: api/category/{id}
        /// <summary>
        /// Cập nhật thông tin một danh mục.
        /// </summary>
        /// <param name="id">ID danh mục cần cập nhật (trong URL).</param>
        /// <param name="command">Thông tin danh mục mới.</param>
        /// <returns>Kết quả cập nhật danh mục.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateCategoryCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID in URL does not match ID in request body.");
            }

            var result = await Mediator.Send(command);
            return Ok(result);
        }

        // DELETE: api/category/{id}
        /// <summary>
        /// Xóa một danh mục theo ID.
        /// </summary>
        /// <param name="id">ID của danh mục cần xóa.</param>
        /// <returns>Kết quả xóa danh mục.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await Mediator.Send(new DeleteCategoryCommand { Id = id });
            return Ok(result);
        }

    }
}
