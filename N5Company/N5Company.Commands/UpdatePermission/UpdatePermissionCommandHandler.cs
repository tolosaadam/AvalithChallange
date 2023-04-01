using MediatR;
using N5Company.Entities.Models;
using N5Company.Entities.Responses;
using N5Company.Repositories.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace N5Company.Commands.UpdatePermission
{
    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, CommandResponse<Permission>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePermissionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResponse<Permission>> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await request.Validate();

            if (validationResult.Errors.Any()) return new CommandResponse<Permission>(string.Join(", ", validationResult.Errors));

            var permission = await _unitOfWork.GetRepository<Permission>().FindByIdAsync(request.Id);

            var permissionType = await _unitOfWork.GetRepository<PermissionType>().FindByIdAsync(request.PermissionTypeId);

            if (permissionType == null) return new CommandResponse<Permission>($"PermissionType not found. Id => {request.PermissionTypeId} invalid");

            if (permission == null) return new CommandResponse<Permission>("Permission not found.");

            permission.EmployeeForename = request.EmployeeForename;
            permission.EmployeeSurname = request.EmployeeSurname;
            permission.PermissionTypeId = request.PermissionTypeId;

            _unitOfWork.GetRepository<Permission>().Update(permission);

            await _unitOfWork.CommitAsync();

            return new CommandResponse<Permission>(permission);
        }
    }
}
