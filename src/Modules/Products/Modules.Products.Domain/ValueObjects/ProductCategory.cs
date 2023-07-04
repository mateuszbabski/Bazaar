using Modules.Products.Domain.Exceptions;
using Modules.Products.Domain.Rules;

namespace Modules.Products.Domain.ValueObjects
{
    public class ProductCategory
    {
        public string CategoryName { get;}

        internal ProductCategory(string categoryName)
        {
            CategoryName = categoryName;
        }

        public static ProductCategory Create(string categoryName)
        {
            if (new SystemAcceptsProductCategoryRule(categoryName).IsBroken() || string.IsNullOrEmpty(categoryName))
            {
                throw new InvalidProductCategoryException();
            }

            return new ProductCategory(categoryName);
        }
    }
}
