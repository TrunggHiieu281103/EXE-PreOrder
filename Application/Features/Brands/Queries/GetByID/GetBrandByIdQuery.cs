using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetBrandById
{
    public class GetBrandByIdQuery : IRequest<BaseResponse<GetBrandByIdViewModel>>
    {
        public long Id { get; set; }
    }

    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, BaseResponse<GetBrandByIdViewModel>>
    {
        private readonly IBrandRepositoryAsync _brandRepository;
        private readonly IMapper _mapper;

        public GetBrandByIdQueryHandler(IBrandRepositoryAsync brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<GetBrandByIdViewModel>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetByIdAsync(request.Id);

            if (brand == null)
                return new BaseResponse<GetBrandByIdViewModel>(null, $"Brand with Id {request.Id} not found.");

            var viewModel = _mapper.Map<GetBrandByIdViewModel>(brand);
            return new BaseResponse<GetBrandByIdViewModel>(viewModel);
        }
    }
}
