using DiscordClone.Api.Entities;
using DiscordClone.Api.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiscordClone.Api.Controllers
{
    
    [ApiController]
    public class GenericController<T> : ControllerBase where T : class
    {
        private IGenericService<T> _GenericService;

        public GenericController(IGenericService<T> GenericService)
        {
            _GenericService = GenericService;
        }

        [HttpGet("test")]
        public async Task<ActionResult<string>> test()
        {
           
            return Ok("hej");
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<T>>> GetAll()
        {
            var Items = await _GenericService.GetAll();

            if (Items is null)
            {
                return NotFound();
            }

            return Ok(Items);
        }

        [HttpGet("GetById/{Id}")]
        public async Task<ActionResult> GetById(Type Id)
        {
            if(Id != typeof(int) && Id != typeof(Guid))
            {
                return BadRequest("type error");
            }

            var Item = await _GenericService.GetById(Id);

            if (Item is null)
            {
                return NotFound();
            }

            return Ok(Item);
        }

        [HttpPost("Create")]
        public async virtual Task<ActionResult> Create([FromBody] T Value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object");
            }

            //return CreatedAtAction(nameof(Create), await _GenericService.Create(Value));
            return Ok(await _GenericService.Create(Value));
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] T Value)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object");
            }

            return Ok(await _GenericService.Update(Value));
        }

        [HttpDelete("DeleteById/{Id}")]
        public async Task<ActionResult> Delete(Type Id)
        {
            if (Id != typeof(int) && Id != typeof(Guid))
            {
                return BadRequest("type error");
            }
            return Ok(await _GenericService.Delete(Id));
        }
    }
}
