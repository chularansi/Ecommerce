using Basket.Application.Commands;
using Basket.Core.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handlers
{
    public class DeleteBasketByUserNameHandler : IRequestHandler<DeleteBasketByUserNameCommand, Unit>
    {
        private readonly IBasketRepository basketRepository;

        public DeleteBasketByUserNameHandler(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }
        public async Task<Unit> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
        {
            await this.basketRepository.DeleteBasket(request.UserName);
            return Unit.Value;
        }
    }
}
