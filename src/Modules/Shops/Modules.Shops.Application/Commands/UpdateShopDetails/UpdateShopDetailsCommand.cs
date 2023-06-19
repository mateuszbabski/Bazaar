using MediatR;

namespace Modules.Shops.Application.Commands.UpdateShopDetails
{
    public class UpdateShopDetailsCommand : IRequest
    {
        public Guid Id { get; set; }
        public string OwnerName { get; set; }
        public string OwnerLastName { get; set; }
        public string ShopName { get; set; }
        public string TaxNumber { get; set; }
        public string ContactNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}
