using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Responses;
using Ordering.Core.Contracts;
using Ordering.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CheckoutOrderCommandHandler> logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<CheckoutOrderCommandHandler> logger)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = mapper.Map<Order>(request);
            var generatedOrder = await orderRepository.AddAsync(orderEntity);
            logger.LogInformation($"Order with Id {generatedOrder.Id} successfully created");
            return generatedOrder.Id;
        }
    }
}
