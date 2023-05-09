
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTracking.Application.Features.Commands;
using StockTracking.Application.Features.Queries;

namespace StockTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProducts()
        {
            ProductGetAllRequest request = new();
            ProductGetAllResponse response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            ProductCreateResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{CategoryName}")]
        public async Task<IActionResult> GetProductsByName([FromRoute] GetProductByCategoryRequest request)
        {
            ProductGetByCategoryResponse response = await _mediator.Send(request);

            return Ok(response);    
        }
    }
}
