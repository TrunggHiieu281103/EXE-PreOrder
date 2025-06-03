using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using CategoryEntity = Domain.Entities.Categories;

namespace Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<BaseResponse<long>>
    {
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, BaseResponse<long>>
    {
        private readonly ICategoryRepositoryAsync _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepositoryAsync categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<long>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null)
            {
                return new BaseResponse<long>(0, $"Category with Id {request.Id} not found.");
            }

            category.CategoryName = request.CategoryName;
            category.Description = request.Description;
            category.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            await _categoryRepository.UpdateAsync(category);

            return new BaseResponse<long>(category.Id, "Category updated successfully.");
        }
    }
}
