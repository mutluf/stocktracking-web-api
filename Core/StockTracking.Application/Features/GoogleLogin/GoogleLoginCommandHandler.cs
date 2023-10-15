using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StockTracking.Application.Abstractions.Token;
using StockTracking.Application.DTOS;
using StockTracking.Domain.Entities.User;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace StockTracking.Application.Features.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IMapper _mapper;

        public GoogleLoginCommandHandler(UserManager<User> userManager, ITokenHandler tokenHandler, IMapper mapper)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _mapper = mapper;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {

            var settings = new ValidationSettings()
            {
                Audience = new List<string> { "102045876809-mfv4gsshjuav0mr2393o4e5ckh97o14n.apps.googleusercontent.com" }
            };

            var payload = await ValidateAsync(request.IdToken, settings);
            var info =new UserLoginInfo(request.Provider, payload.Subject, request.Provider);

            User user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            bool result = user != null;
            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);

                if(user == null)
                {
                    user = new()
                    {

                        Email = payload.Email,
                        UserName = payload.Email,
                        Name = payload.Name,
                        Surname = payload.FamilyName
                    };
                }

                //user = _mapper.Map<User>(request);
                var identityResult =  await _userManager.CreateAsync(user);
                result= identityResult.Succeeded;
            }

            if (result)            
                await _userManager.AddLoginAsync(user, info);            
            else            
                throw new Exception("Invalid external login");   
            

            Token token = _tokenHandler.CreateAccess(5, user.Id.ToString());

            return new()
            {
                Token = token,
            };
        }
    }
}
