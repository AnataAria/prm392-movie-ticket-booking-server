using BusinessObjects;
using BusinessObjects.Dtos.Auth;
using BusinessObjects.Dtos.Movie;
using BusinessObjects.Dtos.Promotion;
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
        public async Task<ActionResult<ResponseModel<PromotionDto>>> GetById(int id)
        {
            try
            {
                var promotion = await _promotionService.GetById(id);
                if (promotion == null)
                {
                    return NotFound(new ResponseModel<PromotionDto>
                    {
                        Success = false,
                        Error = "Promotion not found",
                        ErrorCode = 404
                    });
                }

                var promotionDto = new PromotionDto
                {
                    Id = promotion.Id,
                    Condition = promotion.Condition,
                    Discount = promotion.Discount,
                };

                return Ok(new ResponseModel<PromotionDto>
                {
                    Success = true,
                    Data = promotionDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<PromotionDto> { Success = false, Error = ex.Message, ErrorCode = 500 });
            }
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<PromotionDto>>> GetAll()
        {
            try
            {
                var promotions = await _promotionService.GetAll();
                var promotionResponses = promotions.Select(promotion => new PromotionDto
                {
                    Id = promotion.Id,
                    Condition = promotion.Condition,
                    Discount = promotion.Discount,
                });
                return Ok(new ResponseModel<IEnumerable<PromotionDto>>
                {
                    Success = true,
                    Data = promotionResponses
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<IEnumerable<PromotionDto>>
                {
                    Success = false,
                    Error = ex.Message,
                    ErrorCode = 500
                });
            }
        }

        [HttpPost]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> Create([FromBody] Promotion promotion)
        {
            var createdPromotion = await _promotionService.Add(promotion);
            return CreatedAtAction(nameof(GetById), new { id = createdPromotion.Id }, createdPromotion);
        }

        [HttpPut("{id}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> Update(int id, [FromBody] Promotion promotion)
        {
            var existingPromotion = await _promotionService.GetById(id);
            if (existingPromotion == null) return NotFound();

            promotion.Id = id;
            await _promotionService.Update(promotion);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingPromotion = await _promotionService.GetById(id);
            if (existingPromotion == null) return NotFound();

            await _promotionService.Delete(id);
            return NoContent();
        }

        //[HttpGet("CheckDiscount/{quantity}")]
        //public async Task<ActionResult<Promotion>> CheckDiscount(int quantity)
        //{
        //    try
        //    {
        //        var promotion = await _promotionService.CheckDiscount(quantity);
        //        return Ok(promotion);
        //    }
        //    catch(Exception ex) { return NotFound(new ResponseModel<object> { Success = false, Error = ex.Message, ErrorCode = 404 }); }

        //}
    }
}
