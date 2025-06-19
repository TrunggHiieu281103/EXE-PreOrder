using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<BaseResponse<long>>
    {
        public long Id { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, BaseResponse<long>>
    {
        private readonly ICategoryRepositoryAsync _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepositoryAsync categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<BaseResponse<long>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null)
            {
                return new BaseResponse<long>(0, $"Category with Id {request.Id} not found.");
            }

            category.IsActive = false;

            await _categoryRepository.UpdateAsync(category);
            return new BaseResponse<long>(category.Id, "Category deleted successfully.");
        }
    }
}
