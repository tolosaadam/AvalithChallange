using N5Company.Entities.DTOS;
using N5Company.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace N5Company.Business.Interfaces
{
    public interface IPermissionBusiness
    {
        Task<CommandResponse<PermissionDTO>> CreatePermissionAsync(PermissionDTO model);
        Task<IEnumerable<PermissionDTO>> GetAllPermissionsAsync();
        Task<CommandResponse<PermissionDTO>> UpdatePermissionAsync(int id, PermissionDTO model);
        Task<IEnumerable<PermissionDTO>> GetAllPermissionsFromElasticAsync();
    }
}
