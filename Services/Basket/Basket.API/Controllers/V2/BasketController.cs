using Asp.Versioning;
using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Quries;
using Basket.Core.Entities;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers.V2
{
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly ILogger<BasketController> logger;

        public BasketController(IMediator mediator, IPublishEndpoint publishEndpoint, ILogger<BasketController> logger)
        {
            this.mediator = mediator;
            this.publishEndpoint = publishEndpoint;
            this.logger = logger;
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckoutV2 basketCheckout)
        {
            //Get the existing basket with username
            var query = new GetBasketByUserNameQuery(basketCheckout.UserName);
            var basket = await mediator.Send(query);
            if (basket == null)
            {
                return BadRequest();
            }

            var eventMsg = BasketMapper.Mapper.Map<BasketCheckoutEventV2>(basketCheckout);
            eventMsg.TotalPrice = basket.TotalPrice;
            await publishEndpoint.Publish(eventMsg);
            logger.LogInformation($"Basket Published for {basket.UserName} with V2 endpoint");

            //remove the basket
            var deleteCmd = new DeleteBasketByUserNameCommand(basketCheckout.UserName);
            await mediator.Send(deleteCmd);
            return Accepted();
        }
    }
}
