using MediatR;
using Microsoft.AspNetCore.Identity;
using StockTracking.Application.Abstractions.Token;
using StockTracking.Application.DTOS;
using StockTracking.Domain.Entities.User;

namespace StockTracking.Application.Features
{
    public class LoginUserRequest : IRequest<LoginUserResponse>
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, LoginUserResponse>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ITokenHandler _tokenHandler;

        public LoginUserHandler(SignInManager<User> signInManager, UserManager<User> userManager, ITokenHandler tokenHandler = null)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }


        public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccess(10, user.Id.ToString());
                return new ()
                {
                    AccessToken = token,
                    Message = "Giriş başarılı"
                };

            }
            return new ()
            {
                
                Message = "Kullanıcı adı veya şifre hatalı!"
            };


        }
    }
    public class LoginUserResponse
    {
        public Token AccessToken { get; set; }
        public string Message { get; set; }
    }
}
