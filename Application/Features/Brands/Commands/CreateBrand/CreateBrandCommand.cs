using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand : IRequest<BaseResponse<long>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BaseResponse<long>>
    {
        private readonly IBrandRepositoryAsync _brandRepository;
        private readonly IMapper _mapper;

        public CreateBrandCommandHandler(IBrandRepositoryAsync brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<long>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            var brand = _mapper.Map<Domain.Entities.Brands>(request);

            brand.CreatedAt = now;
            brand.UpdatedAt = now;
            brand.IsActive = true;

            await _brandRepository.AddAsync(brand);
            return new BaseResponse<long>(brand.Id, "Brand created successfully");
        }
    }

}