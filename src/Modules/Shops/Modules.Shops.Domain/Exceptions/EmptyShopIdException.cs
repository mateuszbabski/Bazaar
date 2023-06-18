﻿namespace Modules.Shops.Domain.Exceptions
{
    public class EmptyShopIdException : Exception
    {
        public EmptyShopIdException() : base(message: "Shop Id cannot be empty.")
        {

        }
    }
}
