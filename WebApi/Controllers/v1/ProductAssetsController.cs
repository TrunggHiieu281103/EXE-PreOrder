using Application.Features.ProductAssets.Queries.GetAssetsByProductId;
using Application.Features.Products.Queries.GetAllProduct;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductAssetsController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] long productId)
        {

            return Ok(await Mediator.Send(new GetAssetsByProductIdQuery()
            {
                Id = productId
            }));
        }
    }
}
