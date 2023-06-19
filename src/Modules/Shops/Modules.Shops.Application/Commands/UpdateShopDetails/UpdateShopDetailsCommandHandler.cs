using MediatR;
using Modules.Shops.Domain.Repositories;
using Shared.Abstractions.UnitOfWork;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Modules.Shops.Application.Commands.UpdateShopDetails
{
    public class UpdateShopDetailsCommandHandler : IRequestHandler<UpdateShopDetailsCommand>
    {
        private readonly ICurrentUserService _userService;
        private readonly IShopRepository _shopRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateShopDetailsCommandHandler(ICurrentUserService userService,
                                               IShopRepository shopRepository,
                                               IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _shopRepository = shopRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateShopDetailsCommand command, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;
            var shop = await _shopRepository.GetShopById(command.Id);

            if (shop == null || shop.Id.Value != userId)
            {
                throw new BadRequestException("Shop not found");
            }

            shop.UpdateShopDetails(command.OwnerName,
                                   command.OwnerLastName,
                                   command.ShopName,
                                   command.Country,
                                   command.City,
                                   command.Street,
                                   command.PostalCode,
                                   command.TaxNumber,
                                   command.ContactNumber);

            await _unitOfWork.CommitAndDispatchEventsAsync();
        }
    }
}
