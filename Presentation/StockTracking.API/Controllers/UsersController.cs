using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockTracking.Application.Features;
using StockTracking.Application.Features.GoogleLogin;
using StockTracking.Infrastructure.MessageBus;
using System.Net;
using static MassTransit.Monitoring.Performance.BuiltInCounters;

namespace StockTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMessageBus _messageBus;
        public UsersController(IMediator mediator, IMessageBus messageBus)
        {
            _mediator = mediator;
            _messageBus = messageBus;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            CreateUserResponse response = await _mediator.Send(request);
            if (response.Errors == null)
            {
                _messageBus.PublishMessage(response.Message, "hadi-rabbit");
            }
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserRequest loginUserCommandRequest)
        {
            LoginUserResponse response = await _mediator.Send(loginUserCommandRequest);
            if (response.Message == "Giriş başarılı")
            {
                Response.Cookies.Append(
                    "access_token",
             response.AccessToken.AccesssToken,
            new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
                IsEssential = true,
                SameSite = SameSiteMode.None,
                Secure = true,
            });


                using (RabbitMQMessageBus messageBus = new RabbitMQMessageBus())
                {
                    messageBus.PublishMessage(response.Message, "hadi-rabbit");
                }

                //_messageBus.PublishMessage(response.Message,"hadi-rabbit");
                //(_messageBus as RabbitMQMessageBus)?.Dispose();



                return Ok(response);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.NotAcceptable, response);
            }
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginCommandRequest request)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(request);

            return Ok(response);
        }

    }
}

