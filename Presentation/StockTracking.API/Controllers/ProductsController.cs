
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTracking.Application.Features.Commands.Products;
using StockTracking.Application.Features.Queries.Categories;
using StockTracking.Application.Features.Queries.Products;

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
            GetAllProductsRequest request = new();
            GetAllProductsResponse response = await _mediator.Send(request);

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
            GetProductByCategoryResponse response = await _mediator.Send(request);

            return Ok(response);    
        }


        [HttpPost("[action]/{Id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request, [FromRoute] int Id)
        {
            request.Id = Id;
            UpdateProductResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
