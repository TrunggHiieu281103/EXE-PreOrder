using Application.Features.Orders.Commands.CreateOrder;
using Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserAddress.Commands.CreateUserAddress
{
    public class CreateUserAddressCommandValidator : AbstractValidator<CreateUserAddressCommand>
    {
        private readonly IUserRepositoryAsync _userRepository;

        public CreateUserAddressCommandValidator(IUserRepositoryAsync userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .GreaterThan(0).WithMessage("UserId must be greater than 0.")
                .MustAsync(UserExists).WithMessage("UserId does not exist.");

            RuleFor(x => x.Province)
                .NotEmpty().WithMessage("Province is required.")
                .MaximumLength(100).WithMessage("Province must not exceed 100 characters.");

            RuleFor(x => x.District)
                .NotEmpty().WithMessage("District is required.")
                .MaximumLength(100).WithMessage("District must not exceed 100 characters.");

            RuleFor(x => x.Ward)
                .NotEmpty().WithMessage("Ward is required.")
                .MaximumLength(100).WithMessage("Ward must not exceed 100 characters.");

            RuleFor(x => x.AddressDetail)
                .NotEmpty().WithMessage("Address detail is required.")
                .MaximumLength(250).WithMessage("Address detail must not exceed 250 characters.");
        }

        private async Task<bool> UserExists(long userId, CancellationToken cancellationToken)
        {
            return await _userRepository.FindUserById(userId);
        }
    }
}