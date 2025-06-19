using Application.Wrappers;
using AutoMapper;
using MediatR;
using Application.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Features.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryQuery : IRequest<PageResponse<IEnumerable<GetAllCategoryViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? CategoryName { get; set; } // optional filter
    }

    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, PageResponse<IEnumerable<GetAllCategoryViewModel>>>
    {
        private readonly ICategoryRepositoryAsync _categoryRepository;
        private readonly IMapper _mapper;

        public GetAllCategoryQueryHandler(ICategoryRepositoryAsync categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<PageResponse<IEnumerable<GetAllCategoryViewModel>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var filter = new GetAllCategoryParameter
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                CategoryName = request.CategoryName
            };

            var categories = await _categoryRepository.GetCategoryPagedResponseAsync(filter);
            var viewModel = _mapper.Map<IEnumerable<GetAllCategoryViewModel>>(categories);

            return new PageResponse<IEnumerable<GetAllCategoryViewModel>>(viewModel, filter.PageNumber, filter.PageSize);
        }
    }
}
