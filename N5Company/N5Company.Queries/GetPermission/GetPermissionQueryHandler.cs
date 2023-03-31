using MediatR;
using N5Company.Entities.Models;
using N5Company.Queries.GetPermissions;
using N5Company.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using N5Company.Entities.Responses;
using System.Linq;

namespace N5Company.Queries.GetPermission
{
    public class GetPermissionQueryHandler : IRequestHandler<GetPermissionQuery, Permission>
    {
        private readonly IPermissionRepository _permissionRepository;

        public GetPermissionQueryHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<Permission> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await request.Validate();

            if (validationResult.Errors.Any()) throw new ArgumentException(string.Join(", ", validationResult.Errors));

            return await _permissionRepository.FindByIdAsync(request.Id);
        }
    }
}
