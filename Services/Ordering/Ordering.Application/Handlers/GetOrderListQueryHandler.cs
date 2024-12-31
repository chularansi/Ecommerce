using AutoMapper;
using MediatR;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using Ordering.Core.Contracts;

namespace Ordering.Application.Handlers
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrderResponse>>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public GetOrderListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public async Task<List<OrderResponse>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var orderList = await orderRepository.GetOrdersByUserName(request.UserName);
            return mapper.Map<List<OrderResponse>>(orderList);
        }
    }
}
