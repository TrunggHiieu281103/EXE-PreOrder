using Application.Features.Orders.Commands.CreateOrder;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserAddress.Commands.CreateUserAddress
{
    public class CreateUserAddressCommand : IRequest<BaseResponse<long>>
    {
        public long UserId { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string AddressDetail { get; set; }
        //public bool IsDefault { get; set; }
    }

    public class CreateUserAddressCommandHandler : IRequestHandler<CreateUserAddressCommand, BaseResponse<long>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IUserAddressRepositoryAsync _userAddressRepository;
        public CreateUserAddressCommandHandler(IUserRepositoryAsync userRepositoryAsync, IUserAddressRepositoryAsync userAddressRepository)
        {
            _userRepository = userRepositoryAsync;
            _userAddressRepository = userAddressRepository;
        }

        public async Task<BaseResponse<long>> Handle(CreateUserAddressCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
   
            var newAddress = new Domain.Entities.UserAddresses
            {
                UserId = request.UserId,
                Province = request.Province,
                District = request.District,
                Ward = request.Ward,
                AddressDetail = request.AddressDetail,
                IsDefault = false // Mặc định là false, có thể thêm logic để đặt địa chỉ mặc định nếu cần
            };
            await _userAddressRepository.AddAsync(newAddress);
            return new BaseResponse<long>(newAddress.Id, "Add address successfully");
        }
    }
}
