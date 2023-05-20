using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTracking.Application.Features.Commands.Categories;
using StockTracking.Application.Features.Queries.Categories;
using StockTracking.Application.Features.Queries.StockMovementTypes;

namespace StockTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockMovementTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockMovementTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateStockMovementTypes([FromBody] CreateStockMovementTypeRequest request)
        {
            CreateStockMovementTypeResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteStockMovementTypes([FromRoute] DeleteStockMovementTypeRequest request)
        {
            DeleteStockMovementTypeResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllStockMovementTypes()
        {
            GetAllStockMovementTypesRequest request = new();
            GetAllStockMovementTypesResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        
        [HttpPut("[action]/{Id}")]
        public async Task<IActionResult> UpdateStockMovementTypes([FromBody] UpdateStockMovementTypeRequest request, [FromRoute] int Id)
        {
            request.Id = Id;
            UpdateStockMovementTypeResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetStockMovementTypeById([FromRoute] GetStockMovementTypeByIdRequest request)
        {
            GetStockMovementTypeByIdResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
