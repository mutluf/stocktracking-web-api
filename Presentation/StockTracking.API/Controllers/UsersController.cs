using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTracking.Application.Features;
using StockTracking.Application.Features.Commands;

namespace StockTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        readonly IConfiguration _configuration;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            CreateUserResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
