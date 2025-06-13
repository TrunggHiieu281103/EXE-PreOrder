using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserAddress.Commands.SetDefaultUserAddress
{
    public class SetDefaultUserAddressCommand : IRequest<BaseResponse<long>>
    {
        public long UserId { get; set; }
        public long UserAddressId { get; set; }

        public SetDefaultUserAddressCommand(long userId, long userAddressId)
        {
            UserId = userId;
            UserAddressId = userAddressId;
        }

        public class SetDefaultUserAddressCommandHandler : IRequestHandler<SetDefaultUserAddressCommand, BaseResponse<long>>
        {
            private readonly IUserAddressRepositoryAsync _userAddressRepositoryAsync;
            public SetDefaultUserAddressCommandHandler(IUserAddressRepositoryAsync userAddressRepositoryAsync)
            {
                _userAddressRepositoryAsync = userAddressRepositoryAsync;
            }
            public async Task<BaseResponse<long>> Handle(SetDefaultUserAddressCommand request, CancellationToken cancellationToken)
            {
                var listUserAddress = await _userAddressRepositoryAsync.GetAllAddressByUserIdAsync(request.UserId);

                if (listUserAddress == null || !listUserAddress.Any())
                {
                    return new BaseResponse<long>("No addresses found for this user.");
                }

                var newDefaultAddress = listUserAddress.FirstOrDefault(a => a.Id == request.UserAddressId);
                if (newDefaultAddress == null)
                {
                    return new BaseResponse<long>("Address not found.");
                }

                foreach (var address in listUserAddress)
                {
                    address.IsDefault = address.Id == request.UserAddressId;
                    await _userAddressRepositoryAsync.UpdateAsync(address); // Cập nhật từng địa chỉ
                }

                return new BaseResponse<long>(newDefaultAddress.Id, "Default address set successfully.");
            }

        }

    }
}
