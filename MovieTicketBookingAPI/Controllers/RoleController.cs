using BusinessObjects;
using BusinessObjects.Dtos.Role;
using BusinessObjects.Dtos.Schema_Response;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Services.Service;

namespace MovieTicketBookingAPI.Controllers
{
    [Route("api/2024-11-11/roles")]
    [ApiController]
    public class RoleController(IRoleService roleService) : ControllerBase
    {
        private readonly IRoleService _roleService = roleService;

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<RoleDto>>> GetRoleById(int id)
        {
            try
            {
                var role = await _roleService.GetById(id);
                if (role == null)
                {
                    return NotFound(new ResponseModel<RoleDto>
                    {
                        Success = false,
                        Error = "Role not found",
                        ErrorCode = 404
                    });
                }
                var roleDto = new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                };
                return Ok(new ResponseModel<RoleDto>
                {
                    Success = true,
                    Data = roleDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<RoleDto> { Success = false, Error = ex.Message, ErrorCode = 500 });
            }
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<RoleDto>>> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetAll();
                var roleResponses = roles.Select(role => new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                });
                return Ok(new ResponseModel<IEnumerable<RoleDto>>
                {
                    Success = true,
                    Data = roleResponses
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel<IEnumerable<RoleDto>>
                {
                    Success = false,
                    Error = ex.Message,
                    ErrorCode = 500
                });
            }
        }

        [HttpPost]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> AddRole([FromBody] Role role)
        {
            var result = await _roleService.Add(role);
            return CreatedAtAction(nameof(AddRole), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] Role role)
        {
            var existingRole = await _roleService.GetById(id);
            if (existingRole == null) return NotFound();

            await _roleService.Update(role);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Tags("CRUD Server Only")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var existingRole = await _roleService.GetById(id);
            if (existingRole == null) return NotFound();

            await _roleService.Delete(id);
            return NoContent();
        }
    }
}
