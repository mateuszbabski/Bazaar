﻿using Modules.Customers.Domain.ValueObjects;
using Shared.Domain.ValueObjects;
using Shared.Domain;
using Modules.Customers.Domain.Events;

namespace Modules.Customers.Domain.Entities
{
    public class Customer : Entity, IAggregateRoot
    {
        public CustomerId Id { get; private set; }
        public Email Email { get; private set; }
        public PasswordHash PasswordHash { get; private set; }
        public Name Name { get; private set; }
        public LastName LastName { get; private set; }
        public Address Address { get; private set; }
        public TelephoneNumber TelephoneNumber { get; private set; }
        public Roles Role { get; private set; } = Roles.customer;

        private Customer() { }
        
        internal Customer(Email email,
                          PasswordHash passwordHash,
                          Name name,
                          LastName lastName,
                          Address address,
                          TelephoneNumber telephoneNumber)
        {
            Id = new CustomerId(Guid.NewGuid());
            Email = email;
            PasswordHash = passwordHash;
            Name = name;
            LastName = lastName;
            Address = address;
            TelephoneNumber = telephoneNumber;
            Role = Roles.customer;
        }

        public static Customer Create(Email email,
                                      PasswordHash passwordHash,
                                      Name name,
                                      LastName lastName,
                                      Address address,
                                      TelephoneNumber telephoneNumber)
        {
            var customer = new Customer(email, passwordHash, name, lastName, address, telephoneNumber);
            customer.AddDomainEvent(new CustomerCreatedDomainEvent(customer));

            return customer;
        }

        public void UpdateCustomerDetails(string name,
                                          string lastName,
                                          string country,
                                          string city,
                                          string street,
                                          string postalCode,
                                          string telephoneNumber)
        {
            var addressParams = new List<string>()
            {
                country, city, street, postalCode
            };

            if (addressParams.All(c => !string.IsNullOrEmpty(c)))
            {
                var newShopAddress = Address.CreateAddress(country, city, street, postalCode);
                SetAddress(newShopAddress);
            }

            SetName(name);
            SetLastName(lastName);
            SetTelephoneNumber(telephoneNumber);
        }

        internal void SetAddress(Address address)
        {
            if (address is not null)
                Address = address;
        }

        internal void SetName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                Name = new Name(name);
        }

        internal void SetLastName(string lastName)
        {
            if (!string.IsNullOrEmpty(lastName))
                LastName = new LastName(lastName);
        }

        internal void SetTelephoneNumber(string telephoneNumber)
        {
            if (!string.IsNullOrEmpty(telephoneNumber))
                TelephoneNumber = new TelephoneNumber(telephoneNumber);
        }

        public string ShowCustomerAddress()
        {
            return Address.ToString();
        }
    }
}
