using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Application.Services;
using Basket.Core.Contracts;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Handlers
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository basketRepository;
        private readonly DiscountGrpcService discountGrpcService;

        public CreateShoppingCartCommandHandler(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
        {
            this.basketRepository = basketRepository;
            this.discountGrpcService = discountGrpcService;
        }
        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            // Integrate Discount Service
            foreach (var item in request.Items)
            {
                var coupon = await discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            var shoppingCart = await this.basketRepository.UpdateBasket(new ShoppingCart
            {
                UserName = request.UserName,
                Items = request.Items
            });

            var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
            return shoppingCartResponse;
        }
    }
}
