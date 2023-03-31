using AutoMapper;
using MediatR;
using N5Company.Business.Interfaces;
using N5Company.Commands.UpdatePermission;
using N5Company.Entities.DTOS;
using N5Company.Entities.Models;
using N5Company.Entities.Responses;
using N5Company.Queries.GetPermission;
using N5Company.Queries.GetPermissions;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace N5Company.Business
{
    public class PermissionBusiness : IPermissionBusiness
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public PermissionBusiness(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PermissionDTO>> GetAllPermissionsAsync()
        {
            var permissions = await _mediator.Send(new GetAllPermissionsQuery());
            return _mapper.Map<IEnumerable<Permission>, IEnumerable<PermissionDTO>>(permissions);
        }

        public async Task<PermissionDTO> GetPermissionAsync(int id)
        {
            var permission = await _mediator.Send(new GetPermissionQuery(id));
            return _mapper.Map<Permission, PermissionDTO>(permission);
        }

        public async Task<CommandResponse<PermissionDTO>> UpdatePermissionAsync(int id, PermissionDTO model)
        {
            var permission = _mapper.Map<PermissionDTO, Permission>(model);
            var commandResponse = await _mediator.Send(new UpdatePermissionCommand(id, permission.EmployeeForename, permission.EmployeeSurname, permission.PermissionTypeId));
            return _mapper.Map<CommandResponse<Permission>, CommandResponse<PermissionDTO>>(commandResponse);
        }
    }
    
}
