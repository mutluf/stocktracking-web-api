using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTracking.Application.Features.Commands.Categories;
using StockTracking.Application.Features.Commands.Depots;
using StockTracking.Application.Features.Queries.Categories;
using StockTracking.Application.Features.Queries.Depots;

namespace StockTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepotsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepotsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateDepot([FromBody] CreateDepotRequest request)
        {
            CreateDepotResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteDepot([FromRoute] DeleteDepotRequest request)
        {
            DeleteDepotResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllDepots()
        {
            GetAllDepotsRequest request = new();
            GetAllDepotsResponse response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPut("[action]/{Id}")]
        public async Task<IActionResult> UpdateDepot([FromBody] UpdateDepotRequest request, [FromRoute] int Id)
        {
            request.Id = Id;
            UpdateDepotResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
