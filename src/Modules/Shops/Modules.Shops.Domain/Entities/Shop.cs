using Modules.Shops.Domain.Events;
using Modules.Shops.Domain.ValueObjects;
using Shared.Domain;
using Shared.Domain.ValueObjects;

namespace Modules.Shops.Domain.Entities
{
    public class Shop : Entity, IAggregateRoot
    {
        public ShopId Id { get; private set; }
        public Email Email { get; private set; }
        public PasswordHash PasswordHash { get; private set; }
        public Name OwnerName { get; private set; }
        public LastName OwnerLastName { get; private set; }
        public ShopName ShopName { get; private set; }
        public Address ShopAddress { get; private set; }
        public TaxNumber TaxNumber { get; private set; }
        public TelephoneNumber ContactNumber { get; private set; }
        public Roles Role { get; private set; } = Roles.shop;

        private Shop() { }

        internal Shop(Email email,
                      PasswordHash passwordHash,
                      Name ownerName,
                      LastName ownerLastName,
                      ShopName shopName,
                      Address shopAddress,
                      TaxNumber taxNumber,
                      TelephoneNumber contactNumber)
        {
            Id = new ShopId(Guid.NewGuid());
            Email = email;
            PasswordHash = passwordHash;
            OwnerName = ownerName;
            OwnerLastName = ownerLastName;
            ShopName = shopName;
            ShopAddress = shopAddress;
            TaxNumber = taxNumber;
            ContactNumber = contactNumber;
            Role = Roles.shop;
        }

        public static Shop Create(Email email,
                                  PasswordHash passwordHash,
                                  Name ownerName,
                                  LastName ownerLastName,
                                  ShopName shopName,
                                  Address shopAddress,
                                  TaxNumber taxNumber,
                                  TelephoneNumber contactNumber)
        {
            var shop = new Shop(email,
                            passwordHash,
                            ownerName,
                            ownerLastName,
                            shopName,
                            shopAddress,
                            taxNumber,
                            contactNumber);
            shop.AddDomainEvent(new ShopCreatedDomainEvent(shop));

            return shop;
        }

        public void UpdateShopDetails(string ownerName,
                                      string ownerLastName,
                                      string shopName,
                                      string country,
                                      string city,
                                      string street,
                                      string postalCode,
                                      string taxNumber,
                                      string contactNumber)
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

            SetShopName(shopName);
            SetOwnerLastName(ownerLastName);
            SetOwnerName(ownerName);
            SetContactNumber(contactNumber);
            SetTaxNumber(taxNumber);
        }

        internal void SetAddress(Address shopAddress)
        {
            if (shopAddress is not null)
                ShopAddress = shopAddress;
        }

        internal void SetContactNumber(string contactNumber)
        {
            if (!string.IsNullOrEmpty(contactNumber))
                ContactNumber = new TelephoneNumber(contactNumber);
        }

        internal void SetTaxNumber(string taxNumber)
        {
            if (!string.IsNullOrEmpty(taxNumber))
                TaxNumber = new TaxNumber(taxNumber);
        }
        internal void SetShopName(string shopName)
        {
            if (!string.IsNullOrEmpty(shopName))
                ShopName = new ShopName(shopName);
        }

        internal void SetOwnerName(string ownerName)
        {
            if (!string.IsNullOrEmpty(ownerName))
                OwnerName = new Name(ownerName);
        }

        internal void SetOwnerLastName(string ownerLastName)
        {
            if (!string.IsNullOrEmpty(ownerLastName))
                OwnerLastName = new LastName(ownerLastName);
        }

        public string ShowShopAddress()
        {
            return ShopAddress.ToString();
        }
    }
}
