using Application.Features.Products.Queries.GetProductById;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserAddress.Queries
{
    public class GetAddressByUserIdQuery : IRequest<BaseResponse<List<GetAddressByUserIdViewModel>>>
    {
        public GetAddressByUserIdQuery(long userId)
        {
            UserId = userId;
        }

        public long UserId { get; set; }

        public class GetAddressByUserIdQueryHandler : IRequestHandler<GetAddressByUserIdQuery, BaseResponse<List<GetAddressByUserIdViewModel>>>
        {
            private readonly IUserAddressRepositoryAsync _userAddressRepositoryAsync;
            private readonly IMapper _mapper;

            public GetAddressByUserIdQueryHandler(IUserAddressRepositoryAsync userAddressRepositoryAsync, IMapper mapper)
            {
                _userAddressRepositoryAsync = userAddressRepositoryAsync;
                _mapper = mapper;
            }

            public async Task<BaseResponse<List<GetAddressByUserIdViewModel>>> Handle(GetAddressByUserIdQuery request, CancellationToken cancellationToken)
            {
                var userAddress = await _userAddressRepositoryAsync.GetAllAddressByUserIdAsync(request.UserId);

                if (userAddress == null || !userAddress.Any())
                {
                    return new BaseResponse<List<GetAddressByUserIdViewModel>>("No address found for this user.");
                }

                var addressViewModel = _mapper.Map<List<GetAddressByUserIdViewModel>>(userAddress);

                return new BaseResponse<List<GetAddressByUserIdViewModel>>(addressViewModel, "Address retrieved successfully.");
            }
        }

    }
}

