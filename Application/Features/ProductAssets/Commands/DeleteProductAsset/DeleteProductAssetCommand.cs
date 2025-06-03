using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductAssets.Commands.DeleteProductAsset
{
    public class DeleteProductAssetCommand : IRequest<BaseResponse<long>>
    {
        public long Id { get; set; }

        public class DeleteProductAssetCommandHandler : IRequestHandler<DeleteProductAssetCommand, BaseResponse<long>>
        {
            private readonly IProductAssetsRepositoryAsync _productAssetsRepository;

            public DeleteProductAssetCommandHandler(IProductAssetsRepositoryAsync productAssetsRepository)
            {
                _productAssetsRepository = productAssetsRepository;
            }

            public async Task<BaseResponse<long>> Handle(DeleteProductAssetCommand request, CancellationToken cancellationToken)
            {
                var asset = await _productAssetsRepository.GetByIdAsync(request.Id);
                if (asset == null)
                    return new BaseResponse<long>("Asset not found.");

                asset.IsActive = false;
                await _productAssetsRepository.UpdateAsync(asset);

                return new BaseResponse<long>(asset.Id, "Asset has been soft deleted.");
            }
        }
    }
}
