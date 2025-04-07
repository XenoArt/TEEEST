using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TEEEST.Models;
using TEEEST.Services;

namespace TEEEST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashregController : ControllerBase
    {
        private readonly ICashregService _service;

        public CashregController(ICashregService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<Cashreg>> Get()
        {
            return Ok(await _service.GetCurrentRegister());
        }

        [HttpPost]
        public async Task<ActionResult<Cashreg>> Post([FromBody] CashregInput input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _service.UpdateRegister(input.Cash, input.Card));
        }

        [HttpPost("reset-add")]
        public async Task<ActionResult<Cashreg>> ResetAndAdd([FromBody] ResetAddInput input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _service.ResetAndAddCash(input.Amount));
        }

        [HttpPost("update-card")]
        public async Task<ActionResult<Cashreg>> UpdateCard([FromBody] UpdateCardInput input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _service.UpdtCard(input.Amount));
        }

        [HttpPost("update-cash")]
        public async Task<ActionResult<Cashreg>> UpdateCash([FromBody] UpdateCashInput input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _service.UpdtCash(input.Amount));
        }

        public class CashregInput
        {
            [Required, Range(0, double.MaxValue)]
            public decimal Cash { get; set; }

            [Required, Range(0, double.MaxValue)]
            public decimal Card { get; set; }
        }

        public class ResetAddInput
        {
            [Required, Range(0, double.MaxValue)]
            public decimal Amount { get; set; }
        }

        public class UpdateCardInput
        {
            [Required, Range(0, double.MaxValue)]
            public decimal Amount { get; set; }
        }

        public class UpdateCashInput
        {
            [Required, Range(0, double.MaxValue)]
            public decimal Amount { get; set; }
        }
    }
}
