using AutoMapper;
using MagicVilla_Infrastructure.BusinessObjects;
using MagicVilla_Infrastructure.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LocalUserBO = MagicVilla_Infrastructure.BusinessObjects.LocalUser;
using LocalUserEO = MagicVilla_Infrastructure.Entities.LocalUser;

namespace MagicVilla_Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        private readonly string _secretKey;

        public UserService(IApplicationUnitOfWork applicationUnitOfWork, 
            IMapper mapper, IConfiguration configuration)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
            _secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public async Task<bool> IsUniqueUser(string username)
        {
            var userCount = await _applicationUnitOfWork.Users.GetCount(x => x.UserName.Equals(username));

            if (userCount > 0)
                return false;

            return true;
        }

        public async Task<LoginResponse> Login(Login loginRequest)
        {
            var user = await _applicationUnitOfWork.Users.GetUserDetails(loginRequest.UserName, loginRequest.Password);

            if (user == null)
            {
                return new LoginResponse()
                {
                    Token = "",
                    User = null
                };
            }
                

            //If user was found geenrate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userBO = _mapper.Map<LocalUserBO>(user);

            var loginResponse = new LoginResponse()
            {
                Token = tokenHandler.WriteToken(token),
                User = userBO
            };

            return loginResponse;
        }

        public async Task<LocalUserBO> Register(Registration registrationRequest)
        {
            var user = _mapper.Map<LocalUserEO>(registrationRequest);

            await _applicationUnitOfWork.Users.Add(user);
            _applicationUnitOfWork.Save();
            user.Password = "";

            return _mapper.Map<LocalUserBO>(user);
        }
    }
}