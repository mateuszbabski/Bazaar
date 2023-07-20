using MediatR;
using Modules.Baskets.Application.Contracts;
using Modules.Baskets.Domain.Exceptions;
using Modules.Baskets.Domain.Repositories;
using Shared.Abstractions.CurrencyConverters;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;
using Shared.Domain.Rules;

namespace Modules.Baskets.Application.Commands.ChangeBasketCurrency
{
    public class ChangeBasketCurrencyCommandHandler : IRequestHandler<ChangeBasketCurrencyCommand, Guid>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IBasketRepository _basketRepository;
        private readonly ICurrencyConverter _currencyConverter;
        private readonly IBasketsUnitOfWork _unitOfWork;

        public ChangeBasketCurrencyCommandHandler(ICurrentUserService currentUserService,
                                                  IBasketRepository basketRepository,
                                                  ICurrencyConverter currencyConverter,
                                                  IBasketsUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _basketRepository = basketRepository;
            _currencyConverter = currencyConverter;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(ChangeBasketCurrencyCommand command, CancellationToken cancellationToken)
        {
            var customerId = _currentUserService.UserId;

            var basket = await _basketRepository.GetBasketByCustomerId(customerId)
                ?? throw new NotFoundException("Basket not found.");

            if (command.Currency.ToUpper() == basket.TotalPrice.Currency)
                return basket.Id;

            if (new SystemMustAcceptsCurrencyRule(command.Currency).IsBroken())
                throw new InvalidBasketPriceException("Invalid currency.");

            var conversionRate = await _currencyConverter.GetConversionRate(basket.TotalPrice.Currency, command.Currency);

            basket.ChangeBasketCurrency(conversionRate, command.Currency);

            await _unitOfWork.CommitAndDispatchDomainEventsAsync(basket);

            return basket.Id;
        }
    }
}
