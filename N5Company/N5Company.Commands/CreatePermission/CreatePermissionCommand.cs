using MediatR;
using N5Company.Entities.Models;
using N5Company.Entities.Responses;
using N5Company.Entities.Validator;
using System;
using System.Threading.Tasks;

namespace N5Company.Commands.CreatePermission
{
    public class CreatePermissionCommand : IRequest<CommandResponse<Permission>>
    {
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }
        public int PermissionTypeId { get; set; }
        public DateTime PermissionDate { get; set; }

        public CreatePermissionCommand(string employeeForename, string employeeSurname, int permissionTypeId)
        {
            EmployeeForename = employeeForename;
            EmployeeSurname = employeeSurname;
            PermissionTypeId = permissionTypeId;
            PermissionDate = DateTime.Now;
        }

        public async Task<ValidatorResponse> Validate() => await CustomValidator.ValidateModelAsync(this, new CreatePermissionCommandValidator());


    }
}
