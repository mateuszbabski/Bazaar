using System.Collections.ObjectModel;

namespace Modules.Products.Domain.ValueObjects
{
    public class Categories
    {
        private readonly List<string> _categories;
        public ReadOnlyCollection<string> CategoryList
        {
            get
            {
                return _categories.AsReadOnly();
            }
        }
        public Categories()
        {
            _categories = new List<string>
            {

            };
        }
    }
}
