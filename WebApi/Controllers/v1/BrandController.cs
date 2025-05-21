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
        // GET: api/brand
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllBrandsParameters filter)
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

        // GET api/brand/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await Mediator.Send(new GetBrandByIdQuery { Id = id });
            return Ok(result);
        }


        // POST api/brand
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBrandCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = result.Data }, result);
        }

        // PUT api/brand/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] UpdateBrandCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            var result = await Mediator.Send(command);
            if (!result.Succeeded)
                return NotFound(result.Message);

            return Ok(result);
        }

        // DELETE api/brand/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var command = new DeleteBrandCommand { Id = id };
            var result = await Mediator.Send(command);
            if (!result.Succeeded)
                return NotFound(result.Message);

            return NoContent();
        }
    }
}
