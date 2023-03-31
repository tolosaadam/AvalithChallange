using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using N5Company.Business.Interfaces;
using N5Company.Entities.DTOS;
using N5Company.Entities.Responses;
using System;
using System.Net;
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
        /// Creates a new permission in the API.
        /// </summary>
        /// <param name="model">Updated permission data.</param>
        /// <returns>Response for the request.</returns>
        /// <response code="201">Returns the newly created permission data.</response>
        /// <response code="400">Return data containing information about why has the request failed.</response>
        [HttpPost]
        [ProducesResponseType(typeof(PermissionDTO), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> RequestPermission([FromBody] PermissionDTO model)
        {
            _logger.LogInformation("Received request for {OperationName}", HttpContext.Request.Path);
            return await HandleBusinessResponse(async () => await _permissionBusiness.CreatePermissionAsync(model), HttpStatusCode.Created);
        }

        /// <summary>
        /// Lists all permissions from the API. 
        /// </summary>
        /// <returns>List of permissions.</returns>
        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            _logger.LogInformation("Received request for {OperationName}", HttpContext.Request.Path);
            return Ok(await _permissionBusiness.GetAllPermissionsAsync());
        }

        /// <summary>
        /// Lists all permissions from the API Hitting ElasticSearch. 
        /// </summary>
        /// <returns>List of permissions.</returns>
        [HttpGet("GetAllPermissionsElastic")]
        public async Task<IActionResult> GetPermissionsFromElastic()
        {
            _logger.LogInformation("Received request for {OperationName}", HttpContext.Request.Path);
            return Ok(await _permissionBusiness.GetAllPermissionsFromElasticAsync());
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
            _logger.LogInformation("Received request for {OperationName}", HttpContext.Request.Path);
            return await HandleBusinessResponse(async () => await _permissionBusiness.UpdatePermissionAsync(id, model), HttpStatusCode.OK);
        }

        private async Task<IActionResult> HandleBusinessResponse<TData>(Func<Task<CommandResponse<TData>>> businessAction, HttpStatusCode successStatusCode)
        {
            var businessResponse = await businessAction();

            if (businessResponse.Success)
            {
                return StatusCode((int)successStatusCode, businessResponse.Data);
            }
            else
            {
                return BadRequest(businessResponse.Message);
            }
        }
    }
}
