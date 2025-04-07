using Microsoft.AspNetCore.Mvc;
using TEEEST.Models;
using TEEEST.Services;

namespace TEEEST.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EndOrEditController : ControllerBase
    {
        private readonly IEndOrEditService _service;

        public EndOrEditController(IEndOrEditService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EndOrEdit>>> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EndOrEdit>> Get(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<EndOrEdit>> Create(EndOrEdit entity)
        {
            var created = await _service.CreateAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EndOrEdit entity)
        {
            if (id != entity.Id) return BadRequest();
            var updated = await _service.UpdateAsync(entity);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
