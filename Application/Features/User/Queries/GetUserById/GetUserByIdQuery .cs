using Application.Features.User.Queries.GetUserByIdQuery;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.User.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<BaseResponse<GetUserByIdViewModel>>
    {
        public long Id { get; set; }

        public class Handler : IRequestHandler<GetUserByIdQuery, BaseResponse<GetUserByIdViewModel>>
        {
            private readonly IUserRepositoryAsync _userRepository;
            private readonly IMapper _mapper;

            public Handler(IUserRepositoryAsync userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<BaseResponse<GetUserByIdViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUserWithRolesByIdAsync(request.Id);
                if (user == null)
                    return new BaseResponse<GetUserByIdViewModel>("User not found.");

                var vm = _mapper.Map<GetUserByIdViewModel>(user);
                return new BaseResponse<GetUserByIdViewModel>(vm, "User retrieved successfully.");
            }
        }
    }
}
