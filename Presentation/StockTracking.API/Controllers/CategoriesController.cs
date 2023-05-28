using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockTracking.Application.Features.Commands.Categories;
using StockTracking.Application.Features.Queries.Categories;

namespace StockTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            CreateCategoryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] DeleteCategoryRequest request)
        {
            DeleteCategoryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCategories()
        {
            GetAllCategoriesRequest request = new();
            GetAllCategoriesResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("[action]/{Id}")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequest request, [FromRoute] int Id)
        {          
            request.Id= Id;
            UpdateCategoryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
