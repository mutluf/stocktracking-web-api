using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StockTracking.Domain.Entities.User;

namespace StockTracking.Application.Features
{
    public class CreateUserRequest:IRequest<CreateUserResponse>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public CreateUserHandler(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request);
            IdentityResult result = await _userManager.CreateAsync(user);

            List<string> errors = new();

            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }

            if (result.Succeeded)
            {
                return new()
                {
                    Message = "Kayıt başarıyla oluşturuldu."
                };
            }
            else
            {
                return new()
                {
                    Errors = errors
                };
            }
        }
    }
    public class CreateUserResponse
    {
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
