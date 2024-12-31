using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Core.Contracts;
using Ordering.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    public class CheckoutOrderCommandV2Handler : IRequestHandler<CheckoutOrderCommandV2, int>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CheckoutOrderCommandHandler> logger;

        public CheckoutOrderCommandV2Handler(IOrderRepository orderRepository, IMapper mapper, ILogger<CheckoutOrderCommandHandler> logger)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<int> Handle(CheckoutOrderCommandV2 request, CancellationToken cancellationToken)
        {
            var orderEntity = mapper.Map<Order>(request);
            var generatedOrder = await orderRepository.AddAsync(orderEntity);
            logger.LogInformation($"Order with Id {generatedOrder.Id} successfully created with V2 Handler");
            return generatedOrder.Id;
        }
    }
}
