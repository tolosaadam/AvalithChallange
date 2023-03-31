using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using N5Company.Business.Interfaces;
using N5Company.Entities.DTOS;
using N5Company.Entities.Responses;
using System.Threading.Tasks;

namespace N5CompanyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly ILogger<PermissionController> _logger;
        private readonly IPermissionBusiness _permissionBusiness;

        public PermissionController(ILogger<PermissionController> logger, IPermissionBusiness permissionBusiness)
        {
            _logger = logger;
            _permissionBusiness = permissionBusiness;
        }

        /// <summary>
        /// Lists a given permission by their ID.
        /// </summary>
        /// <param name="id">Permission ID.</param>
        /// <returns>Permission.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PermissionDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> RequestPermission(int id)
        {
            var permission = await _permissionBusiness.GetPermissionAsync(id);
            if (permission == null) return NotFound($"Permission with id = {id} not found");
            return Ok(permission);
        }

        /// <summary>
        /// Lists all permissions from the API. 
        /// </summary>
        /// <returns>List of permissions.</returns>
        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            return Ok(await _permissionBusiness.GetAllPermissionsAsync());
        }

        /// <summary>
        /// Updates a given permission by their identifier.
        /// </summary>
        /// <param name="id">Permission ID.</param>
        /// <param name="model">Updated permission data.</param>
        /// <returns>Response for the request.</returns>
        /// <response code="200">Returns the response data container the updated permission information.</response>
        /// <response code="400">Return data containing information about why has the request failed.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CommandResponse<PermissionDTO>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> ModifyPermission(int id, [FromBody] PermissionDTO model)
        {
            var businessResponse = await _permissionBusiness.UpdatePermissionAsync(id, model);
            return ProducePermissionResponse(businessResponse);
        }

        private IActionResult ProducePermissionResponse(CommandResponse<PermissionDTO> response)
        {
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }
    }
}
