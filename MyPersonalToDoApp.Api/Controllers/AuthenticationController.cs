using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyPersonalToDoApp.DataModel.DTOs;
using MyPersonalToDoApp.DataModel.Identity;
using System;
using MyPersonalToDoApp.Api.Helpers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MyPersonalToDoApp.Data.Contracts;
using MyPersonalToDoApp.DataModel.Entities;

namespace MyPersonalToDoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IRegisterRepository _registerRepository;

        public AuthenticationController(IConfiguration configuration, IRegisterRepository registerRepository, UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
            this._configuration = configuration;
            this._registerRepository = registerRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO model)
        {
            var appUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToGuidString()
            };

            var customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            bool created = await _registerRepository.RegisterCustomer(appUser, customer, model.Password);

            if (!created)
            {
                return BadRequest();
            }

            return Ok(new UserCreatedDTO(appUser.Id));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            ApplicationUser appUser = await _userManager.FindByEmailAsync(model.Email);

            if (appUser == null)
            {
                return Unauthorized();
            }

            bool isValidPassword = await _userManager.CheckPasswordAsync(appUser, model.Password);

            if (!isValidPassword)
            {
                return Unauthorized();
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, appUser.Email),
                new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToGuidString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthJwt:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthJwt:ValidIssuer"],
                audience: _configuration["AuthJwt:ValidAudience"],
                expires: DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("AuthJwt:ExpirationInMinute")),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return Ok(new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                Email = appUser.Email
            }); ;
        }
    }
}
