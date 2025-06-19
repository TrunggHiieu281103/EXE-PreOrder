using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CategoryEntity = Domain.Entities.Categories; // 👈 alias rõ ràng

namespace Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<BaseResponse<long>>
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, BaseResponse<long>>
    {
        private readonly ICategoryRepositoryAsync _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepositoryAsync categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<long>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<CategoryEntity>(request);

            category.CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            await _categoryRepository.AddAsync(category);

            return new BaseResponse<long>(category.Id, "Category created successfully.");
        }
    }
}
