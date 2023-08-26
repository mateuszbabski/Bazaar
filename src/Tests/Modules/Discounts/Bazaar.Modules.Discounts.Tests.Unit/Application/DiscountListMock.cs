﻿using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.ValueObjects;
using Shared.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazaar.Modules.Discounts.Tests.Unit.Application
{
    public class DiscountListMock
    {
        private readonly List<Discount> _discountList;
        public List<Discount> Discounts
        {
            get { return _discountList; }
        }

        public DiscountListMock(Guid shopId)
        {
            var discountTarget = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToVendors, shopId);

            var discount1 = Discount.CreateValueDiscount(Guid.NewGuid(), 10, "USD", discountTarget);
            var discount2 = Discount.CreatePercentageDiscount(Guid.NewGuid(), 10, discountTarget);

            _discountList = new List<Discount>
            {
                discount1,
                discount2,
            };
        }
    }
}
