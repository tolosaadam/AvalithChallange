using MediatR;
using N5Company.Entities.Models;
using N5Company.Entities.Responses;
using N5Company.Entities.Validator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace N5Company.Commands.UpdatePermission
{
    public class UpdatePermissionCommand : IRequest<CommandResponse<Permission>>
    {
        public int Id { get; set; }
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }
        public int PermissionTypeId { get; set; }

        public UpdatePermissionCommand(int id, string employeeForename, string employeeSurname, int permissionTypeId)
        {
            Id = id;
            EmployeeForename = employeeForename;
            EmployeeSurname = employeeSurname;
            PermissionTypeId = permissionTypeId;
        }

        public async Task<ValidatorResponse> Validate() => await CustomValidator.ValidateModelAsync(this, new UpdatePermissionCommandValidator());


    }


}
