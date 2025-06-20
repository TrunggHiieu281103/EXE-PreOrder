using Application.Features.PreOrder.Queries.GetAllPreOrders;
using Application.Features.User.Queries.GetAllUser;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Queries.GetAllUserQuery
{
    public class GetAllUserQuery : IRequest<PageResponse<IEnumerable<GetAllUserViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Email { get; set; }

        public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, PageResponse<IEnumerable<GetAllUserViewModel>>>
        {
            private readonly IUserRepositoryAsync _userRepository;
            private readonly IMapper _mapper;

            public GetAllUserQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<PageResponse<IEnumerable<GetAllUserViewModel>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
            {
                var parameter = new GetAllUserParameter
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    Email = request.Email
                };

                var users = await _userRepository.GetPagedUserResponseAsync(parameter);
                var userViewModels = _mapper.Map<IEnumerable<GetAllUserViewModel>>(users);

                return new PageResponse<IEnumerable<GetAllUserViewModel>>(userViewModels, request.PageNumber, request.PageSize);
            }
        }
    }
}
