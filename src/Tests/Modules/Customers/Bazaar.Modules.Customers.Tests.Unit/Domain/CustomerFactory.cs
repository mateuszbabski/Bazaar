using Modules.Customers.Domain.Entities;
using Shared.Domain.ValueObjects;

namespace Bazaar.Modules.Customers.Tests.Unit.Domain
{
    public class CustomerFactory
    {
        public static Customer GetCustomer()
        {
            var address = Address.CreateAddress("country", "city", "street", "postalCode");

            var customer = Customer.Create("customer@example.com",
                                           "passwordHash",
                                           "name",
                                           "lastName",
                                           address,
                                           "telephoneNumber");

            return customer;
        }
    }
}
