using DiscordClone.Api.DataModels;
using DiscordClone.Api.Entities;
using DiscordClone.Api.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Nodes;

namespace DiscordClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : GenericController<User, Guid>
    {
        private readonly IUserService _UserService;
        public UserController(IUserService UserService) : base(UserService)
        {
            _UserService = UserService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<JWT>> Login([FromBody] JsonObject obj)
        {
            //TODO: kig på type af Paraneter og derefter se på hvordan data skal håndter 
            if (!obj.TryGetPropertyValue("Email", out var Email) || !obj.TryGetPropertyValue("Password", out var Password))
            {
                return BadRequest("entity error");
            }

            var Token = await _UserService.Login((string)Email, (string)Password);

            if (Token is null)
            {
                return NotFound("Account not found");
            }

            return Ok(Token);
        }


        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel Register)
        {
            return Ok(await _UserService.Register(Register));
        }

        [HttpGet("activate/{token}")]
        public async Task<ActionResult> Activate(Guid token)
        {
            return Ok(await _UserService.Activate(token));
        }
    }
}
/* test data
{
"Displayname" : "ds5",
"Email":"ds5@gmail.com",
"Password":"Pa$$w0rd",
"PhoneNumber":""
}
 */