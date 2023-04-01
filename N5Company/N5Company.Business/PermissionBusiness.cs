using AutoMapper;
using MediatR;
using N5Company.Business.Interfaces;
using N5Company.Commands.CreatePermission;
using N5Company.Commands.UpdatePermission;
using N5Company.Entities.DTOS;
using N5Company.Entities.Models;
using N5Company.Entities.Responses;
using N5Company.Queries.GetPermissions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace N5Company.Business
{
    public class PermissionBusiness : IPermissionBusiness
    {
        private readonly IMediator _mediator;
        private readonly IElasticSearchBusiness<Permission> _elasticSearchBusiness;
        private readonly IMapper _mapper;
        public PermissionBusiness(IMediator mediator, IElasticSearchBusiness<Permission> elasticSearchBusiness, IMapper mapper)
        {
            _mediator = mediator;
            _elasticSearchBusiness = elasticSearchBusiness;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PermissionDTOGet>> GetAllPermissionsAsync()
        {
            var permissions = await _mediator.Send(new GetAllPermissionsQuery());
            foreach (Permission permission in permissions)
            {
                await _elasticSearchBusiness.IndexAsync(permission);
            };
            return _mapper.Map<IEnumerable<Permission>, IEnumerable<PermissionDTOGet>>(permissions);
        }

        public async Task<CommandResponse<PermissionDTOGet>> CreatePermissionAsync(PermissionDTO model)
        {
            var permission = _mapper.Map<PermissionDTO, Permission>(model);
            var commandResponse = await _mediator.Send(new CreatePermissionCommand(permission.EmployeeForename, permission.EmployeeSurname, permission.PermissionTypeId));
            if (commandResponse.Success) await _elasticSearchBusiness.IndexAsync(commandResponse.Data);
            return _mapper.Map<CommandResponse<Permission>, CommandResponse<PermissionDTOGet>>(commandResponse);
        }

        public async Task<CommandResponse<PermissionDTOGet>> UpdatePermissionAsync(int id, PermissionDTO model)
        {
            var permission = _mapper.Map<PermissionDTO, Permission>(model);
            var commandResponse = await _mediator.Send(new UpdatePermissionCommand(id, permission.EmployeeForename, permission.EmployeeSurname, permission.PermissionTypeId));
            if(commandResponse.Success) await _elasticSearchBusiness.IndexAsync(commandResponse.Data);
            return _mapper.Map<CommandResponse<Permission>, CommandResponse<PermissionDTOGet>>(commandResponse);
        }

        public async Task<IEnumerable<PermissionDTOGet>> GetAllPermissionsFromElasticAsync()
        {
            var permissions = await _elasticSearchBusiness.GetAllAsync();
            return _mapper.Map<IEnumerable<Permission>, IEnumerable<PermissionDTOGet>>(permissions);
        }
    }
    
}
