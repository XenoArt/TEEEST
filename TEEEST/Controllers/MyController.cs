using Microsoft.AspNetCore.Mvc;
using TEEEST.Models;
using TEEEST.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TEEEST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ActivesController : ControllerBase
    {
        private readonly IActiveService _activeService;
        private readonly ILogger<ActivesController> _logger;

        public ActivesController(IActiveService activeService, ILogger<ActivesController> logger)
        {
            _activeService = activeService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all active items
        /// </summary>
        /// <response code="200">Returns all active items</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Active>))]
        public async Task<ActionResult<IEnumerable<Active>>> GetActives()
        {
            _logger.LogInformation("Retrieving all active items");
            return Ok(await _activeService.GetAllActivesAsync());
        }

        /// <summary>
        /// Retrieves a specific active item by ID
        /// </summary>
        /// <param name="id">The ID of the active item</param>
        /// <response code="200">Returns the requested active item</response>
        /// <response code="404">If the active item is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Active))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Active>> GetActive(int id)
        {
            _logger.LogInformation($"Retrieving active item with ID: {id}");
            var active = await _activeService.GetActiveByIdAsync(id);

            if (active == null)
            {
                _logger.LogWarning($"Active item with ID {id} not found");
                return NotFound();
            }

            return Ok(active);
        }

        /// <summary>
        /// Updates an existing active item
        /// </summary>
        /// <param name="id">The ID of the active item to update</param>
        /// <param name="active">The updated active item data</param>
        /// <response code="204">If the update was successful</response>
        /// <response code="400">If the ID in URL doesn't match the ID in body or validation fails</response>
        /// <response code="404">If the active item is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutActive(int id, [FromBody] Active active)
        {
            if (id != active.Id)
            {
                _logger.LogWarning($"ID mismatch in PUT request. URL ID: {id}, Body ID: {active.Id}");
                return BadRequest("ID in URL does not match ID in body");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Model validation failed for active item with ID: {id}");
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation($"Updating active item with ID: {id}");
                await _activeService.UpdateActiveAsync(id, active);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Active item with ID {id} not found for update");
                return NotFound();
            }
        }

        /// <summary>
        /// Creates a new active item
        /// </summary>
        /// <param name="active">The active item to create</param>
        /// <response code="201">Returns the newly created active item</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Active))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Active>> PostActive([FromBody] Active active)
        {
            if (active == null)
            {
                _logger.LogWarning("Attempt to create null active item");
                return BadRequest("Active item cannot be null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model validation failed for new active item");
                return BadRequest(ModelState);
            }

            // No need to check HasValue for non-nullable bool
            // Mimdinare will default to false if not provided

            // Clear any provided ID to let the database generate it
            active.Id = 0;

            var createdActive = await _activeService.CreateActiveAsync(active);
            return CreatedAtAction(nameof(GetActive), new { id = createdActive.Id }, createdActive);
        }

        /// <summary>
        /// Deletes a specific active item
        /// </summary>
        /// <param name="id">The ID of the active item to delete</param>
        /// <response code="204">If the deletion was successful</response>
        /// <response code="404">If the active item is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteActive(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting active item with ID: {id}");
                await _activeService.DeleteActiveAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Active item with ID {id} not found for deletion");
                return NotFound();
            }
        }
    }
}