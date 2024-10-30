using BusinessObjects;
using BusinessObjects.Dtos.Schema_Response;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Services.Service;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController(IPromotionService promotionService) : ControllerBase
    {
        private readonly IPromotionService _promotionService = promotionService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var promotion = await _promotionService.GetById(id);
            if (promotion == null) return NotFound();
            return Ok(promotion);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var promotions = await _promotionService.GetAll();
            return Ok(promotions);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Promotion promotion)
        {
            var createdPromotion = await _promotionService.Add(promotion);
            return CreatedAtAction(nameof(GetById), new { id = createdPromotion.Id }, createdPromotion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Promotion promotion)
        {
            var existingPromotion = await _promotionService.GetById(id);
            if (existingPromotion == null) return NotFound();

            promotion.Id = id;
            await _promotionService.Update(promotion);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingPromotion = await _promotionService.GetById(id);
            if (existingPromotion == null) return NotFound();

            await _promotionService.Delete(id);
            return NoContent();
        }

        [HttpGet("CheckDiscount/{quantity}")]
        public async Task<ActionResult<Promotion>> CheckDiscount(int quantity)
        {
            try
            {
                var promotion = await _promotionService.CheckDiscount(quantity);
                return Ok(promotion);
            }
            catch(Exception ex) { return NotFound(new ResponseModel<object> { Success = false, Error = ex.Message, ErrorCode = 404 }); }

        }
    }
}
