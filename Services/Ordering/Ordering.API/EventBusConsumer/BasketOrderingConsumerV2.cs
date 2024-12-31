using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Commands;

namespace Ordering.API.EventBusConsumer
{
    public class BasketOrderingConsumerV2 : IConsumer<BasketCheckoutEventV2>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly ILogger<BasketOrderingConsumerV2> logger;

        public BasketOrderingConsumerV2(IMediator mediator, IMapper mapper, ILogger<BasketOrderingConsumerV2> logger)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task Consume(ConsumeContext<BasketCheckoutEventV2> context)
        {
            using var scope = logger.BeginScope("Consuming Basket Checkout Event for {correlationId} with version 2", context.Message.CorrelationId);
            var cmd = mapper.Map<CheckoutOrderCommandV2>(context.Message);
            var result = await mediator.Send(cmd);
            logger.LogInformation("Basket Checkout Event completed with version 2!!!");
        }
    }
}
