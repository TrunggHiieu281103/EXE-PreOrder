using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductByIdCommand : IRequest<BaseResponse<long>>
    {
        public long Id { get; set; }
        public bool? IsActive { get; set; }


        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, BaseResponse<long>>
        {
            private readonly IProductRepositoryAsync _productRepository;
            public DeleteProductByIdCommandHandler(IProductRepositoryAsync productRepositoryAsync)
            {
                _productRepository = productRepositoryAsync;
            }

            public async Task<BaseResponse<long>> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(command.Id);

                if (product == null)
                {
                    throw new ApiException($"Product Not Found.");
                }
                else
                {
                    product.IsActive = false;
                    await _productRepository.UpdateAsync(product);
                    return new BaseResponse<long>(product.Id, $"Product deleted successfully.");
                }
            }
        }
    }
}
