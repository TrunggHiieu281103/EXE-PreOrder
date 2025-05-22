using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommand : IRequest<BaseResponse<long>>
    {
        public long Id { get; set; }  // Id cần để biết cập nhật brand nào
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, BaseResponse<long>>
    {
        private readonly IBrandRepositoryAsync _brandRepository;
        private readonly IMapper _mapper;

        public UpdateBrandCommandHandler(IBrandRepositoryAsync brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<long>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            // Lấy brand cần cập nhật
            var brand = await _brandRepository.GetByIdAsync(request.Id);
            if (brand == null)
            {
                return new BaseResponse<long>(0, $"Brand with Id {request.Id} not found.");
            }

            // Cập nhật các trường
            brand.Name = request.Name;
            brand.Description = request.Description;
            brand.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            await _brandRepository.UpdateAsync(brand);

            return new BaseResponse<long>(brand.Id, "Brand updated successfully");
        }
    }
}