using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Core.Contracts;
using Ordering.Core.Entities;

namespace Ordering.Application.Handlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IOrderRepository orderRepository;
        private readonly ILogger<DeleteOrderCommandHandler> logger;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<DeleteOrderCommandHandler> logger)
        {
            this.orderRepository = orderRepository;
            this.logger = logger;
        }
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = await orderRepository.GetByIdAsync(request.Id);
            if (orderToDelete == null)
            {
                throw new OrderNotFoundException(nameof(Order), request.Id);
            }

            await orderRepository.DeleteAsync(orderToDelete);
            logger.LogInformation($"Order with {request.Id} is deleted successfully.");
            return Unit.Value;
        }
    }
}
