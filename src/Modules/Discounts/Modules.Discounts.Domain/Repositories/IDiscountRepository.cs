﻿using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.ValueObjects;

namespace Modules.Discounts.Domain.Repositories
{
    public interface IDiscountRepository
    {
        Task<Discount> Add(Discount discount);
        Task<Discount> GetDiscountById(Guid id);
        Task<IEnumerable<Discount>> GetAllCreatorDiscounts(Guid creatorId);
        Task<IEnumerable<Discount>> GetDiscountsByType(DiscountType discountType, Guid? discountTargetId);
        Task<IEnumerable<Discount>> GetAll();
        void Delete(Discount discount);
    }
}
