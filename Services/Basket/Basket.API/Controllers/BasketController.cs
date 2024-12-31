using Asp.Versioning;
using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Quries;
using Basket.Application.Responses;
using Basket.Core.Entities;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;

namespace Basket.API.Controllers
{
    [ApiVersion("1")]
    public class BasketController : ApiController
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

        [HttpGet]
        [Route("[action]/{userName}", Name = "GetBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
        {
            var query = new GetBasketByUserNameQuery(userName);
            var basket = await mediator.Send(query);
            return Ok(basket);
        }

        [HttpPost("CreateBasket")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> UpdateBasket([FromBody] CreateShoppingCartCommand createShoppingCartCommand)
        {
            var basket = await mediator.Send(createShoppingCartCommand);
            return Ok(basket);
        }

        [HttpDelete]
        [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasket(string userName)
        {
            var cmd = new DeleteBasketByUserNameCommand(userName);
            return Ok(await mediator.Send(cmd));
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            //Get the existing basket with username
            var query = new GetBasketByUserNameQuery(basketCheckout.UserName);
            var basket = await mediator.Send(query);
            if (basket == null)
            {
                return BadRequest();
            }

            var eventMsg = BasketMapper.Mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMsg.TotalPrice = basket.TotalPrice;
            await publishEndpoint.Publish(eventMsg);
            logger.LogInformation($"Basket Published for {basket.UserName}");

            //remove the basket
            var deleteCmd = new DeleteBasketByUserNameCommand(basketCheckout.UserName);
            await mediator.Send(deleteCmd);
            return Accepted();
        }
    }
}
