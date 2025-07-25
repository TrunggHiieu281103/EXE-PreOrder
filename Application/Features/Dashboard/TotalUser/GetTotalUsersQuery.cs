using Application.Features.Categories.Queries.GetCategoryById;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Dashboard.TotalUser
{
    public class GetTotalUsersQuery : IRequest<BaseResponse<GetTotalUsersViewModel>>
    {


        public class GetTotalUsersHandler : IRequestHandler<GetTotalUsersQuery, BaseResponse<GetTotalUsersViewModel>>
        {
            private readonly IUserRepositoryAsync _userRepository;

            public GetTotalUsersHandler(IUserRepositoryAsync userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<BaseResponse<GetTotalUsersViewModel>> Handle(GetTotalUsersQuery request, CancellationToken cancellationToken)
            {
                var totalUsers = await _userRepository.CountAllUsersAsync();
                var activeUsers = await _userRepository.CountActiveUsersAsync();
                var inactiveUsers = await _userRepository.CountInactiveUsersAsync();
                var newUsersThisMonth = await _userRepository.CountNewUsersThisMonthAsync();
                var adminCount = await _userRepository.CountUsersByRoleAsync("Admin");

                var result = new GetTotalUsersViewModel
                {
                    TotalUsers = totalUsers,
                    ActiveUsers = activeUsers,
                    InactiveUsers = inactiveUsers,
                    NewUsersThisMonth = newUsersThisMonth,
                    AdminCount = adminCount
                };

                return new BaseResponse<GetTotalUsersViewModel>(result, "Get total user success");
            }
        }
    }
    
}
