using MediatR;

namespace Modules.Shippings.Application.Commands.ShippingMethods.UpdateShippingMethodDetails
{
    public class UpdateShippingMethodDetailsCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int DurationTime { get; set; }
    }
}
