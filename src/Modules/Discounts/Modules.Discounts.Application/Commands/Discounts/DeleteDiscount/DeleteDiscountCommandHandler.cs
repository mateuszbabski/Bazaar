using MediatR;
using Modules.Discounts.Application.Contracts;
using Modules.Discounts.Domain.Repositories;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Modules.Discounts.Application.Commands.Discounts.DeleteDiscount
{
    public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, Unit>
    {
        private readonly ICurrentUserService _userService;
        private readonly IDiscountRepository _discountRepository;
        private readonly IDiscountsUnitOfWork _unitOfWork;

        public DeleteDiscountCommandHandler(ICurrentUserService userService,
                                            IDiscountRepository discountRepository,
                                            IDiscountsUnitOfWork unitOfWork)
        {
            _userService = userService;
            _discountRepository = discountRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteDiscountCommand command, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;

            var discount = await _discountRepository.GetDiscountById(command.DiscountId)
                ?? throw new NotFoundException("Discount not found.");

            if (discount.CreatedBy != userId)
            {
                throw new ForbidException("You dont have an access to proceed this action");
            }

            _discountRepository.Delete(discount);
            await _unitOfWork.CommitChangesAsync();

            return Unit.Value;
        }
    }
}
