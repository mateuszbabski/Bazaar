using MediatR;

namespace Modules.Baskets.Application.Commands.ChangeBasketCurrency
{
    public class ChangeBasketCurrencyCommand : IRequest<Guid>
    {
        public string Currency {get; set;}
    }
}
