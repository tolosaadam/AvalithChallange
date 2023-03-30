using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using N5Company.Interfaces.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N5CompanyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly ILogger<PermissionController> _logger;
        private readonly IPermissionBusiness _permissionBusiness;

        public PermissionController(ILogger<PermissionController> logger, IPermissionBusiness permissionBusiness)
        {
            _logger = logger;
            _permissionBusiness = permissionBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RequestPermission()
        {
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> ModifyPermission()
        {
            return Ok();
        }
    }
}
