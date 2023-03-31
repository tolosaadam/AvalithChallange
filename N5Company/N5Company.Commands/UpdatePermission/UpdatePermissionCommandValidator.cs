using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace N5Company.Commands.UpdatePermission
{
    public class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
    {
        public UpdatePermissionCommandValidator()
        {
            RuleFor(x => x.EmployeeForename)
                .NotEmpty().WithMessage("Employee forename is required.");

            RuleFor(x => x.EmployeeSurname)
                .NotEmpty().WithMessage("Employee surname is required.");

            RuleFor(x => x.PermissionTypeId)
                .GreaterThan(0).WithMessage("Permission type is required.");
        }
    }
}
