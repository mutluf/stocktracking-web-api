using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockTracking.Application.Features.Commands.Suppliers;
using StockTracking.Application.Features.Queries.Suppliers;

namespace StockTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SuppliersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierRequest request)
        {
            CreateSupplierResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteSupplier([FromRoute] DeleteSupplierRequest request)
        {
            DeleteSupplierResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllSuppliers()
        {
            GetAllSuppliersRequest request = new();
            GetAllSuppliersResponse response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPut("[action]/{Id}")]
        public async Task<IActionResult> UpdateSupplier([FromBody] UpdateSupplierRequest request, [FromRoute] int Id)
        {
            request.Id = Id;
            UpdateSupplierResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}

