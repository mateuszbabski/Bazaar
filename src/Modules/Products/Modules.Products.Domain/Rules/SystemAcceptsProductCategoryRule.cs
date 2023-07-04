using Modules.Products.Domain.ValueObjects;
using Shared.Domain;

namespace Modules.Products.Domain.Rules
{
    public class SystemAcceptsProductCategoryRule : Categories, IBusinessRule
    {
        private readonly string _categoryName;
        public SystemAcceptsProductCategoryRule(string categoryName)
        {
            _categoryName = categoryName;
        }
        public string Message => "Category is not accepted by the system";
        public bool IsBroken() => !CategoryList.Contains(_categoryName, StringComparer.OrdinalIgnoreCase);
    }
}
