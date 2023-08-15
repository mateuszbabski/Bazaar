using MediatR;

namespace Modules.Shippings.Application.Commands.ShippingMethods.AddShippingMethod
{
    public class AddShippingMethodCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "PLN";
        public int DurationInDays { get; set; } = 1;
    }
}
