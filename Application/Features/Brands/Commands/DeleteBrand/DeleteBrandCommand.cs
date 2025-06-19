using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.DeleteBrand
{
    public class DeleteBrandCommand : IRequest<BaseResponse<long>>
    {
        public long Id { get; set; }
    }

    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, BaseResponse<long>>
    {
        private readonly IBrandRepositoryAsync _brandRepository;

        public DeleteBrandCommandHandler(IBrandRepositoryAsync brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<BaseResponse<long>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetByIdAsync(request.Id);
            if (brand == null)
            {
                return new BaseResponse<long>($"Brand with Id {request.Id} not found.");
            }

            brand.IsActive = false; // Set IsActive to false instead of deleting

            await _brandRepository.UpdateAsync(brand);

            return new BaseResponse<long>(request.Id, "Brand deleted successfully.");
        }
    }
}