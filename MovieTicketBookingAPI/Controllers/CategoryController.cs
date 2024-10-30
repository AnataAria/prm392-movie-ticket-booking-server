using BusinessObjects;
using BusinessObjects.Dtos.Schema_Response;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Services.Service;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<Category>>> GetById(int id)
        {
            try {
                var category = await _categoryService.GetById(id);
                if (category == null) return NotFound(new ResponseModel<Category>() {
                    Data = null,
                    Error = $"Not found category with id {id}",
                    Success = false,
                    ErrorCode = 404
                });
                return Ok(new ResponseModel<Category>() {
                    Data = category,
                    Error = null,
                    Success = true,
                    ErrorCode = 200
                });
            } catch (Exception ex) {
                return StatusCode(500, new ResponseModel<Category>() {
                    Data = null,
                    Error = ex.Message,
                    Success = false,
                    ErrorCode = 500
                });
            }
            
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<Category>>>> GetAll()
        {
            try {
                var categories = await _categoryService.GetAll();
                return Ok(new ResponseModel<IEnumerable<Category>>() {
                    Data = categories,
                    Error = null,
                    Success = true,
                    ErrorCode = 200
                });
            } catch(Exception ex) {
                return StatusCode(500, new ResponseModel<IEnumerable<Category>>() {
                    Data = null,
                    Error = ex.Message,
                    Success = false,
                    ErrorCode = 500
                });
            }
            
            
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            var createdCategory = await _categoryService.Add(category);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category category)
        {
            var existingCategory = await _categoryService.GetById(id);
            if (existingCategory == null) return NotFound();

            category.Id = id;
            await _categoryService.Update(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingCategory = await _categoryService.GetById(id);
            if (existingCategory == null) return NotFound();

            await _categoryService.Delete(id);
            return NoContent();
        }

        [HttpGet("GetCategoryByName/{name}")]
        public async Task<IActionResult> GetCategoryByName(string name)
        {
            var category = await _categoryService.getByCateName(name);
            if (category == null) return NotFound();
            return Ok(category);
        }
    }
}
