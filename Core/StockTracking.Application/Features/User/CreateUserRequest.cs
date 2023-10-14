using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StockTracking.Application.Background;
using StockTracking.Domain.Entities.User;

namespace StockTracking.Application.Features
{
    public class CreateUserRequest:IRequest<CreateUserResponse>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }

    }
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserBackgroundJob _backgroundJob;

        public CreateUserHandler(IMapper mapper, UserManager<User> userManager, IUserBackgroundJob backgroundJob)
        {
            _mapper = mapper;
            _userManager = userManager;
            _backgroundJob = backgroundJob;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request);
            IdentityResult result = await _userManager.CreateAsync(user,request.Password);

            List<string> errors = new();

            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }

            if (result.Succeeded)
            {
                _backgroundJob.AddEnque(() => Console.WriteLine("Kullanıcıya hoş geldin maili gönderildi."));

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
