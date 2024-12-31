using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    public class CatalogController : ApiController
    {
        private readonly IMediator mediator;
        private readonly ILogger<CatalogController> logger;

        public CatalogController(IMediator mediator, ILogger<CatalogController> logger) 
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductResponse>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{productName}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductResponse>>> GetProductByName(string productName)
        {
            var query = new GetProductByNameQuery(productName);
            var result = await mediator.Send(query);
            logger.LogInformation($"Product with {productName} fetched");
            return Ok(result);
        }

        [HttpGet]
        [Route("GetProducts")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Pagination<ProductResponse>>> GetProducts([FromQuery] CatalogSpecificationsParams catalogSpecsParams)
        {
            var query = new GetProductsQuery(catalogSpecsParams);
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetBrands")]
        [ProducesResponseType(typeof(IList<BrandResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<BrandResponse>>> GetBrands()
        {
            var query = new GetBrandsQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetTypes")]
        [ProducesResponseType(typeof(IList<TypeResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<TypeResponse>>> GetTypes()
        {
            var query = new GetTypesQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/{brand}", Name = "GetProductsByBrandName")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<ProductResponse>>> GetProductsByBrandName(string brand)
        {
            var query = new GetProductByBrandQuery(brand);
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] CreateProductCommand productCommand)
        {
            var result = await mediator.Send(productCommand);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponse>> UpdateProduct([FromBody] UpdateProductCommand productCommand)
        {
            var result = await mediator.Send(productCommand);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponse>> DeleteProduct(string id)
        {
            var command = new DeleteProductCommand(id);
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
