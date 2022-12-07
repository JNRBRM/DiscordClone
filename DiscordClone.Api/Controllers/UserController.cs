using DiscordClone.Api.Entities;
using DiscordClone.Api.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiscordClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : GenericController<User>
    {

        public UserController(IGenericService<User> GenericService) : base(GenericService)
        {
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] User User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("entity error");
            }
            return Ok(User);
        }
    }
}
