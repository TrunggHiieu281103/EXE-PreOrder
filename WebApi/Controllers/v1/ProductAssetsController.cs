using Application.Features.ProductAssets.Commands.DeleteProductAsset;
using Application.Features.ProductAssets.Commands.UpdateProductAsset;
using Application.Features.ProductAssets.Commands.UploadProductAsset;
using Application.Features.ProductAssets.Queries.GetAssetById;
using Application.Features.ProductAssets.Queries.GetAssetsByProductId;
using Application.Features.Products.Queries.GetAllProduct;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductAssetsController : BaseApiController
    {
        // GET: api/<controller>/productId=2
        /// <summary>
        /// Lấy ảnh theo product Id
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <returns>List of product assets.</returns>
        [HttpGet("by-product/{productId}")]
        public async Task<IActionResult> GetByProductId([FromRoute] long productId)
        {

            return Ok(await Mediator.Send(new GetAssetsByProductIdQuery()
            {
                Id = productId
            }));
        }

        // GET api/<controller>/5
        /// <summary>
        /// Lấy ảnh theo Id
        /// </summary>
        /// <param name="id">The ID of the asset.</param>
        /// <returns>The asset.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await Mediator.Send(new GetAssetByIdQuery { Id = id }));
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsset([FromForm] UploadProductAssetCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("update/{assetId}")]
        public async Task<IActionResult> UpdateAsset(long assetId, [FromForm] UpdateProductAssetCommand command)
        {
            command.Id = assetId;
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("delete/{assetId}")]
        public async Task<IActionResult> DeleteAsset(long assetId)
        {
            return Ok(await Mediator.Send(new DeleteProductAssetCommand { Id = assetId }));
        }
    }
}
