using Modules.Baskets.Contracts.Events.BasketCheckedOut;
using Modules.Customers.Contracts;
using Modules.Discounts.Contracts.Interfaces;
using Modules.Discounts.Domain.Entities;
using Modules.Orders.Application.Contracts;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Shippings.Contracts;
using Modules.Shippings.Domain.Entities;
using Modules.Shippings.Domain.ValueObjects;
using Shared.Abstractions.Events;
using Shared.Application.Exceptions;

namespace Modules.Orders.Application.Events.EventHandlers
{
    public class BasketCheckedOutEventHandler : IEventHandler<BasketCheckedOutEvent>
    {
        private readonly IDiscountChecker _discountChecker;
        private readonly IDiscountCouponChecker _discountCouponChecker;
        private readonly IShippingMethodChecker _shippingMethodChecker;
        private readonly ICustomerChecker _customerChecker;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrdersUnitOfWork _unitOfWork;

        public BasketCheckedOutEventHandler(IDiscountChecker discountChecker, 
                                            IDiscountCouponChecker discountCouponChecker,
                                            IShippingMethodChecker shippingMethodChecker,
                                            ICustomerChecker customerChecker,
                                            IOrderRepository orderRepository,
                                            IOrdersUnitOfWork unitOfWork)
        {
            _discountChecker = discountChecker;
            _discountCouponChecker = discountCouponChecker;
            _shippingMethodChecker = shippingMethodChecker;
            _customerChecker = customerChecker;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(BasketCheckedOutEvent notification, CancellationToken cancellationToken)
        {
            var shippingMethod = await _shippingMethodChecker.GetShippingMethodByItsName(notification.Message.ShippingMethod);
            if (shippingMethod == null || !shippingMethod.IsAvailable)
            {
                throw new NotFoundException(notification.Message.ShippingMethod);
            }

            var newOrderId = Guid.NewGuid();

            var discountCoupon = await _discountCouponChecker.GetDiscountCouponByCodeToProcess(notification.Message.CouponCode);

            if (discountCoupon == null)
            {
                await ProcessAndCreateOrderWithoutDiscount(newOrderId, notification.Message, shippingMethod);
                return;
            }

            var discount = await _discountChecker.GetDiscountByIdToProcess(discountCoupon.DiscountId);

            var newOrder = await ProcessDiscount(newOrderId, discount, notification.Message, shippingMethod);
            
            await _orderRepository.Add(newOrder);

            await _unitOfWork.CommitAndDispatchDomainEventsAsync(newOrder);

            // mock paymentmethod until module is done
        }

        private async Task<Order> ProcessAndCreateOrderWithoutDiscount(Guid newOrderId, BasketCheckoutMessage message, ShippingMethod shippingMethod)
        {
            var newOrder = 
                Order.CreateOrder(
                    newOrderId,
                    await CreateReceiverFromBasket(message.CustomerId), 
                    CreateOrderItemsFromBasketItems(message.BasketItems, newOrderId), 
                    CreateOrderShippingMethodFromBasketShippingMethod(shippingMethod),
                    message.Weight
                    );

            await _orderRepository.Add(newOrder);

            await _unitOfWork.CommitAndDispatchDomainEventsAsync(newOrder);

            return newOrder;
        }

        private async Task<Order> ProcessDiscount(Guid newOrderId, Discount discount, BasketCheckoutMessage message, ShippingMethod shippingMethod)
        {        
            if (discount == null)
            {
                return 
                    Order.CreateOrder(
                        newOrderId,
                        await CreateReceiverFromBasket(message.CustomerId), 
                        CreateOrderItemsFromBasketItems(message.BasketItems, newOrderId), 
                        CreateOrderShippingMethodFromBasketShippingMethod(shippingMethod),
                        message.Weight
                        );
            }

            switch (discount.DiscountTarget.DiscountType.ToString())
            {
                case "AssignedToOrderTotal":
                    return await ApplyDiscountToOrder(newOrderId, discount, message, shippingMethod);                    
                case "AssignedToProduct":
                    return await ApplyDiscountToProduct(newOrderId, discount, message, shippingMethod);                    
                case "AssignedToVendors":
                    return await ApplyDiscountToVendors(newOrderId, discount, message, shippingMethod);                    
                case "AssignedToShipping":
                    return await ApplyDiscountToShipping(newOrderId, discount, message, shippingMethod);                    
                case "AssignedToAllProducts":
                    return await ApplyDiscountToAllProducts(newOrderId, discount, message, shippingMethod);                    
                case "AssignedToCustomer":
                    return await ApplyDiscountToCustomer(newOrderId, discount, message, shippingMethod);
                default:
                    return 
                        Order.CreateOrder(
                            newOrderId,
                            await CreateReceiverFromBasket(message.CustomerId), 
                            CreateOrderItemsFromBasketItems(message.BasketItems, newOrderId), 
                            CreateOrderShippingMethodFromBasketShippingMethod(shippingMethod),
                            message.Weight
                            );
            }
        }

        private async Task<Order> ApplyDiscountToCustomer(Guid newOrderId, Discount discount, BasketCheckoutMessage message, ShippingMethod shippingMethod)
        {
            throw new NotImplementedException();
        }

        private async Task<Order> ApplyDiscountToAllProducts(Guid newOrderId, Discount discount, BasketCheckoutMessage message, ShippingMethod shippingMethod)
        {
            throw new NotImplementedException();
        }

        private async Task<Order> ApplyDiscountToShipping(Guid newOrderId, Discount discount, BasketCheckoutMessage message, ShippingMethod shippingMethod)
        {
            throw new NotImplementedException();
        }

        private async Task<Order> ApplyDiscountToVendors(Guid newOrderId, Discount discount, BasketCheckoutMessage message, ShippingMethod shippingMethod)
        {
            throw new NotImplementedException();
        }

        private async Task<Order> ApplyDiscountToProduct(Guid newOrderId, Discount discount, BasketCheckoutMessage message, ShippingMethod shippingMethod)
        {
            throw new NotImplementedException();
        }

        private async Task<Order> ApplyDiscountToOrder(Guid newOrderId, Discount discount, BasketCheckoutMessage message, ShippingMethod shippingMethod)
        {
            throw new NotImplementedException();
        }

        private async Task<Receiver> CreateReceiverFromBasket(Guid customerId) 
        {            
            var customer = await _customerChecker.GetCustomerByIdToProcess(customerId);
            if (customer == null)
            {
                throw new NotFoundException(customerId.ToString());
            }

            var receiver = 
                Receiver.Create(
                    customer.Id, 
                    customer.Email, 
                    customer.Name, 
                    customer.LastName, 
                    customer.Address, 
                    customer.TelephoneNumber
                    );

            return receiver;
        }

        private List<OrderItem> CreateOrderItemsFromBasketItems(List<BasketItemMapped> basketItems, Guid orderId)
        {
            var itemList = new List<OrderItem>();

            foreach (var basketItem in basketItems)
            {
                var mappedOrderItem = OrderItem.CreateOrderItemFromProduct(
                    basketItem.ProductId, 
                    basketItem.ShopId,
                    orderId,
                    basketItem.Quantity, 
                    basketItem.Price
                    );

                itemList.Add(mappedOrderItem);
            }

            return itemList;
        }

        private OrderShippingMethod CreateOrderShippingMethodFromBasketShippingMethod(ShippingMethod shippingMethod)
        {
            var orderShippingMethod = 
                OrderShippingMethod.CreateNewShippingMethod(
                    shippingMethod.Id,
                    shippingMethod.Name, 
                    shippingMethod.BasePrice.Amount, 
                    shippingMethod.BasePrice.Currency
                    );

            return orderShippingMethod;
        }
    }
}
