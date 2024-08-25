using Modules.Orders.Domain.ValueObjects;
using Shared.Domain;
using Shared.Domain.ValueObjects;

namespace Modules.Orders.Domain.Entities
{
    public class Receiver : Entity
    {
        public ReceiverId Id { get; private set; }
        public Email Email { get; private set; }
        public Name Name { get; private set; }
        public LastName LastName { get; private set; }
        public Address Address { get; private set; }
        public TelephoneNumber TelephoneNumber { get; private set; }

        private Receiver() { }
        internal Receiver(Guid id,
                          Email email,
                          Name name,
                          LastName lastName,
                          Address address,
                          TelephoneNumber telephoneNumber)
        {
            Id = new ReceiverId(id);
            Email = email;
            Name = name;
            LastName = lastName;
            Address = address;
            TelephoneNumber = telephoneNumber;
        }

        public static Receiver Create(Guid id,
                                      Email email,
                                      Name name,
                                      LastName lastName,
                                      Address address,
                                      TelephoneNumber telephoneNumber)
        {
            return new Receiver(id, email, name, lastName, address, telephoneNumber);
        }
    }
}
