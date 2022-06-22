using Microsoft.AspNetCore.Mvc;
using RefactorThis.Core.Common.ViewModels;
using RefactorThis.Core.ProductOptions.Queries;
using RefactorThis.Core.Products.Commands.Create;
using RefactorThis.Core.Products.Commands.Update;
using RefactorThis.Core.Products.Queries;

namespace RefactorThis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ApiController
    {
        #region Product

        #region POST method
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> Create(CreateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        #endregion

        #region PUT method
        [HttpPut("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> Update(UpdateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        #endregion

        #region GET method
        [HttpDelete("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> Delete(DeleteProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductVm>> GetProduct(Guid id)
        {
            return Ok(await Mediator.Send(new GetProductOptionsQuery { ProductId = id }));
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAlProducts()
        {
            return Ok(await Mediator.Send(new GetProductsListQuery()));
        }

        [HttpGet]
        [Route("SearchByName/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductVm>> GetProducts(string name)
        {
            return Ok(await Mediator.Send(new GetProductsByNameQuery { Name = name }));
        }
        #endregion

        #endregion

        #region ProductOptions
  
        #endregion
    }
}
