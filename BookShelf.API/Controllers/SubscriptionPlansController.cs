using BookShelf.Application.DTOs.SubscriptionPlan;
using BookShelf.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionPlansController : ControllerBase
    {
        private readonly ISubscriptionPlanService _service;

        public SubscriptionPlansController(ISubscriptionPlanService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var plans = await _service.GetAllAsync();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var plan = await _service.GetByIdAsync(id);
            if (plan == null) return NotFound();
            return Ok(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubscriptionPlanRequestDto dto)
        {
            var plan = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = plan.Id }, plan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SubscriptionPlanRequestDto dto)
        {
            var plan = await _service.UpdateAsync(id, dto);
            return Ok(plan);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
