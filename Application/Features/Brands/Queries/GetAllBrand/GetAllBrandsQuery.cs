using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetAllBrand
{
    public class GetAllBrandsQuery : IRequest<PageResponse<IEnumerable<GetAllBrandsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Name { get; set; }
    }

    public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, PageResponse<IEnumerable<GetAllBrandsViewModel>>>
    {
        private readonly IBrandRepositoryAsync _brandRepository;
        private readonly IMapper _mapper;

        public GetAllBrandsQueryHandler(IBrandRepositoryAsync brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<PageResponse<IEnumerable<GetAllBrandsViewModel>>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            // Map request sang parameter filter nếu cần, hoặc dùng chính request
            var filter = new GetAllBrandsParameter
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Name = request.Name
            };

            // Lấy dữ liệu phân trang từ repo, cần bạn implement method này
            var (brands, totalItems) = await _brandRepository.GetBrandPagedReponseWithAssetsAsync(filter);

            // Map sang ViewModel
            var brandViewModels = _mapper.Map<IEnumerable<GetAllBrandsViewModel>>(brands);

            return new PageResponse<IEnumerable<GetAllBrandsViewModel>>(brandViewModels, filter.PageNumber, filter.PageSize,totalItems);
        }
    }
}