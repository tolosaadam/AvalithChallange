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
        Task<CommandResponse<PermissionDTOGet>> CreatePermissionAsync(PermissionDTO model);
        Task<IEnumerable<PermissionDTOGet>> GetAllPermissionsAsync();
        Task<CommandResponse<PermissionDTOGet>> UpdatePermissionAsync(int id, PermissionDTO model);
        Task<IEnumerable<PermissionDTOGet>> GetAllPermissionsFromElasticAsync();
    }
}
