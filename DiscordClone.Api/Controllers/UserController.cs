using DiscordClone.Api.Entities;
using DiscordClone.Api.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public override async Task<ActionResult> Create([FromBody] User Value)
        {
            return Ok(await _UserService.Create(Value));
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Create(User user, SecurityCredentials creds)
        {
          
            //_JsonObjectHandler.CheckJsonObject(obj);
            return Ok(/*await _UserService.Create(obj)*/);
        }
    }
}
/* test data
{
"id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
"Displayname" : "ds5",
"image":{
"bytes": "",
"fileExtension": "",
"size": 0
},
"Email":"ds5@gmail.com",
"Password":"Pa$$w0rd",
"PhoneNumber":""
}
 */