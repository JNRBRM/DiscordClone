using DiscordClone.Api.Entities;
using DiscordClone.Api.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace DiscordClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : GenericController<User,Guid>
    {
        private readonly IUserService _UserService;
        private readonly JsonObjectHandler _JsonObjectHandler;
        public UserController(IGenericService<User,Guid> GenericService, IUserService UserService) : base(GenericService)
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

        [HttpPost("UserCreate")]
        public async Task<ActionResult> Create([FromBody] JsonObject obj)
        {
            if (obj["Email"] is null || obj["Displayname"] is null)
            {
                return BadRequest();
            }
            //_JsonObjectHandler.CheckJsonObject(obj);
            return Ok(/*await _UserService.Create(obj)*/);
        }
    }

    public class JsonObjectHandler
    {
        public bool CheckJsonObject(JsonObject obj)
        {
            var h = obj["Email"];
            return true;
        }
    }
}
/* test data
{
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