﻿using Modules.Customers.Domain.Entities;
using Modules.Customers.Domain.Events;
using Shared.Domain;
using Shared.Domain.Exceptions;
using Shared.Domain.ValueObjects;

namespace Bazaar.Modules.Customers.Tests.Unit.Domain
{
    public class CustomerDomainTest
    {
        [Fact]
        public void CreateCustomer_ReturnsCustomerIfParamsAreValid()
        {
            var address = Address.CreateAddress("country", "city", "street", "postalCode");

            var customer = Customer.Create("customer@example.com",
                                           "passwordHash",
                                           "name",
                                           "lastName",
                                           address,
                                           "telephoneNumber");

            Assert.NotNull(customer);
            Assert.IsType<Customer>(customer);
            Assert.Equal("customer@example.com", customer.Email);
        }

        [Fact]
        public void CreateCustomer_ThrowsInvalidEmailExceptionWhenEmailIsInvalid()
        {
            var address = Address.CreateAddress("country", "city", "street", "postalCode");

            var act = Assert.Throws<InvalidEmailException>(() => Customer.Create("",
                                           "passwordHash",
                                           "name",
                                           "lastName",
                                           address,
                                           "telephoneNumber"));

            Assert.IsType<InvalidEmailException>(act);
        }

        [Fact]
        public void UpdateCustomerDetails_ReturnsUpdatedCustomerDetailsIfParamsAreValid()
        {
            var customer = GetCustomer();

            customer.UpdateCustomerDetails("updatedName",
                                           null,
                                           null,
                                           null,
                                           null,
                                           null,
                                           null);

            Assert.NotNull(customer);
            Assert.IsType<Customer>(customer);
            Assert.Equal("updatedName", customer.Name);
        }

        [Fact]
        public void UpdateCustomerDetails_ReturnsOriginalCustomerDataIfParamsAreNull()
        {
            var customer = GetCustomer();

            customer.UpdateCustomerDetails(null,
                                           null,
                                           null,
                                           null,
                                           null,
                                           null,
                                           null);

            Assert.NotNull(customer);
            Assert.IsType<Customer>(customer);
            Assert.Equal("name", customer.Name);
        }

        private static Customer GetCustomer()
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
