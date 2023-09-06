//using Modules.Baskets.Domain.Repositories;
//using Shared.Abstractions.Events;
//using Shared.Application.IntegrationEvents;

//namespace Modules.Baskets.Application.Events.EventHandlers
//{
//    public class ProductPriceChangedEventHandler : IEventHandler<ProductPriceChangedEvent>
//    { // TODO: check active baskets and change price of product
//        private readonly IBasketRepository _basketRepository;

//        public ProductPriceChangedEventHandler(IBasketRepository basketRepository)
//        {
//            _basketRepository = basketRepository;
//        }

//        public Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
//        {            
//            throw new NotImplementedException();
//        }
//    }
//}
