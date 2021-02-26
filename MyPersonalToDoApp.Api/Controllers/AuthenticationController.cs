using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyPersonalToDoApp.DataModel.DTOs;
using MyPersonalToDoApp.DataModel.Identity;
using System;
using MyPersonalToDoApp.Api.Helpers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyPersonalToDoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return BadRequest();
            }

            var appUser = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToGuidString()
            };

            IdentityResult creationResult = await _userManager.CreateAsync(appUser, model.Password);
            
            if (!creationResult.Succeeded)
            {
                return BadRequest();
            }

            return Ok(new UserCreatedDTO(appUser.Id));
        }

        //[HttpPost]
        //public IActionResult Login()
        //{
        //    return Ok();
        //}
    }
}
