using MediatR;
using N5Company.Commands.UpdatePermission;
using N5Company.Entities.Models;
using N5Company.Entities.Responses;
using N5Company.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace N5Company.Commands.CreatePermission
{
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, CommandResponse<Permission>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePermissionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResponse<Permission>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await request.Validate();

            if (validationResult.Errors.Any()) return new CommandResponse<Permission>(string.Join(", ", validationResult.Errors));

            var permission = new Permission
            {
                EmployeeForename = request.EmployeeForename,
                EmployeeSurname = request.EmployeeSurname,
                PermissionTypeId = request.PermissionTypeId,
                PermissionDate = request.PermissionDate 
            };
            

            await _unitOfWork.GetRepository<Permission>().AddAsync(permission);

            await _unitOfWork.CommitAsync();

            return new CommandResponse<Permission>(permission);
        }
    }
}
