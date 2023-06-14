//namespace Shared.Infrastructure.Auth
//{
//    internal sealed class EmailUniquenessChecker : IEmailUniquenessChecker
//    {
//        private readonly ICustomerRepository _customerRepository;
//        private readonly IShopRepository _shopRepository;

//        public EmailUniquenessChecker(ICustomerRepository customerRepository,
//                                      IShopRepository shopRepository)
//        {
//            _customerRepository = customerRepository;
//            _shopRepository = shopRepository;

//        }

//        public async Task<bool> IsEmailUnique(string email)
//        {
//            var customer = await _customerRepository.GetCustomerByEmail(email);
//            var shop = await _shopRepository.GetShopByEmail(email);

//            if (customer != null || shop != null)
//                throw new BadRequestException("Email cannot be used");
//            return true;
//        }
//    }
//}
