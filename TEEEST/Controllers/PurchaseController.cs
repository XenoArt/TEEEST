using Microsoft.AspNetCore.Mvc;
using TEEEST.Models;

namespace TEEEST.Controllers
{
    [Route("api/[controller]")] // Define route to make it an API endpoint
    [ApiController]  // Ensure it’s treated as an API controller
    public class PurchaseController : Controller
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseController(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        // GET: /Purchase
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var purchases = await _purchaseRepository.GetAllPurchasesAsync();
            return Ok(purchases); // Ensure you're returning an appropriate response for an API
        }

        // POST: /Purchase/Add
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PurchaseRecord purchase)
        {
            if (purchase == null)
            {
                return BadRequest("Purchase data is missing.");
            }

            if (ModelState.IsValid)
            {
                await _purchaseRepository.AddPurchaseAsync(purchase);
                return CreatedAtAction(nameof(Index), new { id = purchase.Id }, purchase);
            }
            return BadRequest(ModelState);
        }
        // DELETE: /Purchase/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _purchaseRepository.RemovePurchaseAsync(id);
            return NoContent(); // Indicate that the resource was deleted successfully
        }
    }
}