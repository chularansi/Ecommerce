using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Commands;

namespace Ordering.API.EventBusConsumer
{
    public class BasketOrderingConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly ILogger<BasketOrderingConsumer> logger;

        public BasketOrderingConsumer(IMediator mediator, IMapper mapper, ILogger<BasketOrderingConsumer> logger)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            using var scope = logger.BeginScope("Consuming Basket Checkout Event for {correlationId}", context.Message.CorrelationId);
            var cmd = mapper.Map<CheckoutOrderCommand>(context.Message);
            var result = await mediator.Send(cmd);
            logger.LogInformation("Basket Checkout Event completed!!!");
        }
    }
}
