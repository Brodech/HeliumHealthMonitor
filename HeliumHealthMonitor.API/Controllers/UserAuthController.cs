using HeliumHealthMonitor.BusinessLogic.Authentication;
using HeliumHealthMonitor.Data.MongoDBLayer.DataAccess;
using HeliumHealthMonitor.Data.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HeliumHealthMonitor.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthentication _authentication;
        private readonly IUserDataAccess _userDataAccess;

        public UserAuthController(IConfiguration config,
                                   IUserDataAccess userDataAccess,
                                   IAuthentication authentication)
        {
            _config = config;
            _authentication = authentication;
            _userDataAccess = userDataAccess;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<UserLoginResult> Login([FromBody] UserLoginFormModel userLogin)
        {
            try
            {
                var user = await _authentication.AuthenticateUser(userLogin);

                if (user != null)
                {
                    return new UserLoginResult()
                    {
                        Message = "Login successful.",
                        Id = user.Id,
                        Role = user.Role,
                        Username = user.Username,
                        JwtBearer = Generate(user),
                        Success = true
                    };
                }

                return new UserLoginResult() { Message = "User/password not found.", Success = false };
            }
            catch (Exception ex)
            {
                return new UserLoginResult() { Message = "Login has failed.", Success = false };
            }

        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<UserLoginResult> Register([FromBody] UserRegistrationFormModel userRegistration)
        {
            if(userRegistration.Password != userRegistration.Password2)
            {
                return new UserLoginResult() { Message = "Password and confirm password do not match.", Success = false };
            }
            try 
            {
                await _authentication.RegisterUser(userRegistration);
            }
            catch(Exception ex)
            {
                return new UserLoginResult() { Message = "User registration has failed.", Success = false };
                }
            return new UserLoginResult() { Message = "Registration successful.", Success = true };
        }

        private string Generate(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
